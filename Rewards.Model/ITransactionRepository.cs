namespace Rewards.Model
{
    /// <summary>
    /// Interface of repository that returns transactions
    /// </summary>
    public interface ITransactionRepository
    {
        /// <summary>
        /// Reads transactions
        /// </summary>
        /// <returns>Quariable list of transactions</returns>
        IQueryable<Transaction> GetTransactions();
    }
}
