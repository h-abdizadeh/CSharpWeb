using FardaOnlineShop.Models;
using Microsoft.EntityFrameworkCore;
using FardaOnlineShop.Classes;

namespace FardaOnlineShop.Data;

public class DatabaseContext : DbContext
{
    //1
    public DbSet<Product> Products { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }

    //2
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite($"data source=myShop.db");
    }

    //3
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);
        object[] defaultGroups =
        {
            new Group(){Id=1,Title="گروه اول"},
            new Group(){Id=2,Title="گروه دوم"},
            new Group(){Id=3,Title="گروه سوم"}
        };

        modelBuilder.Entity<Group>().HasData(defaultGroups);

        object[] defaultRoles =
        {
            new Role() { Id = Guid.NewGuid(), Title = "admin" },
            new Role() { Id = Guid.NewGuid(), Title = "user" }
        };

        modelBuilder.Entity<Role>().HasData(defaultRoles);

        object[] defaultUser =
        {
            new User(){
                Id=1,
                Mobile="09102223344",
                Password=new AdminClass().HashPassword("123456789"),
                RoleId=((Role)defaultRoles[0]).Id
            }
        };

        modelBuilder.Entity<User>().HasData(defaultUser);
    }
}
