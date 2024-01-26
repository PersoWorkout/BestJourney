using Domain.Interfaces;

namespace Domain.Models
{
    public class User( 
        string firstname,
        string lastname, 
        string email, 
        string password) : IUser
    {
        public Guid Id { get; set; } = 
            Guid.NewGuid();
        public string Firstname { get; set; } = firstname;
        public string Lastname { get; set; } = lastname;
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public List<UserJourney> UsersJourneys { get; set; } = [];

        public void Update(string firstname, string lastname, string email, string password)
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Password = password;
            UpdatedAt = DateTime.Now;
        }
    }
}
