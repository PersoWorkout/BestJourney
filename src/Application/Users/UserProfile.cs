using AutoMapper;
using Domain.Auth.Requests.Customers;
using Domain.Auth.Requests.Suppliers;
using Domain.Users;

namespace Application.Users;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterCustomerRequest, User>();
        CreateMap<RegisterSupplierRequest, User>();
        CreateMap<User, UserResponse>();
    }
}
