﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FeedAPI.Models
{
    public partial class FeedingContext : DbContext
    {
        public FeedingContext()
        {
        }

        public FeedingContext(DbContextOptions<FeedingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClientInformation> ClientInformation { get; set; }
        public virtual DbSet<DeliveryAddress> DeliveryAddress { get; set; }
        public virtual DbSet<FeedInformation> FeedInformation { get; set; }
        public virtual DbSet<LoginInfo> LoginInfo { get; set; }
        public virtual DbSet<LoginInfoRoles> LoginInfoRoles { get; set; }
        public virtual DbSet<LoginMenus> LoginMenus { get; set; }
        public virtual DbSet<LoginRoles> LoginRoles { get; set; }
        public virtual DbSet<LoginRolesMenus> LoginRolesMenus { get; set; }
        public virtual DbSet<StockInformation> StockInformation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientInformation>(entity =>
            {
                entity.ToTable("client_information");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Compiled).HasColumnName("compiled");

                entity.Property(e => e.CreationTime)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("creation_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Invoice)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("invoice");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Ltd)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("ltd");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("mobile");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.NickName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nick_name");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("number");

                entity.Property(e => e.Person)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("person");

                entity.Property(e => e.Telephone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("telephone");
            });

            modelBuilder.Entity<DeliveryAddress>(entity =>
            {
                entity.ToTable("delivery_address");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<FeedInformation>(entity =>
            {
                entity.ToTable("feed_information");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClientId).HasColumnName("client_id");

                entity.Property(e => e.CreationTime)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("creation_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.FeedNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("feed_number");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Machine)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("machine");

                entity.Property(e => e.Quantity)
                    .HasColumnType("numeric(10, 2)")
                    .HasColumnName("quantity");

                entity.Property(e => e.Raw)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("raw");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.StockId).HasColumnName("stock_id");

                entity.Property(e => e.Weight)
                    .HasColumnType("numeric(10, 2)")
                    .HasColumnName("weight");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.FeedInformation)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_feed_information_client_information");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.FeedInformation)
                    .HasForeignKey(d => d.StockId)
                    .HasConstraintName("FK_feed_information_stock");
            });

            modelBuilder.Entity<LoginInfo>(entity =>
            {
                entity.ToTable("login_info");

                entity.HasIndex(e => e.Username, "IX_login_info")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Disabled).HasColumnName("disabled");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<LoginInfoRoles>(entity =>
            {
                entity.ToTable("login_info_roles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.InfoId).HasColumnName("info_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Info)
                    .WithMany(p => p.LoginInfoRoles)
                    .HasForeignKey(d => d.InfoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_login_info_roles_login_info");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.LoginInfoRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_login_info_roles_login_roles");
            });

            modelBuilder.Entity<LoginMenus>(entity =>
            {
                entity.ToTable("login_menus");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Icon)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("icon");

                entity.Property(e => e.MenuName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("menu_name");

                entity.Property(e => e.OrderNumber).HasColumnName("order_number");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("url");
            });

            modelBuilder.Entity<LoginRoles>(entity =>
            {
                entity.ToTable("login_roles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Disabled).HasColumnName("disabled");

                entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<LoginRolesMenus>(entity =>
            {
                entity.ToTable("login_roles_menus");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MenuId).HasColumnName("menu_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.LoginRolesMenus)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_login_roles_menus_login_menus");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.LoginRolesMenus)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_login_roles_menus_login_roles");
            });

            modelBuilder.Entity<StockInformation>(entity =>
            {
                entity.ToTable("stock_information");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Chamfer)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("chamfer");

                entity.Property(e => e.Class)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("class");

                entity.Property(e => e.ClientId).HasColumnName("client_id");

                entity.Property(e => e.Cost)
                    .HasColumnType("numeric(10, 2)")
                    .HasColumnName("cost");

                entity.Property(e => e.Ditch)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ditch");

                entity.Property(e => e.Ear)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ear");

                entity.Property(e => e.FinishAmount).HasColumnName("finish_amount");

                entity.Property(e => e.Hole1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("hole_1");

                entity.Property(e => e.Hole2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("hole_2");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Material)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("material");

                entity.Property(e => e.Mm)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("mm");

                entity.Property(e => e.Omi)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("omi");

                entity.Property(e => e.Pcs)
                    .HasColumnType("numeric(10, 2)")
                    .HasColumnName("pcs");

                entity.Property(e => e.Peel1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("peel_1");

                entity.Property(e => e.Peel2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("peel_2");

                entity.Property(e => e.Place)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("place");

                entity.Property(e => e.Project)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("project");

                entity.Property(e => e.Raise)
                    .HasColumnType("numeric(10, 2)")
                    .HasColumnName("raise");

                entity.Property(e => e.Size)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("size");

                entity.Property(e => e.Special)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("special");

                entity.Property(e => e.StockName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("stock_name");

                entity.Property(e => e.StockNumber)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("stock_number");

                entity.Property(e => e.Taper)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("taper");

                entity.Property(e => e.Typing)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("typing");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Weight)
                    .HasColumnType("numeric(10, 3)")
                    .HasColumnName("weight");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.StockInformation)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_stock_information_client_information");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}