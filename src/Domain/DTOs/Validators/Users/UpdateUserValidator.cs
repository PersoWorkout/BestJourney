using Domain.Utils;

namespace Domain.DTOs.Validators.Users
{
    public class UpdateUserValidator
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }

        public bool Validate()
        {
            return Firstname.IsValid() ||
                Lastname.IsValid() ||
                Email.IsValidEmail();
        }
    }
}
