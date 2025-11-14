using Eshop.Api.Models;

namespace Eshop.Api.Interfaces
{
    public interface IBookManager
    {
        IList<BookDto> GetAllBooks();
    }
}
