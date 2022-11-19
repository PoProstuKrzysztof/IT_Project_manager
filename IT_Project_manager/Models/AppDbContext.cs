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
        DbPath = System.IO.Path.Join( path, "members.db" );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite( $"Data Source = {DbPath}" );

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>().HasData(
             new Member() { Id = 1, Name = "Krzysztof", Surname = "Palonek", Email = "krzysiek.palonek@gmail.com"/*, DateOfBirth=DateTime.Parse("2000-10-23")*/},
            new Member() { Id = 2, Name = "Marzena", Surname = "Kołodziej", Email = "marz.koł@gmail.com"/*, DateOfBirth = DateTime.Parse( "2001-05-24" )*/ },
            new Member() { Id = 3, Name = "Jan", Surname = "Kowalski", Email = "jan.kow@gmail.com"/*, DateOfBirth = DateTime.Parse( "1989-07-11" )*/ },
            new Member() { Id = 4, Name = "Natalia", Surname = "Urodek", Email = "Nat.uro@gmail.com"/*, DateOfBirth = DateTime.Parse( "1999-12-10" )*/ }
            );
    }
}
