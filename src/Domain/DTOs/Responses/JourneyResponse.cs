namespace Domain.DTOs.Responses
{
    public class JourneyResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
}
