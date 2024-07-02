using AutoMapper;
using Domain.Auth.Validators;
using Domain.Users;

namespace Application.Mappers
{
    public class UserProfile: Profile
    {
        public UserProfile() 
        {
            CreateMap<CreateUserValidator, User>();
            CreateMap<User, UserResponse>();
        }
    }
}
