namespace FeedAPI.VMs
{
    public class Feed
    {
        public int Id { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? ClientId { get; set; }
        public string? ClientName { get; set; }
        public string? ItemNumber { get; set; }
        public string? ItemName { get; set; }
        public string? Material { get; set; }
        public string? Size { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Pcs { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Raise { get; set; }
        public string? Class { get; set; }
        public string? Peel1 { get; set; }
        public string? Peel2 { get; set; }
        public string? Typing { get; set; }
        public string? Chamfer { get; set; }
        public string? Hole1 { get; set; }
        public string? Hole2 { get; set; }
        public string? Ditch { get; set; }
        public string? Taper { get; set; }
        public string? Ear { get; set; }
        public string? Special { get; set; }
        public string? Description { get; set; }
        public string? Machine { get; set; }
        public string? FeedNumber { get; set; }
        public string? Project { get; set; }
        public string? Mm { get; set; }
        public bool? IsDeleted { get; set; }
        public int? StockId { get; set; }
        public string? StockName { get; set; }
        public bool? Status { get; set; }
    }
}
