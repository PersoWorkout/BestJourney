using Domain.Utils;

namespace Domain.Auth.Requests;

public class ResetPasswordRequest
{
    public string Password { get; set; }
    public string PasswordConfirmation { get; set; }

    public bool Validate()
    {
        return Password.IsValidPassword() &&
            Password == PasswordConfirmation;
    }
}
