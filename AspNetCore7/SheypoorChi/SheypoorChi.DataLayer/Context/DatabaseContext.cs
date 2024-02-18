using Microsoft.EntityFrameworkCore;
using SheypoorChi.DataLayer.Models;

namespace SheypoorChi.DataLayer.Context;

public class DatabaseContext:DbContext
{
    public DbSet<Role> Roles { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // base.OnConfiguring(optionsBuilder);
        options.UseSqlServer(@"Data Source=.\SQLEXPRESS; 
                                Initial Catalog=SheypoorChiDb;
                                Trusted_Connection=SSPI;
                                  Encrypt=false;
                                    TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);

        var adminRole = new Role()
        {
            Id = Guid.NewGuid(),
            RoleName = "admin",
            RoleTitle = "مدیر",
        };

        modelBuilder.Entity<Role>().HasData(adminRole);
    }
}
