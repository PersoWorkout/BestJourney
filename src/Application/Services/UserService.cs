using Application.Interfaces;
using Application.Interfaces.Users;
using AutoMapper;
using Domain.Abstractions;
using Domain.DTOs.Responses;
using Domain.DTOs.Validators.Users;
using Domain.Errors;

namespace Application.Services
{
    public class UserService(
        IUserRepository userRepository, 
        IMapper mapper) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<UserResponse>> Delete(string id)
        {
            if (!Guid.TryParse(id, null, out var userId))
                return Result<UserResponse>.Failure(UserError.InvalidPayload);

            var user = await _userRepository.GetById(userId);

            if (user is null)
                return Result<UserResponse>.Failure(UserError.NotFound);

            await _userRepository.Delete(user);

            return Result<UserResponse>.Success();
        }

        public async Task<Result<IEnumerable<UserResponse>>> GetUsers()
        {
            //TODO: restrict this service only to admin users

            var users = await _userRepository.GetUsers();
            return Result<IEnumerable<UserResponse>>
                .Success(
                    users
                    .Select(_mapper.Map<UserResponse>)
                    .ToList());
        }

        public async Task<Result<UserResponse>> GetById(string id)
        {
            if(!Guid.TryParse(id, null, out var userId))
                return Result<UserResponse>.Failure(UserError.InvalidPayload);

            var user = await _userRepository.GetById(userId);
            if(user is null)
                return Result<UserResponse>.Failure(UserError.NotFound);

            return Result<UserResponse>
                .Success(_mapper.Map<UserResponse>(user));
        }

        public async Task<Result<UserResponse>> Update(string id, UpdateUserValidator payload)
        {
            if (!Guid.TryParse(id, null, out var userId))
                return Result<UserResponse>.Failure(UserError.InvalidPayload);

            if (!payload.Validate())
                return Result<UserResponse>.Failure(UserError.InvalidPayload);

            var user = await _userRepository.GetById(userId);
            if (user is null)
                return Result<UserResponse>.Failure(UserError.NotFound);

            var userWithSameEmail = await _userRepository.GetByEmail(payload.Email);
            if (userWithSameEmail is not null &&
                userWithSameEmail.Id.ToString() != id)
                return Result<UserResponse>.Failure(UserError.EmailAlreadyUsed);

            user.Update(
                firstname: payload.Firstname,
                lastname: payload.Lastname,
                email: payload.Email);

            await _userRepository.Update(user);

            return Result<UserResponse>
                .Success(_mapper.Map<UserResponse>(user));
        }
    }
}
