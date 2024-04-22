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
    public DbSet<EntityService> EntityServices { get; set; } // تأكد من تعريف هذا بشكل صحيح

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<EntityService>().HasKey(x => x.Id);
        modelBuilder.Entity<EntityService>().HasOne<Service>().WithMany().HasForeignKey(x => x.ServiceId); // تعريف العلاقة
    }

    public DbSet<Notification> Notifications { get; set; }
    public DbSet<RegistrationRequest> RegistrationRequests { get; set; }
    



}