﻿using Application.Users;
using AutoMapper;
using Domain.Auth.Requests;
using Domain.Auth.Requests.Customers;
using Domain.Users;

namespace Application.UnitTest.Mappers
{
    public class UserProfileTest
    {
        private readonly IMapper _mapper;

        public UserProfileTest() 
        {
            MapperConfiguration configuration = new(config =>
            {
                config.CreateMap<string, Guid>().ConvertUsing(value => new Guid(value));
                config.CreateMap<Guid, string>().ConvertUsing(value => value.ToString());
                config.AddProfile(new UserProfile());
            });

            _mapper = new Mapper(configuration);
        }

        [Fact]
        public void Map_ShouldReturnUserResponse_WhenParameterIsUser()
        {
            //Arrange
            var user = new User(
                "John",
                "Doe",
                "john.doe@example.com",
                "Password123!",
                "0606060606");

            //Act
            var result = _mapper.Map<UserResponse>(user);

            //Assert
            Assert.IsType<UserResponse>(result);
        }

        [Fact]
        public void Map_ShouldReturnUser_WhenParameterIsUserValidator()
        {
            //Arrange
            var payload = new RegisterCustomerRequest
            {
                Firstname = "John",
                Lastname = "Doe",
                Email = "john.doe@example.com",
                Password = "Password123!",
                PasswordConfirmation = "Password123!",
                Phone = "0606060606"
            };

            //Act
            var result = _mapper.Map<User>(payload);

            //Assert
            Assert.IsType<User>(result);
        }
    }
}
