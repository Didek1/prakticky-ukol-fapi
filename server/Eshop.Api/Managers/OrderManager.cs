using AutoMapper;
using Eshop.Api.Interfaces;
using Eshop.Api.Models;
using Eshop.Data.Interfaces;
using Eshop.Data.Models;

namespace Eshop.Api.Managers
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public OrderManager(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }

        public OrderDto AddOrder(OrderDto orderDto)
        {
            Order order = mapper.Map<Order>(orderDto);
            Order addedOrder = orderRepository.Insert(order);

            return mapper.Map<OrderDto>(addedOrder);
        }
        
        public OrderDto? GetOrder(uint id)
        {
            Order? order = orderRepository.FindByIdWithItemsAndAddress(id);

            if (order == null)
            {
                return null;
            }

            return mapper.Map<OrderDto>(order);
        }
    }
}
