using Application.Interfaces.Users;
using AutoMapper;
using Domain.Abstractions;
using Domain.DTOs.Responses;
using Domain.DTOs.Validators.Users;
using Domain.Errors.Users;
using Domain.Models;

namespace Application.Services
{
    public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<UserResponse>> Create(CreateUserValidator payload)
        {
            if (!payload.Validate()) 
                return Result<UserResponse>.Failure(UserError.InvalidPayload);

            if (await _userRepository.GetByEmail(payload.Email) is not null)
                return Result<UserResponse>.Failure(UserError.EmailAlreadyUsed);
            
            var user = _mapper.Map<User>(payload);

            await _userRepository.Create(user);

            return Result<UserResponse>.Success(
                _mapper.Map<UserResponse>(user));
        }

        public async Task<Result<UserResponse>> Delete(string id)
        {
            var user = await _userRepository.GetById(id);

            if (user is null)
                return Result<UserResponse>.Failure(UserError.NotFound);

            await _userRepository.Delete(user);

            return Result<UserResponse>.Success(new UserResponse());
        }

        public async Task<Result<string>> Login(LoginUserValidator payload)
        {
            if (!payload.Validate())
                return Result<string>.Failure(UserError.InvalidPayload);

            var user = await _userRepository.GetByEmail(payload.Email);
            if (user is null)
                return Result<string>.Failure(UserError.InvalidCredentials);

            if (!_userRepository.CheckPassword(user, payload.Password))
                return Result<string>.Failure(UserError.InvalidCredentials);

            return Result<string>.Success(user.Id.ToString());
        }

        public async Task<Result<UserResponse>> Update(string id, UpdateUserValidator payload)
        {
            if(!payload.Validate())
                return Result<UserResponse>.Failure(UserError.InvalidPayload);

            var user = await _userRepository.GetById(id);
            if (user is null)
                return Result<UserResponse>.Failure(UserError.NotFound);

            var userWithSameEmail = await _userRepository.GetByEmail(payload.Email);
            if (userWithSameEmail is not null &&
                userWithSameEmail.Id.ToString() != id)
                return Result<UserResponse>.Failure(UserError.EmailAlreadyUsed);

            user.Update(
                payload.Firstname,
                payload.Lastname,
                payload.Email,
                payload.Password);

            await _userRepository.Update(user);

            return Result<UserResponse>
                .Success(_mapper.Map<UserResponse>(user));
        }
    }
}
