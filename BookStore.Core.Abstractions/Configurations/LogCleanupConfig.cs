
namespace BookStore.Core.Abstractions.Configurations
{
    public class LogCleanupConfig
    {
        public int DaysToKeepLogs { get; set; }
        public int CleanupIntervalMinutes { get; set; }
    }
}
