using Eshop.Data.Interfaces;
using Eshop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Data.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDBContext dbContext) : base(dbContext)
        {
        }

        public Order? FindByIdWithItemsAndAddress(uint id)
        {
            // Vraci objednavku včetně položek a adresy
            return dbContext.Orders!
                .Include(a => a.Address)
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Book)
                .FirstOrDefault(o => o.Id == id);
        }
    }
}
