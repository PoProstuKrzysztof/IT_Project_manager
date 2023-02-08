using IT_Project_manager.Areas.Identity.Data;
using IT_Project_manager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IT_Project_manager.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Member> Members { get; set; }
    public DbSet<Manager> Managers { get; set; }

    public DbSet<Team> Teams { get; set; }

    

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Database=IT_Project_manager;Integrated Security=True;", builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });

            base.OnConfiguring(optionsBuilder);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Team>().HasData(
            new Team() { Id = 1, Name = "Back-end", Description = "Creating connection between database and API", AssigmentDate = DateTime.Now, DeadlineDate = DateTime.Parse("2023-03-03") },
            new Team() { Id = 2, Name = "Front-end", Description = "Creating website", AssigmentDate = DateTime.Now, DeadlineDate = DateTime.Parse("2023-05-24"), }
            );

        modelBuilder.Entity<Member>().HasData(
      new Member() { Id = 1, Name = "Krzysztof", Surname = "Palonek", Email = "krzysiek.palonek@gmail.com", DateOfBirth = DateTime.Parse("2000-10-23") },
     new Member() { Id = 2, Name = "Marzena", Surname = "Kołodziej", Email = "marz.koł@gmail.com", DateOfBirth = DateTime.Parse("2001-05-24") },
     new Member() { Id = 3, Name = "Jan", Surname = "Kowalski", Email = "jan.kow@gmail.com", DateOfBirth = DateTime.Parse("1989-07-11") },
     new Member() { Id = 4, Name = "Natalia", Surname = "Urodek", Email = "Nat.uro@gmail.com", DateOfBirth = DateTime.Parse("1999-12-10") }
     );

        modelBuilder.Entity<Manager>().HasData(
            new Manager() { Id = 1, Name = "Maciej", Surname = "Krasko", Telephone = "123-456-789" },
            new Manager() { Id = 2, Name = "Zuzanna", Surname = "Krasko", Telephone = "987-654-321" }
            );
        modelBuilder.Entity<Member>()
            .HasMany(m => m.Managers)
            .WithMany(a => a.Members)

            .UsingEntity(join => join.HasData(
                new { MembersId = 1, ManagersId = 1 },
                new { MembersId = 2, ManagersId = 1 },
                new { MembersId = 3, ManagersId = 2 },
                new { MembersId = 4, ManagersId = 2 }
                ));

        modelBuilder.Entity<Manager>()
            .HasMany(t => t.Teams)
            .WithMany(m => m.Managers)
            .UsingEntity(join => join.HasData(

                new { ManagersId = 1, TeamsId = 1 },
                new { ManagersId = 2, TeamsId = 2 }
                ));

        modelBuilder.Entity<Member>()
            .HasMany(t => t.Teams)
            .WithMany(m => m.Members)
            .UsingEntity(join => join.HasData(
                new { MembersId = 1, TeamsId = 1 },
                new { MembersId = 2, TeamsId = 1 },
                new { MembersId = 3, TeamsId = 2 },
                new { MembersId = 4, TeamsId = 2 }
                ));

        modelBuilder.Entity<IdentityRole>().HasData
            (
            new IdentityRole() { Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", 
                Name = "Administrator", 
                ConcurrencyStamp = "1", 
                NormalizedName = "ADMINISTRATOR"
                 }
            ) ;


        var hasher = new PasswordHasher<ApplicationUser>();


        modelBuilder.Entity<ApplicationUser>().HasData
            (
            new ApplicationUser() { Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                Name = "Maciej", 
                Surname = "Krasko",
                EmailConfirmed = true, 
                Email = "maciej.krasko@gmail.com", 
                UserName = "maciej.krasko@gmail.com", 
                NormalizedUserName = "MACIEJ.KRASKO@gmail.com",
                PasswordHash = hasher.HashPassword(null,"Test!1")}
            );

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
            }
            );
        
    }
}