using System.ComponentModel.DataAnnotations;

namespace FreelancePlatformEFTask.Entities;

public class Review
{
    public int Id { get; set; }     // Key value
    public int SenderId { get; set; }
    public User Sender { get; set; }
    public int ReceiverId { get; set; }
    public User Receiver { get; set; }
    public decimal Rating { get; set; }
    [MaxLength(250)] public string Comment { get; set; }
    public DateTime Date { get; set; }

    public override string ToString()
    {
        return $"Review by {Sender.Name} on {Receiver.Name}, {Date.ToShortDateString()}:\n" +
               $"Rating: {Rating}, comment: {Comment}\n";
    }
}