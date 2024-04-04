using System.ComponentModel.DataAnnotations;

namespace APIWeb.Models
{
    public class RegistrationRequest
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Room { get; set; }
        public string Status { get; set; } = "Pending";
    }
}