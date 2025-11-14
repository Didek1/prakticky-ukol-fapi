using Eshop.Api.Models;

namespace Eshop.Api.Interfaces
{
    public interface IOrderItemManager
    {
        OrderItemDto AddOrderItem(OrderItemDto orderItem);
    }
}
