using Domain.Utils;

namespace Domain.Auth.Requests.Suppliers;

public class RegisterSupplierRequest
{
    public required string Firstname { get; set; }
    public required string Lastname { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string PasswordConfirmation { get; set; }
    public required string Phone { get; set; }
    public required string CompanyName { get; set; }
    public required string WebsiteName { get; set; }
    public required string WebsiteUrl { get; set; }

    public bool Validate()
    {
        return Firstname.IsValid() &&
            Lastname.IsValid() &&
            Email.IsValidEmail() &&
            ValidPasswords() &&
            CompanyName.IsValid() &&
            WebsiteName.IsValid() &&
            WebsiteUrl.IsValid();

    }

    private bool ValidPasswords()
    {
        return Password.IsValidPassword() &&
            Password == PasswordConfirmation;
    }
}
