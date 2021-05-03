using System.Threading.Tasks;
using MassTransit;
using Play.Catalog.Repositories;
using Play.Inventory.Service.Entities;
using static Play.Catalog.Contracts.Contracts;

namespace Play.Inventory.Service.Consumers
{
    public class CatalogItemCreatedConsumer : IConsumer<CatalogItemCreated>
    {
        private readonly IRepository<CatalogItem> _inventoryItemRepository;

        public CatalogItemCreatedConsumer(IRepository<CatalogItem> inventoryItemRepository)
        {
            _inventoryItemRepository = inventoryItemRepository;
        }

        public async Task Consume(ConsumeContext<CatalogItemCreated> context)
        {
            var message = context.Message;
            var item = await _inventoryItemRepository.GetAsync(message.ItemId);
            if (item != null)
            {
                return;
            }

            item = new CatalogItem()
            {
                Id = message.ItemId,
                Name = message.Name,
                Description = message.Description
            };

            await _inventoryItemRepository.CreateAsync(item);
        }
    }
}