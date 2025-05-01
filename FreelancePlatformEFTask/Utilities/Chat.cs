using FreelancePlatformEFTask.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreelancePlatformEFTask.Utilities;

public class Chat
{
    public static void StartChat(ApplicationDbContext context, User currentUser)
    {
        Bullshittery bullshittery = new Bullshittery();
        while (true)
        {
            Console.Clear();
            Console.WriteLine(bullshittery.Logo);
            Console.WriteLine("Welcome to Chat Window!");
            Console.WriteLine("Type 'sendmessage' to... send message. Duh.");
            Console.WriteLine("Type 'refresh' or hit Enter to refresh chat.");
            Console.WriteLine("Type 'exit' to go back to main menu.");
            Console.WriteLine("Press any key to load messages...");
            Console.ReadKey();
            Console.WriteLine();
            
            var messages = context.Chat
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .OrderBy(m => m.Date)
                .ToList();
            
            foreach (var message in messages)
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
                case "sendmessage":
                    SendMessage(context, currentUser);
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

    public static void SendMessage(ApplicationDbContext context, User currentUser)
    {
        Console.Write("Enter your message: ");
        var text = Console.ReadLine();
        Console.Write("Enter Id of a recipient: ");
        var recieverId = int.TryParse(Console.ReadLine(), out int id) ? id : -1;
        User receiver = context.Users.FirstOrDefault(u => u.Id == recieverId);
        if (receiver == null)
            Console.WriteLine("User not found!");
        else
        {
            var message = new Message()
            {
                Sender = currentUser,
                SenderId = currentUser.Id,
                Receiver = receiver,
                ReceiverId = receiver.Id,
                Contents = text,
                Date = DateTime.Now
            };

            context.Chat.Add(message);
            context.SaveChanges();
            Console.WriteLine("DebugLog: Message sent!");
        }
    }
}