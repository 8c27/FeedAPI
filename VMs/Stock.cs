using FeedAPI.Models;
using System.Runtime.CompilerServices;

namespace FeedAPI.VMs
{
    public class Stock
    {
        public long Id { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string StockName { get; set; }
        public int? FinishAmount { get; set; }
        public decimal? Weight { get; set; }
        public bool? IsDeleted { get; set; }
        public long? ClientId { get; set; }
        public string? ClientName { get; set; }
        public string? Material { get; set; }
        public string? Size { get; set; }
        public decimal? Pcs { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Raise { get; set; }
        public string? Class { get; set; }
        public string? Peel1 { get; set; }
        public string? Peel2 { get; set; }
        public string? Typeing { get; set; }
        public string? Chamfer { get; set; }
        public string? Hole1 { get; set; }
        public string? Hole2 { get; set; }
        public string? Ditch { get; set; }
        public string? Taper { get; set; }
        public string? Ear { get; set; }
        public string? Special { get; set; }
        public string? Mm { get; set; }
        public string? Place { get; set; }

        public List<FeedInformation>?Feed { get; set; }
    }
}
