using Rewards.Model.Rules;

namespace Rewards.Model
{
    /// <summary>
    /// Interface of repository that returns rewards
    /// </summary>
    public interface IRewardRuleRepository
    {
        /// <summary>
        /// Reads rewards
        /// </summary>
        /// <returns>List of current rewards</returns>
        IRewardRule[] GetRewardRules();
    }
}
