namespace FreelancePlatformEFTask.Entities;

public class Transaction
{
    public int Id { get; set; }     // Key value
    public User User { get; set; }
    public decimal Amount { get; set; }
    
}

// Another class-specific tableee, I ain't creating separate file for this shit
public class Type
{
    public int Id { get; set; }     // Key value
    public string Name { get; set; }
}