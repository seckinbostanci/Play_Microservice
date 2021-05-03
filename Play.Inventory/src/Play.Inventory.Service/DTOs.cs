using System;

namespace Play.Inventory.Service
{
    public class DTOs
    {
        public record GrantItemsDto(Guid UserId, Guid CatalogItemId, int Quantity);

        public record InventoryItemDto(Guid CatalogItemId, string Name, string Description, int Quantity, DateTimeOffset AcquiredDate);

    }
}