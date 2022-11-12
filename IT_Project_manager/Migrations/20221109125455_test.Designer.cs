﻿// <auto-generated />
using IT_Project_manager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ITProjectmanager.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221109125455_test")]
    partial class test
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("IT_Project_manager.Models.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Members");

                    b.HasData(
                        new
                        {
                            Id = 5,
                            Email = "krzysiek.palonek@gmail.com",
                            Name = "Krzysztof",
                            Surname = "Palonek"
                        },
                        new
                        {
                            Id = 6,
                            Email = "marz.koł@gmail.com",
                            Name = "Marzena",
                            Surname = "Kołodziej"
                        },
                        new
                        {
                            Id = 7,
                            Email = "jan.kow@gmail.com",
                            Name = "Jan",
                            Surname = "Kowalski"
                        },
                        new
                        {
                            Id = 8,
                            Email = "Nat.uro@gmail.com",
                            Name = "Natalia",
                            Surname = "Urodek"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
