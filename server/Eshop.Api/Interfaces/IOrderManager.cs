using Eshop.Api.Models;

namespace Eshop.Api.Interfaces
{
    public interface IOrderManager
    {
        OrderDto? GetOrder(uint id);
        OrderDto AddOrder(OrderDto order);
    }
}
