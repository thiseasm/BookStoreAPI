namespace BookStore.Infrastructure.DTOs
{
    public abstract class AuditLogDto
    {
        public required DateTime Timestamp { get; set; }
        public required string Action { get; set; }
        public required int EntityId { get; set; }
        public required string PreviousState { get; set; }
        public required string NextState { get; set; }
    }
}
