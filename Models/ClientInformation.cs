﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace FeedAPI.Models
{
    public partial class ClientInformation
    {
        public ClientInformation()
        {
            FeedInformation = new HashSet<FeedInformation>();
            StockInformation = new HashSet<StockInformation>();
        }

        public long Id { get; set; }
        public DateTime? CreationTime { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Invoice { get; set; }
        public string Person { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public int? Compiled { get; set; }
        public string Description { get; set; }
        public bool? IsDeleted { get; set; }
        public string NickName { get; set; }

        public virtual ICollection<FeedInformation> FeedInformation { get; set; }
        public virtual ICollection<StockInformation> StockInformation { get; set; }
    }
}