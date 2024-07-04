using Domain.Utils;

namespace Domain.Auth.Requests.Customers;

public class RegisterCustomerRequest
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordConfirmation { get; set; }

    public bool Validate()
    {
        return Firstname.IsValid() &&
            Lastname.IsValid() &&
            Email.IsValidEmail() &&
            ValidPasswords();

    }

    private bool ValidPasswords()
    {
        return Password.IsValidPassword() &&
            Password == PasswordConfirmation;
    }
}
