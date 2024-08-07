﻿using Microsoft.EntityFrameworkCore;
using SheypoorChi.DataLayer.Models;

//using SheypoorChi.Core.Classes;
using System.Security.Cryptography;
using System.Text;

namespace SheypoorChi.DataLayer.Context;

public class DatabaseContext : DbContext
{
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    //public DbSet<UserDetail> UserDetails { get; set; }
    public DbSet<UserInfo> UserInfos { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Factor> Factors { get; set; }
    public DbSet<FactorDetail> FactorDetails { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // base.OnConfiguring(optionsBuilder);
        options.UseSqlServer(@"Data Source=.\SQLEXPRESS; 
                               Initial Catalog=SheypoorChiDb2;
                               Trusted_Connection=SSPI;
                               Encrypt=false;
                               TrustServerCertificate=True");
    }

    protected override async void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);

        //add first role (admin)
        var adminRole = new Role()
        {
            Id = Guid.NewGuid(),
            RoleName = "admin",
            RoleTitle = "مدیر",
        };
        modelBuilder.Entity<Role>().HasData(adminRole);

        //var userRole = new Role()
        //{
        //    Id = Guid.NewGuid(),
        //    RoleName = "user",
        //    RoleTitle = "کاربر",
        //};
        //modelBuilder.Entity<Role>().HasData(userRole);

        //add first user
        var user = new User()
        {
            Id = Guid.NewGuid(),
            RoleId = adminRole.Id,
            UserName = "09112223344",
            UserPassword = await GetHash("123456789")
        };
        modelBuilder.Entity<User>().HasData(user);


        //add default group list
        //var groups = new List<Group>()
        //{
        //    new Group() { Id = 1, GroupName = "لوازم خانگی" },
        //    new Group() { Id = 2, GroupName = "خودرو" },
        //    new Group() { Id = 3, GroupName = "بازی و سرگرمی" }
        //};

        //modelBuilder.Entity<Group>().HasData(groups);
    }

    public async Task<string> GetHash(string str)
    {
        MD5 md5 = MD5.Create();
        byte[] baseInput = ASCIIEncoding.Default.GetBytes(str);
        byte[] hashInput = md5.ComputeHash(baseInput);

        var hashStr = Convert.ToBase64String(hashInput);

        return await Task.FromResult(hashStr);
    }
}