using System.Threading.Tasks;
using MassTransit;
using Play.Catalog.Repositories;
using Play.Inventory.Service.Entities;
using static Play.Catalog.Contracts.Contracts;

namespace Play.Inventory.Service.Consumers
{
    public class CatalogItemDeletedConsumer : IConsumer<CatalogItemDeleted>
    {
        private readonly IRepository<CatalogItem> _inventoryItemRepository;

        public CatalogItemDeletedConsumer(IRepository<CatalogItem> inventoryItemRepository)
        {
            _inventoryItemRepository = inventoryItemRepository;
        }

        public async Task Consume(ConsumeContext<CatalogItemDeleted> context)
        {
            var message = context.Message;
            var item = await _inventoryItemRepository.GetAsync(message.ItemId);
            if (item == null)
            {
                return;
            }
            await _inventoryItemRepository.DeleteAsync(message.ItemId);
        }
    }
}