using AutoMapper;

namespace demo1.model
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<OrderItemRequest, OrderItem>();
            CreateMap<OrderItem, OrderItemRequest>();

            CreateMap<GetOrders, Order>();
            CreateMap<Order, GetOrders>();

            CreateMap<GetOrdItem, OrderItem>();
            CreateMap<OrderItem, GetOrdItem>();



        }

    }
}
