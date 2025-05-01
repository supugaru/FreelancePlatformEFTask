using FreelancePlatformEFTask.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreelancePlatformEFTask.Utilities;

public class Finances
{
    public static void StartFinances(ApplicationDbContext context, User currentUser)
    {
        Bullshittery bullshittery = new Bullshittery();
        while (true)
        {
            Console.Clear();
            Console.WriteLine(bullshittery.Logo);
            Console.WriteLine("Welcome to Finances Window!");
            Console.WriteLine("Type 'deposit' to deposit money.");
            Console.WriteLine("Type 'withdraw' to withdraw money.");
            Console.WriteLine("Type 'transfer' to transfer money.");
            Console.WriteLine("Type 'refresh' or hit Enter to refresh chat.");
            Console.WriteLine("Type 'exit' to go back to main menu.");
            Console.WriteLine("Press any key to load your transaction log...");
            Console.ReadKey();
            Console.WriteLine();
            
            var transactions = context.Transactions
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .Include(m => m.TransactionType)
                .Where(m => m.Sender == currentUser || m.Receiver == currentUser)
                .OrderBy(m => m.Date)
                .ToList();
            
            foreach (var message in transactions)
            {
                Console.WriteLine(message.ToString());
            }
            
            Console.Write("> ");
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "exit":
                    Console.WriteLine($"Goodbye, {currentUser.Name}!");
                    break;
                case "transfer":
                    Transfer(context, currentUser);
                    continue;
                case "deposit":
                    Deposit(context, currentUser);
                    continue;
                case "withdraw":
                    Withdraw(context, currentUser);
                    continue;
                default:
                    continue;
            }
            break;
        }
        Console.Clear();
        Console.WriteLine(bullshittery.Logo);
        Console.WriteLine($"Welcome to soUpWork, {currentUser.Name}!");
        Console.WriteLine("Type 'help' for a list of commands.");
        Console.WriteLine("Type 'exit' to exit the program.");
        Console.WriteLine();
    }

    public static void Transfer(ApplicationDbContext context, User currentUser)
    {
        Console.Write("Enter amount: ");
        int amount = int.TryParse(Console.ReadLine(), out amount) ? amount : 0;
        Console.Write("Enter Id of a recipient: ");
        var recieverId = int.TryParse(Console.ReadLine(), out int id) ? id : -1;
        User receiver = context.Users.FirstOrDefault(u => u.Id == recieverId);
        if (receiver == null)
            Console.WriteLine("User not found!");
        else if (amount < 1 || amount > currentUser.Balance)
        {
            Console.WriteLine("Invalid amount!");
        }
        else
        {
            var transaction = new Transaction()
            {
                SenderId = currentUser.Id,
                Sender = currentUser,
                ReceiverId = receiver.Id,
                Receiver = receiver,
                Amount = amount,
                TransactionTypeId = 3,
                Date = DateTime.Now,
            };
            
            currentUser.Balance -= amount;
            receiver.Balance += amount;

            context.Transactions.Add(transaction);
            context.SaveChanges();
            Console.WriteLine("DebugLog: Transfer successful!");
        }
    }

    public static void Deposit(ApplicationDbContext context, User currentUser)
    {
        Console.Write("Enter amount: ");
        int amount = int.TryParse(Console.ReadLine(), out amount) ? amount : 0;
        Console.Write("Enter your PayPal Email: ");
        Console.ReadLine();     // trollface.jpeg

        if (amount == 0)
        {
            Console.WriteLine("Invalid amount!");
        }
        else
        {
            var transaction = new Transaction()
            {
                SenderId = currentUser.Id,
                Sender = currentUser,
                Amount = amount,
                TransactionTypeId = 1,
                Date = DateTime.Now,
            };
        
            currentUser.Balance += amount;
        
            context.Transactions.Add(transaction);
            context.SaveChanges();
            Console.WriteLine("DebugLog: Deposit successful!");
        }
    }
    
    public static void Withdraw(ApplicationDbContext context, User currentUser)
    {
        Console.Write("Enter amount: ");
        int amount = int.TryParse(Console.ReadLine(), out amount) ? amount : 0;
        Console.Write("Enter your PayPal Email: ");
        Console.ReadLine();     // trollface.jpeg #2
        
        if (amount == 0)
        {
            Console.WriteLine("Invalid amount!");
        }
        else
        {
            var transaction = new Transaction()
            {
                SenderId = currentUser.Id,
                Sender = currentUser,
                Amount = amount,
                TransactionTypeId = 1,
                Date = DateTime.Now,
            };
        
            currentUser.Balance -= amount;
        
            context.Transactions.Add(transaction);
            context.SaveChanges();
            Console.WriteLine("DebugLog: Withdrawal successful!");
        }

    }
}