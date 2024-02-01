using Domain.Utils;

namespace Domain.DTOs.Validators.Users
{
    public class ResetPasswordValidator
    {
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }

        public bool Validate()
        {
            return Password.IsValidPassword() &&
                Password == PasswordConfirmation;
        }
    }
}
