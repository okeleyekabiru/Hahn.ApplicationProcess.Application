﻿// <auto-generated />
using System;
using Hahn.ApplicationProcess.February2021.Data.EfRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hahn.ApplicationProcess.February2021.Web.Migrations
{
    [DbContext(typeof(HahnDbContext))]
    [Migration("20210406074347_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hahn.ApplicationProcess.February2021.Domain.Models.Asset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AssetName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Broken")
                        .HasColumnType("bit");

                    b.Property<string>("CountryOfDepartment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Department")
                        .HasColumnType("int");

                    b.Property<string>("EMailAdressOfDepartment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("PurchaseDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("Assets");
                });
#pragma warning restore 612, 618
        }
    }
}
