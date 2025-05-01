namespace FreelancePlatformEFTask.Entities;

public class Application
{
    public int Id { get; set; }     // Key value
    public int FreelancerId { get; set; }
    public User Freelancer { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; }
    public decimal AskingPrice { get; set; }
    public DateTime Date { get; set; }
    
    public override string ToString()
    {
        return $"Application #{Id}, {Date.ToShortDateString()}:\n" +
               $"{Freelancer.Name} is ready to work on {Project.Name}\n" +
               $"They are asking for a price of ${AskingPrice}.\n";
    }
}