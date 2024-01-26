using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Errors.Users
{
    public static class UserError
    {
        public static readonly Error InvalidPayload = new("Users.InvalidPayload", "The payload is invalid");
        public static readonly Error EmailAlreadyUsed = new("Users.EmailAlreadyUsed", "The email is already used");
        public static readonly Error NotFound = new("Users.NotFound", "The user was not found");
        public static readonly Error InvalidCredentials = new("Users.InvalidCredentials", "The email or password is invalid");
    }
}
