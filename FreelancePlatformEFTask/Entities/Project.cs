using System.ComponentModel.DataAnnotations;

namespace FreelancePlatformEFTask.Entities;

public class Project
{
    public int Id { get; set; }     // Key value
    [MaxLength(100)] public string Name { get; set; }
    [MaxLength(250)] public string Description { get; set; }
    public decimal Budget { get; set; }
    public int StatusId { get; set; }
    public Status Status { get; set; }      // One to Many link
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int ClientId { get; set; }
    public User Client { get; set; }
    public int? FreelancerId { get; set; }
    public User? Freelancer { get; set; }
    
    public List<Application> Applications { get; set; }

    public override string ToString()
    {
        return $"Project #{Id}, \"{Name}\":\n" +
               $"Description: {Description}\n" +
               $"Budget: ${Budget}\n" +
               $"Status: {Status}\n" +
               $"Category: {Category}\n" +
               $"Commissioned by: {Client.Name}\n" +
               $"Taken by: {Freelancer.Name}\n";
    }
}

// Once again, only used in Project class, so I'm leaving it here
public class Status
{
    public int Id { get; set; }     // Key value
    [MaxLength(100)] public string Name { get; set; }

    public override string ToString()
    {
        return Name;
    }
}

public class Category
{
    public int Id { get; set; }     // Key value
    [MaxLength(100)] public string Name { get; set; }

    public override string ToString()
    {
        return Name;
    }
}