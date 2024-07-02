using AutoMapper;
using Domain.Auth.Requests;
using Domain.Users;

namespace Application.Users;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserRequest, User>();
        CreateMap<User, UserResponse>();
    }
}
