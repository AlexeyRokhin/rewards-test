using Microsoft.Extensions.Logging;
using Rewards.Model;

namespace Rewards.Data.Csv
{
    /// <summary>
    /// Transaction repository that reads transactions from csv file
    /// </summary>
    public class CsvTransactionRepository : ITransactionRepository
    {
        private readonly string _filePath;
        private readonly ILogger<CsvTransactionRepository> _logger;

        public CsvTransactionRepository(CsvConnectionOptions options, ILogger<CsvTransactionRepository> logger)
        {
            _logger = logger;
            _filePath = Path.Combine(options.Path, "transactions.csv");
        }

        public IQueryable<Transaction> GetTransactions()
        {
            return Enumerate().AsQueryable();
        }

        private IEnumerable<Transaction> Enumerate()
        {
            const int fieldCount = 3;
            foreach (var line in File.ReadLines(_filePath))
            {
                var parts = line.Split(',');
                if (parts.Length != fieldCount)
                {
                    _logger.LogWarning($"Line should contain exactly {fieldCount} fields: ${line}");
                    continue;
                }
                if (!int.TryParse(parts[0], out var customerId))
                {
                    _logger.LogWarning($"Customer Id should be integer: ${parts[0]}");
                    continue;
                }
                if (!DateTime.TryParse(parts[1], out var createdAt))
                {
                    _logger.LogWarning($"Can't parse transacrion date: ${parts[1]}");
                    continue;
                }
                if (!decimal.TryParse(parts[2], out var amount))
                {
                    _logger.LogWarning($"Can't parse transacrion date: ${parts[1]}");
                    continue;
                }
                yield return new Transaction { CustomerId = customerId, CreatedAt = createdAt, Amount = amount };
            }
        }
    }
}
