namespace Rewards.Calculator
{
    /// <summary>
    /// Result of report
    /// </summary>
    public class RewardByCustomerAndMonthResult
    {
        public int CustomerId { get; private set; }

        public RewardByCustomerAndMonthResult(int customerId, IReadOnlyList<RewardsByPeriod> periods)
        {
            CustomerId = customerId;
            Periods = periods;
        }

        /// <summary>
        /// Rewards by month and total.
        /// </summary>
        public IReadOnlyList<RewardsByPeriod> Periods { get; private set; }
    }
}
