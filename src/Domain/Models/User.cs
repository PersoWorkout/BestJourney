using Domain.Interfaces;
using Domain.Utils;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public string Password { get; set; } = password;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        [JsonIgnore]
        public List<Order> Orders { get; set; } = [];

        public void Update(
            string firstname = "", 
            string lastname = "", 
            string email = "", 
            string password = "")
        {
            if(firstname.IsValid()) Firstname = firstname;
            if(lastname.IsValid()) Lastname = lastname;
            if(email.IsValidEmail()) Email = email;
            if(password.IsValidPassword()) Password = password;
            UpdatedAt = DateTime.Now;
        }
    }
}
