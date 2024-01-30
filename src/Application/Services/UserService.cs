using Application.Interfaces.Hasher;
using Application.Interfaces.Users;
using AutoMapper;
using Domain.Abstractions;
using Domain.DTOs.Responses;
using Domain.DTOs.Validators.Users;
using Domain.Errors.Users;
using Domain.Models;
using Domain.Utils;

namespace Application.Services
{
    public class UserService(
        IUserRepository userRepository, 
        IMapper mapper, 
        IHashService hasherService) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IHashService _hasherService = hasherService;

        public async Task<Result<UserResponse>> Create(CreateUserValidator payload)
        {
            if (!payload.Validate()) 
                return Result<UserResponse>.Failure(UserError.InvalidPayload);

            if (await _userRepository.GetByEmail(payload.Email) is not null)
                return Result<UserResponse>.Failure(UserError.EmailAlreadyUsed);
            
            var hashedPassword = _hasherService.Hash(payload.Password);
            if (string.IsNullOrEmpty(hashedPassword))
                return Result<UserResponse>.Failure(UserError.InvalidPayload);

            var user = _mapper.Map<User>(payload);
            user.Password = hashedPassword;

            await _userRepository.Create(user);

            return Result<UserResponse>.Success(
                _mapper.Map<UserResponse>(user));
        }

        public async Task<Result<UserResponse>> Delete(string id)
        {
            Guid userId = new();
            if (!Guid.TryParse(id, null, out userId))
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
                    .Select(user => _mapper.Map<UserResponse>(user))
                    .ToList());
        }

        public async Task<Result<UserResponse>> GetById(string id)
        {
            Guid userId = new Guid();
            if(!Guid.TryParse(id, null, out userId))
                return Result<UserResponse>.Failure(UserError.InvalidPayload);

            var user = await _userRepository.GetById(userId);
            if(user is null)
                return Result<UserResponse>.Failure(UserError.NotFound);

            return Result<UserResponse>
                .Success(_mapper.Map<UserResponse>(user));
        }

        public async Task<Result<string>> Login(LoginUserValidator payload)
        {
            if (!payload.Validate())
                return Result<string>.Failure(UserError.InvalidPayload);

            var user = await _userRepository.GetByEmail(payload.Email);
            if (user is null)
                return Result<string>.Failure(UserError.InvalidCredentials);

            if (!_hasherService.Verify(payload.Password, user.Password))
                return Result<string>.Failure(UserError.InvalidCredentials);

            return Result<string>.Success(user.Id.ToString());
        }

        public async Task<Result<UserResponse>> Update(string id, UpdateUserValidator payload)
        {
            Guid userId = new Guid();
            if (!Guid.TryParse(id, null, out userId))
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

            string hashedPassword = user.Password;
            if(payload.Password.IsValid() && 
                !_hasherService.Verify(payload.Password, user.Password))
            {
                hashedPassword = 
                    _hasherService.Hash(payload.Password);

                if(string.IsNullOrEmpty(hashedPassword))
                    return Result<UserResponse>.Failure(UserError.InvalidPayload);
            }

            user.Update(
                payload.Firstname,
                payload.Lastname,
                payload.Email,
                hashedPassword);

            await _userRepository.Update(user);

            return Result<UserResponse>
                .Success(_mapper.Map<UserResponse>(user));
        }
    }
}
