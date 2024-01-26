using Domain.Utils;

namespace Domain.DTOs.Validators
{
    public class UpdateUserValidator
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public bool Validate()
        {
            var test = Email.IsValidEmail();
            return Firstname.IsValid() ||
                Lastname.IsValid() ||
                Email.IsValidEmail() ||
                Password.IsValidPassword();
        }
    }
}
