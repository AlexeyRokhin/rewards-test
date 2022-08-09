namespace Rewards.Model
{
    /// <summary>
    /// Basic transaction details
    /// </summary>
    public class Transaction
    {
        public int CustomerId { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public decimal Amount { get; set; }
    }
}
