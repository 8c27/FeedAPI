﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace FeedAPI.Models
{
    public partial class LoginRoles
    {
        public LoginRoles()
        {
            LoginInfoRoles = new HashSet<LoginInfoRoles>();
            LoginRolesMenus = new HashSet<LoginRolesMenus>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool Disabled { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<LoginInfoRoles> LoginInfoRoles { get; set; }
        public virtual ICollection<LoginRolesMenus> LoginRolesMenus { get; set; }
    }
}