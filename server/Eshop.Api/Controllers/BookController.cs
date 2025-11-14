using Eshop.Api.Interfaces;
using Eshop.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookManager bookManager;

        public BookController(IBookManager bookManager)
        {
            this.bookManager = bookManager;
        }

        [HttpGet("books")]
        public IEnumerable<BookDto> GetBooks()
        {
            return bookManager.GetAllBooks();
        }
    }
}
