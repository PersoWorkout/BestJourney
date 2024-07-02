using AutoMapper;
using Domain.DTOs.Responses;
using Domain.DTOs.Validators.Orders;
using Domain.Models;

namespace Application.Mappers;

public class OrderProfile: Profile
{
    public OrderProfile()
    {
        CreateMap<CreateOrderValidator, Order>()
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

        CreateMap<CreateOrderValidator, Order>();
    }
}
