﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace FeedAPI.Models
{
    public partial class LoginInfo
    {
        public LoginInfo()
        {
            LoginInfoRoles = new HashSet<LoginInfoRoles>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public bool Disabled { get; set; }

        public virtual ICollection<LoginInfoRoles> LoginInfoRoles { get; set; }
    }
}