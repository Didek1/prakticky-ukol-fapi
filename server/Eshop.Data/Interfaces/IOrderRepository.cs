using Eshop.Data.Models;

namespace Eshop.Data.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Order? FindByIdWithItemsAndAddress(uint id);
    }
}
