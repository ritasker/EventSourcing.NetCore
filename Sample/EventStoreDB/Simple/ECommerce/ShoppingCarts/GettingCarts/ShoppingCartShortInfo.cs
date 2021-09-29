using System;

namespace ECommerce.ShoppingCarts.GettingCarts
{
    public record ShoppingCartShortInfo
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public int TotalItemsCount { get; set; }
        public ShoppingCartStatus Status { get; set; }
        public int Version { get; set; }
    }

    public class ShoppingCartShortInfoProjection
    {
        public static ShoppingCartShortInfo Handle(ShoppingCartInitialized @event)
        {
            var (shoppingCartId, clientId, shoppingCartStatus) = @event;

            return new ShoppingCartShortInfo
            {
                Id = shoppingCartId,
                ClientId = clientId,
                TotalItemsCount = 0,
                Status = shoppingCartStatus,
                Version = 1
            };
        }

        public static void Handle(ShoppingCartConfirmed @event, ShoppingCartShortInfo view)
        {
            view.Status = ShoppingCartStatus.Confirmed;
            view.Version++;
        }
    }
}
