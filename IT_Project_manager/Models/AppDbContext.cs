using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore.Design;




namespace IT_Project_manager.Models;

public class AppDbContext : DbContext
{
    public DbSet<Member> Members { get; set; }

    public string DbPath { get; set; }


    public AppDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath( folder );
        DbPath = System.IO.Path.Join(path, "members.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite( $"Data Source = {DbPath}" );

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>().HasData(
             new Member() { Name = "Krzysztof", Surname = "Palonek", Email = "krzysiek.palonek@gmail.com" },
            new Member() { Name = "Marzena", Surname = "Kołodziej", Email = "marz.koł@gmail.com" },
            new Member() { Name = "Jan", Surname = "Kowalski", Email = "jan.kow@gmail.com" },
            new Member() { Name = "Natalia", Surname = "Urodek", Email = "Nat.uro@gmail.com"}
            );
    }
}
