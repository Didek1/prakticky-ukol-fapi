using Eshop.Data.Interfaces;
using Eshop.Data.Models;

namespace Eshop.Data.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(AppDBContext dbContext) : base(dbContext)
        {
        }
    }
}
