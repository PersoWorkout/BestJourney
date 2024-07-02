using AutoMapper;
using Domain.DTOs.Responses;
using Domain.DTOs.Validators.Auth;
using Domain.Models;

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
