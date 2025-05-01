using System.ComponentModel.DataAnnotations;

namespace FreelancePlatformEFTask.Entities;

public class Message
{
    public int Id { get; set; }     // Key value
    public int SenderId { get; set; }
    public User Sender { get; set; }
    public int ReceiverId { get; set; }
    public User Receiver { get; set; }
    [MaxLength(250)] public string Contents { get; set; }
    public DateTime Date { get; set; }

    public override string ToString()
    {
        return $"{Sender.Name} to {Receiver.Name} at {Date}:\n" +
               $"\"{Contents}\" \n";
    }
}