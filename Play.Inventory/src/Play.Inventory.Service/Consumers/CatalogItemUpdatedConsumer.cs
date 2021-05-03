using System.Threading.Tasks;
using MassTransit;
using Play.Catalog.Repositories;
using Play.Inventory.Service.Entities;
using static Play.Catalog.Contracts.Contracts;

namespace Play.Inventory.Service.Consumers
{
    public class CatalogItemUpdatedConsumer : IConsumer<CatalogItemUpdated>
    {
        private readonly IRepository<CatalogItem> _inventoryItemRepository;

        public CatalogItemUpdatedConsumer(IRepository<CatalogItem> inventoryItemRepository)
        {
            _inventoryItemRepository = inventoryItemRepository;
        }

        public async Task Consume(ConsumeContext<CatalogItemUpdated> context)
        {
            var message = context.Message;
            var item = await _inventoryItemRepository.GetAsync(message.ItemId);
            if (item == null)
            {
                item = new CatalogItem()
                {
                    Id = message.ItemId,
                    Name = message.Name,
                    Description = message.Description
                };

                await _inventoryItemRepository.CreateAsync(item);
            }
            else
            {
                item.Name = message.Name;
                item.Description = message.Description;
                await _inventoryItemRepository.UpdateAsync(item);
            }
        }
    }
}