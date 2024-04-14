using Microsoft.EntityFrameworkCore;

namespace APIWeb.Models
{
    public class Notification
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Email { get; set; }
        public string SourceId { get; set; }
        public string UserId { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}
