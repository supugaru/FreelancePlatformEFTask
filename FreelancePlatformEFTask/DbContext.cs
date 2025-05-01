using FreelancePlatformEFTask.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FreelancePlatformEFTask;

public class ApplicationDbContext : DbContext
{
    // Config override for pulling connection string from appsettings.json and using it to connect to Db
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        string connStr = configuration.GetConnectionString("FreelancePlatformDb");
        // If "FreelancePlatformDb" database doesn't exist — migration will create it, so we're fine
        
        optionsBuilder.UseSqlServer(connStr);
        Console.WriteLine("DebugLog: Connected!");
    }

    // Creating utility tables
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // This prevents SQL from bitching about cascades
        // For Message
        modelBuilder.Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany(u => u.SentMessages)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Cascade); // Deletes all sent messages if sender was deleted
        
        modelBuilder.Entity<Message>()
            .HasOne(m => m.Receiver)
            .WithMany(u => u.ReceivedMessages)
            .HasForeignKey(n => n.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict); // Restricts deletion if receiver was deleted ig
        
        // For Project
        modelBuilder.Entity<Project>()
            .HasOne(m => m.Client)
            .WithMany()
            .HasForeignKey(m => m.ClientId)
            .OnDelete(DeleteBehavior.Cascade); // Deletes all projects if client was deleted
        
        modelBuilder.Entity<Project>()
            .HasOne(m => m.Freelancer)
            .WithMany()
            .HasForeignKey(n => n.FreelancerId)
            .OnDelete(DeleteBehavior.Restrict); // Restricts deletion 
        
        modelBuilder.Entity<Project>()
            .HasOne(m => m.Category)
            .WithMany()
            .HasForeignKey(n => n.CategoryId)
            .OnDelete(DeleteBehavior.Restrict); // Restricts deletion 
        
        modelBuilder.Entity<Project>()
            .HasOne(m => m.Status)
            .WithMany()
            .HasForeignKey(n => n.StatusId)
            .OnDelete(DeleteBehavior.Restrict); // Restricts deletion 
        
        // For User
        modelBuilder.Entity<User>()
            .HasOne(m => m.Role)
            .WithMany(u => u.Users)
            .HasForeignKey(n => n.RoleId)
            .OnDelete(DeleteBehavior.Restrict); // Restricts deletion 
        
        // For Transaction
        modelBuilder.Entity<Transaction>()
            .HasOne(m => m.Sender)
            .WithMany(u => u.TransactionsSent)
            .HasForeignKey(n => n.SenderId)
            .OnDelete(DeleteBehavior.Restrict); // Restricts deletion
        
        modelBuilder.Entity<Transaction>()
            .HasOne(m => m.TransactionType)
            .WithMany(u => u.Transactions)
            .HasForeignKey(n => n.TransactionTypeId)
            .OnDelete(DeleteBehavior.Restrict); // Restricts deletion 
        
        modelBuilder.Entity<Transaction>()
            .HasOne(m => m.Receiver)
            .WithMany(u => u.TransactionsReceived)
            .HasForeignKey(n => n.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict); // Restricts deletion
        
        // For Review
        modelBuilder.Entity<Review>()
            .HasOne(m => m.Sender)
            .WithMany(u => u.SentReviews)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Cascade); // Deletes all reviews if sender was deleted
        
        modelBuilder.Entity<Review>()
            .HasOne(m => m.Receiver)
            .WithMany(u => u.ReceivedReviews)
            .HasForeignKey(n => n.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict); // Restricts deletion
        
        // For Application
        modelBuilder.Entity<Application>()
            .HasOne(m => m.Freelancer)
            .WithMany()
            .HasForeignKey(m => m.FreelancerId)
            .OnDelete(DeleteBehavior.Cascade); // Deletes all applications if freelancer was deleted
        
        modelBuilder.Entity<Application>()
            .HasOne(m => m.Project)
            .WithMany()
            .HasForeignKey(m => m.ProjectId)
            .OnDelete(DeleteBehavior.Restrict); // Restricts deletion
        
        // Utility table for User
        modelBuilder.Entity<Role>().HasData(
            new Role
            {
                Id = 1,
                Name = "Admin",
            },
            
            new Role
            {
                Id = 2,
                Name = "Freelancer",
            },
            
            new Role
            {
                Id = 3,
                Name = "Client",
            });
        
        // Utility tables for Project
        modelBuilder.Entity<Status>().HasData(
            new Status
            {
                Id = 1,
                Name = "Available",
            },
            
            new Status
            {
                Id = 2,
                Name = "In progress",
            },
            
            new Status
            {
                Id = 3,
                Name = "Finished",
            });
        
        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "App Development",
            },
            
            new Category
            {
                Id = 2,
                Name = "Web Development",
            },
            
            new Category
            {
                Id = 3,
                Name = "Game Development",
            },
            
            new Category
            {
                Id = 4,
                Name = "Misc. Development",
            });
        
        // Utility table for Transaction
        modelBuilder.Entity<TransactionType>().HasData(
            new TransactionType
            {
                Id = 1,
                Name = "Deposit",
            },
            
            new TransactionType
            {
                Id = 2,
                Name = "Withdrawal",
            },
            
            new TransactionType
            {
                Id = 3,
                Name = "Transfer",
            });
        // No more utility tables THANK GOD
    }

    // DbSets start here
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    
    public DbSet<Project> Projects { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<Application> Applications { get; set; }
    
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionType> TransactionTypes { get; set; }
    
    public DbSet<Review> Reviews { get; set; }
    
    public DbSet<Message> Chat { get; set; }
    // No more DbSets for you
}