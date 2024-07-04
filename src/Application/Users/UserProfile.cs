using AutoMapper;
using Domain.Auth.Requests.Customers;
using Domain.Users;

namespace Application.Users;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterCustomerRequest, User>();
        CreateMap<User, UserResponse>();
    }
}
