using Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Validators.Journeys
{
    public class UpdateJourneyValidator
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = false;

        public bool Validate()
        {
            return Name.IsValid() ||
                Description.IsValid() ||
                Country.IsValid() ||
                City.IsValid() ||
                Price > 0m;
        }
    }
}
