
using APIWeb.Models;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<APIWeb.Models.RegistrationRequest> RegisterRequests { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }

    public DbSet<APIWeb.Models.Request> Requests { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Room> Rooms { get; set; }

    public DbSet<Participation> Participations { get; set; }
    public DbSet<MyDocument> MyDocuments { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<EntityService> EntityServices { get; set; }
    public DbSet<Notification> Notifications { get; set; }
   

}