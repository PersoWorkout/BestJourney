using Domain.Utils;

namespace Domain.Auth.Requests;

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }

    public bool Validate()
    {
        return Email.IsValidEmail() && Password.IsValidPassword();
    }
}
