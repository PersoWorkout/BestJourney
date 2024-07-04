using Domain.Utils;

namespace Domain.Auth.Requests.Customers;

public class RegisterCustomerRequest
{
    public required string Firstname { get; set; }
    public required string Lastname { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string PasswordConfirmation { get; set; }
    public required string Phone {  get; set; }
}
