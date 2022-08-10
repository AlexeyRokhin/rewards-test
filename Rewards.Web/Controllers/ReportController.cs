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
        private readonly ILogger<ReportController> _logger;

        public ReportController(
            RewardsByCustomerAndMonthCalculator rewardsCalculator,
            ITransactionRepository transactionRepository,
            ILogger<ReportController> logger)
        {
            _rewardsCalculator = rewardsCalculator;
            _transactionRepository = transactionRepository;
            _logger = logger;
        }

        [HttpPost()]
        [Route("ByCustomerAndMonth")]
        [ProducesResponseType(typeof(RewardByCustomerAndMonthResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ByCustomerAndMonth(ByCustomerAndMonthParameters parameters)
        {
            try
            {
                var startDate = parameters.StartDate;
                var endDate = startDate.AddMonths(3);
                var transactions = _transactionRepository
                    .GetTransactions()
                    .Where(tr => tr.CreatedAt >= startDate && tr.CreatedAt < endDate);
                return Ok(_rewardsCalculator.GetReport(transactions));
            }
            catch (Exception excp)
            {
                _logger.LogError(excp, "Error while creating a report.");
                return base.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }

    public class ByCustomerAndMonthParameters
    {
        public DateTime StartDate { get; set; }
    }
}
