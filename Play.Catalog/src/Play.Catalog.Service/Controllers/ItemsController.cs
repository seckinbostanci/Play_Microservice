using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Play.Catalog.Repositories;
using Play.Catalog.Service.Entities;
using static Play.Catalog.Contracts.Contracts;
using static Play.Catalog.Service.DTOs;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<Item> _itemsRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public ItemsController(IRepository<Item> itemsRepository, IPublishEndpoint publishEndpoint = null)
        {
            _itemsRepository = itemsRepository;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetAsync()
        {
            var items = (await _itemsRepository.GetAllAsync()).Select(item => item.AsDto());
            return items;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid Id)
        {
            var item = (await _itemsRepository.GetAsync(Id)).AsDto();
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItem)
        {
            var item = new Item()
            {
                Name = createItem.Name,
                Description = createItem.Description,
                Price = createItem.Price,
                CreateDate = DateTimeOffset.Now
            };

            await _itemsRepository.CreateAsync(item);

            await _publishEndpoint.Publish(new CatalogItemCreated(item.Id, item.Name, item.Description));

            return CreatedAtAction(nameof(GetByIdAsync), new { Id = item.Id }, item);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PostAsync(Guid Id, UpdateItemDto updateItem)
        {
            var existingItem = await _itemsRepository.GetAsync(Id);

            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Name = updateItem.Name;
            existingItem.Description = updateItem.Description;
            existingItem.Price = updateItem.Price;

            await _itemsRepository.UpdateAsync(existingItem);

            await _publishEndpoint.Publish(new CatalogItemUpdated(existingItem.Id, existingItem.Name, existingItem.Description));

            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var exitItem = await _itemsRepository.GetAsync(Id);
            if (exitItem == null)
            {
                return NotFound();
            }
            await _itemsRepository.DeleteAsync(exitItem.Id);

            await _publishEndpoint.Publish(new CatalogItemDeleted(exitItem.Id));

            return NoContent();
        }
    }
}