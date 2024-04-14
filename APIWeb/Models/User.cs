using Microsoft.AspNetCore.Identity;

namespace APIWeb.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Phone { get; set; }
        public string Room { get; set; }

        public IdentityUser IdentityUser { get; set; }
    }
}
