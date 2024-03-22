﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace FeedAPI.Models
{
    public partial class FeedInformation
    {
        public long Id { get; set; }
        public DateTime? CreationTime { get; set; }
        public long? ClientId { get; set; }
        public decimal? Quantity { get; set; }
        public string Description { get; set; }
        public string Machine { get; set; }
        public string FeedNumber { get; set; }
        public bool? IsDeleted { get; set; }
        public long? StockId { get; set; }
        public bool? Status { get; set; }
        public decimal? Weight { get; set; }
        public string Raw { get; set; }

        public virtual ClientInformation Client { get; set; }
        public virtual StockInformation Stock { get; set; }
    }
}