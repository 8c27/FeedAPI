using FeedAPI.Models;

namespace FeedAPI.VMs
{
    public class Feed
    {
        public long Id { get; set; }
        public DateTime? CreationTime { get; set; }
        public long? ClientId { get; set; }
        public string? ClientName { get; set; }
        public decimal? Quantity { get; set; }
        public string? Description { get; set; }
        public string? Machine { get; set; }
        public string? FeedNumber { get; set; }
        public bool? IsDeleted { get; set; }
        public long? StockId { get; set; }
        public string? StockName { get; set; }
        public bool? Status { get; set; }
        public decimal? Weight { get; set; }
        public string? Raw { get; set; }

        public List<Stock>? Stock { get; set; }
    }
}
