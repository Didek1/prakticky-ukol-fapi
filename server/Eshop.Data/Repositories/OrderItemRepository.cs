using Eshop.Data.Interfaces;
using Eshop.Data.Models;

namespace Eshop.Data.Repositories
{
    public class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(AppDBContext dbContext) : base(dbContext)
        {
        }
    }
}
