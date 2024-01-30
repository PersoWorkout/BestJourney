using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Errors
{
    public class JourneyError
    {
        public static readonly Error InvalidPayload = new("Journey.InvalidPayload", "The payload is invalid");
        public static Error NotFound(string id) => new("Journey.NotFound", $"The journey with id = {id} was not found");
    }
}
