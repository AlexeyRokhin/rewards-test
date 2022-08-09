using Moq;
using Rewards.Model;
using Rewards.Model.Rules;

namespace Rewards.Calculator.Tests
{
    public class RewardsCalculatorTests
    {
        private const int _monthCount = 3;
        private const int _k1 = 1;
        private const int _k2 = 2;

        private readonly RewardsByCustomerAndMonthCalculator _calculator;
        private readonly Mock<IRewardRule>[] _ruleMocks;
        private readonly Transaction[] _transactions;

        public RewardsCalculatorTests()
        {
            var ruleMock1 = new Mock<IRewardRule>();
            ruleMock1
                .Setup(rule => rule.CalculateRewardPoints(It.IsAny<Transaction>()))
                .Returns((Transaction tr) => _k1 * tr.Amount);
            var ruleMock2 = new Mock<IRewardRule>();
            ruleMock2
                .Setup(rule => rule.CalculateRewardPoints(It.IsAny<Transaction>()))
                .Returns((Transaction tr) => _k2 * tr.Amount);
            _ruleMocks = new[] { ruleMock1, ruleMock2 };
            _calculator = new RewardsByCustomerAndMonthCalculator(_ruleMocks.Select(m => m.Object).ToArray());

            _transactions = GenerateCustomerTransactions(1)
                .Concat(GenerateCustomerTransactions(2))
                .ToArray();
        }

        [Fact]
        public void ShouldInvokeEachRuleForEachTransaction()
        {
            _calculator.GetReport(_transactions);

            Assert.All(_ruleMocks, mock => mock.Verify(m => m.CalculateRewardPoints(It.IsAny<Transaction>()), Times.Exactly(_transactions.Length)));

        }

        [Fact]
        public void ShouldCalculateByMonthAndTotalsForEachCustomer()
        {
            var results = _calculator.GetReport(_transactions);

            Assert.Equal(2, results.Count());
            Assert.All(results, result => Assert.Equal(_monthCount + 1, result.Periods.Count()));
        }

        [Fact]
        public void ShouldCalculateCorrectly()
        {
            var results = _calculator.GetReport(_transactions);

            Enumerable
                .Range(0, results.Count)
                .ToList()
                .ForEach(i =>
                {
                    var trs = _transactions[(i * _monthCount)..((i + 1) * _monthCount)];
                    var total = results[i].Periods[^1];
                    Assert.Equal(total.RewardPoints, trs.Sum(tr => tr.Amount) * (_k1 + _k2));
                });
        }

        private static IEnumerable<Transaction> GenerateCustomerTransactions(int customerId)
        {
            return Enumerable
                .Range(1, _monthCount)
                .Select(num => new Transaction
                {
                    CustomerId = customerId,
                    CreatedAt = new DateTime(2022, num, 1),
                    Amount = Random.Shared.Next(1, 10)
                });
        }
    }
}
