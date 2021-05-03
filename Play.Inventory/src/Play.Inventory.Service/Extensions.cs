using Play.Inventory.Service.Entities;
using static Play.Inventory.Service.DTOs;

namespace Play.Inventory.Service
{
    public static class Extensions
    {
        public static InventoryItemDto AsDto(this InventoryItem item, string Name, string Description)
        {
            return new InventoryItemDto(item.CatalogItemId, Name, Description, item.Quantity, item.AcquiredDate);
        }
    }
}