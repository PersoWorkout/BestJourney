using Domain.Users;

namespace Application.Users;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetCustomers();
    Task<IEnumerable<User>> GetSuppliers();
    Task<User> Create(User user);
    Task<User?> GetCustomerById(Guid id);
    Task<User?> GetSupplierById(Guid id);
    Task<User?> GetCustomerByEmail(string email);
    Task<User?> GetSupplierByEmail(string email);
    Task<User?> Update(User user);
    Task Delete(User user);
}
