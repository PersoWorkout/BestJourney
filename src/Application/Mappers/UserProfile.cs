using AutoMapper;
using Domain.DTOs.Responses;
using Domain.DTOs.Validators.Auth;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
