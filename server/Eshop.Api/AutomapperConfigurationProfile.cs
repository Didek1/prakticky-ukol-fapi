using AutoMapper;
using Eshop.Api.Models;
using Eshop.Data.Models;

namespace Eshop.Api
{
    public class AutomapperConfigurationProfile : Profile
    {
        public AutomapperConfigurationProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();

            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();

            CreateMap<OrderItemDto, OrderItem>();
            CreateMap<OrderItem, OrderItemDto>();

            CreateMap<AddressDto, Address>();
            CreateMap<Address, AddressDto>();
        }
    }
}
