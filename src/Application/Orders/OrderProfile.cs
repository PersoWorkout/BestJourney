using AutoMapper;
using Domain.Orders;
using Domain.Orders.Requests;

namespace Application.Orders;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<CreateOrderRequest, Order>()
            .ForMember(dest => dest.JourneyId,
                opt => opt.MapFrom(
                    src => Guid.Parse(src.JourneyId)));

        CreateMap<Order, OrderResponse>()
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(
                    src => src.Status.ToString()))
            .ForMember(dest => dest.Journey,
                opt => opt.MapFrom(
                    src => src.Journey));
    }
}
