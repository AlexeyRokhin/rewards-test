using Rewards.Model;
using Rewards.Model.Rules;

namespace Rewards.Calculator
{
    /// <summary>
    /// Implements report by customer and month.
    /// </summary>
    public class RewardsByCustomerAndMonthCalculator
    {
        private readonly IRewardRule[] _rewardRules;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rewardRules">List of rewards rules</param>
        public RewardsByCustomerAndMonthCalculator(IRewardRule[] rewardRules)
        {
            _rewardRules = rewardRules;
        }

        public IReadOnlyList<RewardByCustomerAndMonthResult> GetReport(IEnumerable<Transaction> transactions)
        {
            return transactions
                .GroupBy(t => t.CustomerId)
                .Select(groupByCustomer => GetReportResultForCustomer(groupByCustomer.Key, groupByCustomer))
                .ToList()
                .AsReadOnly();
        }

        private RewardByCustomerAndMonthResult GetReportResultForCustomer(int customerId, IEnumerable<Transaction> transactions)
        {
            var periods = transactions
                // by year and month
                .GroupBy(t => t.CreatedAt.ToString("yyyy-MM"))
                .Select(groupByMonth => new RewardsByPeriod(
                    groupByMonth.Key,
                    // for each transaction calculate rewards from all rules and sum rewards across transactions
                    groupByMonth.Sum(
                        // sum rewards from each rule for each transaction
                        t => _rewardRules.Sum(rule => rule.CalculateRewardPoints(t)))
                    )
                ).ToList();
            // add total
            var total = new RewardsByPeriod("Total", periods.Sum(period => period.RewardPoints));
            periods.Add(total);
            return new RewardByCustomerAndMonthResult(customerId, periods.AsReadOnly());
        }
    }
}
