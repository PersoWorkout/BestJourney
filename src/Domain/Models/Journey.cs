using Domain.Interfaces;

namespace Domain.Models
{
    public class Journey: IJourney
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; }
        public List<UserJourney> UsersJourneys { get; set; }
    }
}
