using Rewards.Model.Rules;

namespace Rewards.Model.Tests.Rules
{
    public class SimpleSpendOverRewardRuleTests
    {
        private const decimal _lowerValue = 30;
        private const decimal _pointsPerUnit = 2;

        private readonly SimpleSpendOverRewardRule _rule;


        public SimpleSpendOverRewardRuleTests()
        {
            _rule = new SimpleSpendOverRewardRule(_lowerValue, _pointsPerUnit);
        }

        [Theory]
        [InlineData(-100, 0)]
        [InlineData(0, 0)]
        [InlineData(30, 0)]
        [InlineData(31, 2)]
        [InlineData(40, 20)]
        public void ShouldCalculateCorrectly(decimal amount, decimal expectedReward)
        {
            var reward = _rule.CalculateRewardPoints(new Transaction { Amount = amount });

            Assert.Equal(expectedReward, reward);
        }
    }
}
