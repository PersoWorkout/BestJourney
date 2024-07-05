using Domain.Abstractions;
using Domain.Users;
using Domain.Users.Requests;

namespace Application.Users.Customers;

public interface ICustomerService
{
    Task<Result<IEnumerable<User>>> GetAll();
    Task<Result<User>> GetById(string id);
    Task<Result<User>> Update(string id, UpdateCustomerRequest paylaod);
    Task<Result<object>> Delete(string id);
}
