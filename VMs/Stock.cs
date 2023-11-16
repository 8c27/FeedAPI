using FeedAPI.Models;

namespace FeedAPI.VMs
{
    public class Stock
    {
        public int Id { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string StockName { get; set; }
        public int? FinishAmount { get; set; }
        public decimal? Weight { get; set; }
        public bool? IsDeleted { get; set; }
        public List<FeedInformation>?Feed { get; set; }
    }
}
