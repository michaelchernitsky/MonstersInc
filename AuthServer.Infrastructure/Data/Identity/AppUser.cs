using Microsoft.AspNetCore.Identity;
using System;

namespace AuthServer.Infrastructure.Data.Identity
{
    public class AppUser : IdentityUser
    {
        // Add additional profile data for application users by adding properties to this class
        public string Name { get; set; }
        public DateTime ScareStartDate { get; set; }
    }
}
