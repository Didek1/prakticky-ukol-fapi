using AutoMapper;
using Eshop.Api.Interfaces;
using Eshop.Api.Models;
using Eshop.Data.Interfaces;
using Eshop.Data.Models;

namespace Eshop.Api.Managers
{
    public class OrderItemManager : IOrderItemManager
    {
        private readonly IOrderItemRepository orderItemRepository;
        private readonly IMapper mapper;

        public OrderItemManager(IOrderItemRepository orderItemRepository, IMapper mapper)
        {
            this.orderItemRepository = orderItemRepository;
            this.mapper = mapper;
        }

        public OrderItemDto AddOrderItem(OrderItemDto orderItemDto)
        {
            OrderItem orderItem = mapper.Map<OrderItem>(orderItemDto);
            OrderItem addedOrderItem = orderItemRepository.Insert(orderItem);

            return mapper.Map<OrderItemDto>(addedOrderItem);
        }
    }
}
