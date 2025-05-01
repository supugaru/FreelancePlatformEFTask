// This project is a rare sighting of Supugaru actually leaving comments,
// instead of creating elder scriptures only she can understand

using FreelancePlatformEFTask.Entities;
using FreelancePlatformEFTask.Utilities;
using Microsoft.EntityFrameworkCore;

namespace FreelancePlatformEFTask;

class Program
{
    static void Main(string[] args)
    {
        #region Initialization and login
        
        Console.WriteLine("DebugLog: Welcome to Supu-soft!");
        ApplicationDbContext context = new ApplicationDbContext();
        Console.WriteLine("DebugLog: DbContext initialized...");
        DbInteractions dbAction = new DbInteractions();
        Console.WriteLine("DebugLog: DbInteractions initialized...");
        Bullshittery bullshittery = new Bullshittery();
        Console.WriteLine("DebugLog: Bullshittery initialized...");
        Console.WriteLine(bullshittery.Logo);
        User currentUser = new User();
        Console.WriteLine("Welcome! Are you a [N]ew or [E]xisting user?");
        while (true)        // Looped until input is valid
        {
            Console.Write("Option: ");
            string userInput = Console.ReadKey().Key.ToString().ToUpper();
            Console.WriteLine();
            switch (userInput)
            {
                case "N":
                    var user = currentUser.CreateFromConsole();
                    user.AddUserToDb(context);
                    currentUser = context.Users
                        .Include(u => u.Role)
                        .FirstOrDefault(u => u.Id == user.Id);
                    break;
                case "E":
                    Console.Write("Enter your Email: ");
                    string userEmail = Console.ReadLine();
                    Console.WriteLine();
                    currentUser = context.Users
                        .Include(u => u.Role)
                        .FirstOrDefault(u => u.Email == userEmail);
                    if (currentUser == null)
                    {
                        Console.WriteLine("User not found. Try again or sign up!");
                        continue;
                    }
                    break;
                default:
                    Console.WriteLine("\nInvalid input. Try again!");
                    continue;
            }
            break; 
        }
        Console.WriteLine($"\nWelcome, {currentUser.Name}!");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        
        #endregion
        
        #region Main
        Console.Clear();
        Console.WriteLine(bullshittery.Logo);
        Console.WriteLine($"Welcome to soUpWork, {currentUser.Name}!");
        Console.WriteLine("Type 'help' for a list of commands.");
        Console.WriteLine("Type 'exit' to exit the program.");
        Console.WriteLine();
        while (true) // Looped until 'exit' command is used
        {
            Console.Write("> ");
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "exit":
                    Console.WriteLine($"Goodbye, {currentUser.Name}!");
                    break;
                case "help":
                    Console.WriteLine(bullshittery.HelpM);
                    continue;
                case "me":
                    currentUser.AboutMe();
                    continue;
                case "chat":
                    Chat.StartChat(context, currentUser);
                    continue;
                case "finances":
                    Finances.StartFinances(context, currentUser);
                    continue;
                default:
                    continue;
            }
            break;
        }
        #endregion
    }
}