﻿using Domain.Users.Requests;

namespace Domain.UnitTest.DTOs.Users
{
    public class UpdateUserValidatorTest
    {
        [Fact]
        public void Validate_ShouldBeTrue_WhenPayloadIsValid()
        {
            //Arrange
            var payload = CreateValidPayload();

            //Act
            var result = payload.Validate();

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_ShouldBeFalse_WhenPayloadIsEmpty()
        {
            //Arrange
            var payload = CreateInvalidPayload();

            //Act
            var result = payload.Validate();

            //Assert
            Assert.False(result);
        }

        private static UpdateCustomerRequest CreateValidPayload()
        {
            return new UpdateCustomerRequest
            {
                Firstname = "John",
                Lastname = "Doe",
                Email = "john.doe@example.com"
            };
        }

        private static UpdateCustomerRequest CreateInvalidPayload()
        {
            return new UpdateCustomerRequest();
        }
    }
}
