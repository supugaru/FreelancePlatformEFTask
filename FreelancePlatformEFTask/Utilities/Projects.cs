using FreelancePlatformEFTask.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreelancePlatformEFTask.Utilities;

public class Projects
{
    public static void StartProjects(ApplicationDbContext context, User currentUser)
    {
        Bullshittery bullshittery = new Bullshittery();
        while (true)
        {
            Console.Clear();
            Console.WriteLine(bullshittery.Logo);
            Console.WriteLine("Welcome to Projects Window!");
            if (currentUser.RoleId == 2)
                Console.WriteLine("Type 'apply' to apply for a project.");
            if (currentUser.RoleId == 3)
            {
                Console.WriteLine("Type 'create' to create a new project.");
                Console.WriteLine("Type 'applications' to check pending applications.");
            }
            Console.WriteLine("Type 'refresh' or hit Enter to refresh chat.");
            Console.WriteLine("Type 'exit' to go back to main menu.");
            Console.WriteLine("Press any key to load your transaction log...");
            Console.ReadKey();
            Console.WriteLine();

            if (currentUser.RoleId == 2)
            {
                var projects = context.Projects
                    .Include(p => p.Status)
                    .Include(p => p.Category)
                    .Include(p => p.Client)
                    .Include(p => p.Freelancer)
                    .Where(p => p.FreelancerId == null)
                    .OrderBy(p => p.Id)
                    .ToList();
            }
            else if (currentUser.RoleId == 3)
            {
                var projects = context.Projects
                    .Include(p => p.Status)
                    .Include(p => p.Category)
                    .Include(p => p.Client)
                    .Include(p => p.Freelancer)
                    .Where(p => p.ClientId == currentUser.Id)
                    .OrderBy(p => p.Id)
                    .ToList();
            }
            else
            {
                var projects = context.Projects
                    .Include(p => p.Status)
                    .Include(p => p.Category)
                    .Include(p => p.Client)
                    .Include(p => p.Freelancer)
                    .OrderBy(p => p.Id)
                    .ToList();
            }
            
            Console.Write("> ");
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "exit":
                    Console.WriteLine($"Goodbye, {currentUser.Name}!");
                    break;
                case "create":
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

    public static void CreateProject(ApplicationDbContext context, User currentUser)
    {
        Console.WriteLine("Enter project name: ");
        string name = Console.ReadLine();
        Console.WriteLine("Enter project description: ");
        string description = Console.ReadLine();
        Console.WriteLine("Categories:");
        Console.WriteLine("1. App Development");
        Console.WriteLine("2. Web Development");
        Console.WriteLine("3. Game Development");
        Console.WriteLine("4. Misc Development");
        Console.WriteLine("Enter category Id: ");
        int categoryId;
        int choice = int.TryParse(Console.ReadLine(), out int choiceNumber) ? choiceNumber : 4;
        if (choice >= 1 || choice <= 4)
        {
            categoryId = choice;
        }
        else
        {
            categoryId = 4;
        }
        decimal budget = decimal.TryParse(Console.ReadLine(), out decimal budgetOut) ? budgetOut : 0;
        var project = new Project()
        {
            Name = name,
            Description = description,
            Budget = budget,
            StatusId = 1,
            CategoryId = categoryId,
            ClientId = currentUser.Id,
        };
        
        context.Projects.Add(project);
        context.SaveChanges();
        Console.WriteLine("DebugLog: Project created!");
    }
}