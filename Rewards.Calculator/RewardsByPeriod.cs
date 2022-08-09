namespace Rewards.Calculator
{
    public class RewardsByPeriod
    {
        public string PeriodName { get; private set; }

        public decimal RewardPoints { get; private set; }

        public RewardsByPeriod(string periodName, decimal rewardPoints)
        {
            PeriodName = periodName;
            RewardPoints = rewardPoints;
        }
    }
}
