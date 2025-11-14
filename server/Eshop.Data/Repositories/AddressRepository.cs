using Eshop.Data.Interfaces;
using Eshop.Data.Models;

namespace Eshop.Data.Repositories
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(AppDBContext dbContext) : base(dbContext)
        {
        }
    }
}
