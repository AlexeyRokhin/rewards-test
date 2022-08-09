namespace Rewards.Model.Rules
{
    /// <summary>
    /// Implements rule that returns non-zero reward if transaction amount is greater than limit.
    /// </summary>
    public class SimpleSpendOverRewardRule : IRewardRule
    {
        private readonly decimal _lowerValue;
        private readonly decimal _pointsPerUnit;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lowerValue">Lower transaction amount for rewards</param>
        /// <param name="pointsPerUnit">Amount of rewards per unit (USD, EURO, etc)</param>
        public SimpleSpendOverRewardRule(decimal lowerValue, decimal pointsPerUnit = 1)
        {
            _lowerValue = lowerValue;
            _pointsPerUnit = pointsPerUnit;
        }

        public decimal CalculateRewardPoints(Transaction transaction)
        {
            return transaction.Amount > _lowerValue
                ? (transaction.Amount - _lowerValue) * _pointsPerUnit
                : 0;
        }
    }
}
