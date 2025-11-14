using AutoMapper;
using Eshop.Api.Interfaces;
using Eshop.Api.Models;
using Eshop.Data.Interfaces;
using Eshop.Data.Models;

namespace Eshop.Api.Managers
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository bookRepository;
        private readonly IMapper mapper;

        public BookManager(IBookRepository bookRepository, IMapper mapper)
        {
            this.bookRepository = bookRepository;
            this.mapper = mapper;
        }

        public IList<BookDto> GetAllBooks()
        {
            IList<Book> books = bookRepository.GetAll();
            return mapper.Map<IList<BookDto>>(books);
        }
    }
}
