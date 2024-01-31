﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace FeedAPI.Models
{
    public partial class StockInformation
    {
        public StockInformation()
        {
            FeedInformation = new HashSet<FeedInformation>();
        }

        public int Id { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string StockName { get; set; }
        public int? FinishAmount { get; set; }
        public decimal? Weight { get; set; }
        public bool? IsDeleted { get; set; }
        public int? ClientId { get; set; }

        public virtual ClientInformation Client { get; set; }
        public virtual ICollection<FeedInformation> FeedInformation { get; set; }
    }
}