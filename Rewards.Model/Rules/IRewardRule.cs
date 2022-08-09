
namespace Rewards.Model.Rules
{
    /// <summary>
    /// Reward rule interface
    /// </summary>
    public interface IRewardRule
    {
        /// <summary>
        /// Calculates reward for transaction
        /// </summary>
        /// <param name="transaction">Transaction</param>
        /// <returns>Reward</returns>
        decimal CalculateRewardPoints(Transaction transaction);
    }
}
