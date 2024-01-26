using Domain.Utils;

namespace Domain.DTOs.Validators
{
    public class LoginUserValidator
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public bool Validate()
        {
            return Email.IsValidEmail() && Password.IsValidPassword();
        }
    }
}
