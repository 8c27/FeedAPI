﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace FeedAPI.Models
{
    public partial class Stock
    {
        public int Id { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string StockId { get; set; }
        public int? FinishAmount { get; set; }
        public decimal? Weight { get; set; }
        public bool? IsDeleted { get; set; }
    }
}