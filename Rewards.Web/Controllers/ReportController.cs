using Microsoft.AspNetCore.Mvc;
using Rewards.Calculator;
using Rewards.Model;

namespace Rewards.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly RewardsByCustomerAndMonthCalculator _rewardsCalculator;
        private readonly ITransactionRepository _transactionRepository;

        public ReportController(RewardsByCustomerAndMonthCalculator rewardsCalculator, ITransactionRepository transactionRepository)
        {
            _rewardsCalculator = rewardsCalculator;
            _transactionRepository = transactionRepository;
        }

        [HttpPost()]
        [Route("ByCustomerAndMonth")]
        public IEnumerable<RewardByCustomerAndMonthResult> ByCustomerAndMonth(ByCustomerAndMonthParameters parameters)
        {
            var startDate = parameters.StartDate;
            var endDate = startDate.AddMonths(3);
            var transactions = _transactionRepository
                .GetTransactions()
                .Where(tr => tr.CreatedAt >= startDate && tr.CreatedAt < endDate);
            return _rewardsCalculator.GetReport(transactions);
        }
    }

    public class ByCustomerAndMonthParameters
    {
        public DateTime StartDate { get; set; }
    }
}
