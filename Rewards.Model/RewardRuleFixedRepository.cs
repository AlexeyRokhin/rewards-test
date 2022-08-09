using Rewards.Model.Rules;

namespace Rewards.Model
{
    /// <summary>
    /// Returns fixed list of rules.
    /// In real application the implementation can read rules from database.
    /// </summary>
    public class RewardRuleFixedRepository : IRewardRuleRepository
    {
        public IRewardRule[] GetRewardRules()
        {
            return new[] { new SimpleSpendOverRewardRule(50, 1), new SimpleSpendOverRewardRule(100, 1) };
        }
    }
}
