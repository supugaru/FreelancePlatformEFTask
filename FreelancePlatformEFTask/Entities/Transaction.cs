using System.ComponentModel.DataAnnotations;

namespace FreelancePlatformEFTask.Entities;

public class Transaction
{
    public int Id { get; set; }     // Key value
    public int SenderId { get; set; }
    public User Sender { get; set; }
    public int? ReceiverId { get; set; }
    public User? Receiver { get; set; }
    public decimal Amount { get; set; }
    public int TransactionTypeId { get; set; }
    public TransactionType TransactionType { get; set; }       // One to Many link
    public DateTime Date { get; set; }

    public override string ToString()
    {
        return $"Transaction #{Id}, {Date.ToShortDateString()}:\n" +
               $"Sender: {Sender.Name}, receiver: {Receiver.Name}\n" +
               $"Transaction type: {TransactionType.Name}, amount: ${Amount}\n";
    }
}

// Another class-specific tableee, I ain't creating separate file for this shit
public class TransactionType
{
    public int Id { get; set; }     // Key value
    [MaxLength(100)] public string Name { get; set; }
    
    public ICollection<Transaction> Transactions { get; set; }
}