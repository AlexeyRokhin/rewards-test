namespace Rewards.Data.Csv
{
    /// <summary>
    /// Csv connection parameters
    /// </summary>
    public class CsvConnectionOptions
    {
        public const string SectionName = "CsvConnection";

        public string Path { get; set; } = string.Empty;
    }
}
