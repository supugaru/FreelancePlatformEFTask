using System.ComponentModel.DataAnnotations;

namespace FreelancePlatformEFTask.Entities;

public class User
{
    public int Id { get; set; }     // Key value
    [MaxLength(100)] public string Name { get; set; } 
    [MaxLength(100)] public string Email { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }      // One to Many link
    public decimal Balance { get; set; }
    public decimal Rating { get; set; }
    
    public ICollection<Message> SentMessages { get; set; } = new List<Message>();
    public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
    public ICollection<Review> SentReviews { get; set; } = new List<Review>();
    public ICollection<Review> ReceivedReviews { get; set; } = new List<Review>();
    public ICollection<Transaction> TransactionsSent { get; set; } = new List<Transaction>();
    public ICollection<Transaction> TransactionsReceived { get; set; } = new List<Transaction>();

    public User() {}
    
    public User CreateFromConsole()
    {
        var user = new User();
        
        Console.Write("Enter full name of the account: ");
        user.Name = Console.ReadLine();
        Console.Write("Enter Email of the account: ");
        user.Email = Console.ReadLine();
        Console.WriteLine("Are you [F]reelancer or [C]lient?");
        while (true)
        {
            Console.Write("Option: ");
            string userInput = Console.ReadKey().Key.ToString().ToUpper();
            Console.WriteLine();
            switch (userInput)
            {
                case "F":
                    user.RoleId = 2;
                    break;
                case "C":
                    user.RoleId = 3;
                    break;
                default:
                    Console.WriteLine("\nInvalid input. Try again!");
                    continue;
            }
            break;
        }

        Balance = 0;
        Rating = 0;
        
        return user;
    }

    public override string ToString()
    {
        return $"User ${Id}, {Name}:\n" +
               $"{Name}'s role: {Role}\n" +
               $"{Name}'s rating: {Rating}\n";
    }

    public void AboutMe()
    {
        Console.WriteLine($"User #{Id}, {Name}:\n" +
                          $"Your role: {Role}\n" +
                          $"Your rating: {Rating}\n" +
                          $"Your balance: ${Balance}\n" +
                          $"Your Email: {Email}\n");
    }

    public void AddUserToDb(ApplicationDbContext context)
    {
        context.Users.Add(this);
        context.SaveChanges();
        Console.WriteLine($"DebugLog: Added user #{Id}!");
    }
}

// Only needed for User class, so I'm leaving it here
public class Role
{
    public int Id { get; set; }     // Key value
    [MaxLength(100)] public string Name { get; set; }
    
    public ICollection<User> Users { get; set; } = new List<User>();

    public override string ToString()
    {
        return Name;
    }
}