using Domain.Abstractions;
using Domain.Users;
using Domain.Users.Requests;

namespace Application.Users.Customers;

public class CustomerService(IUserRepository repository) : ICustomerService
{
    private readonly IUserRepository _repository = repository;

    public async Task<Result<IEnumerable<User>>> GetAll()
    {
        return Result<IEnumerable<User>>.Success(
            await _repository.GetCustomers());
    }

    public async Task<Result<User>> GetById(string id)
    {
        if (!Guid.TryParse(id, out var userId))
            return Result<User>.Failure(
                UserError.NotFound);

        var user = await _repository.GetCustomerById(userId);
        if (user is null)
            return Result<User>.Failure(
                UserError.NotFound);

        return Result<User>.Success(user);
    }

    public async Task<Result<User>> Update(string id, UpdateUserRequest paylaod)
    {
        if (!paylaod.Validate())
            return Result<User>.Failure(
                UserError.InvalidPayload);

        if (!Guid.TryParse(id, out var userId))
            return Result<User>.Failure(
                UserError.NotFound);

        var user = await _repository.GetCustomerById(userId);
        if (user is null)
            return Result<User>.Failure(
                UserError.NotFound);

        user.UpdateCustomer(
            firstname: paylaod.Firstname,
            lastname: paylaod.Lastname,
            email: paylaod.Email,
            password: paylaod.Password,
            phone: paylaod.Phone);

        var result = await _repository.Update(user);

        return Result<User>.Success(result);
    }

    public async Task<Result<object>> Delete(string id)
    {
        if (!Guid.TryParse(id, out var userId))
            return Result<object>.Failure(
                UserError.NotFound);

        var user = await _repository.GetCustomerById(userId);
        if (user is null)
            return Result<object>.Failure(
                UserError.NotFound);

        await _repository.Delete(user);

        return Result<object>.Success();
    }
}
