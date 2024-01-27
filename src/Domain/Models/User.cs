using Domain.Interfaces;
using Domain.Utils;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class User( 
        string firstname,
        string lastname, 
        string email, 
        string password) : IUser
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Firstname { get; set; } = firstname;
        public string Lastname { get; set; } = lastname;
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public List<UserJourney> UsersJourneys { get; set; } = [];

        public void Update(string firstname, string lastname, string email, string password)
        {
            if(firstname.IsValid()) Firstname = firstname;
            if(lastname.IsValid()) Lastname = lastname;
            if(email.IsValidEmail()) Email = email;
            if(password.IsValidPassword()) Password = password;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
