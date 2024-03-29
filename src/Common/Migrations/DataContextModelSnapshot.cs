﻿// <auto-generated />
using Common.DbDataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Common.Migrations
{
    [DbContext(typeof(CarDataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.6.22329.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Common.Entities.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("ManufactureYear")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BrandName = "LAMBORGINI",
                            ManufactureYear = "1998",
                            Model = "COUNTACH"
                        },
                        new
                        {
                            Id = 2,
                            BrandName = "PORSCHE",
                            ManufactureYear = "1976",
                            Model = "911 TURBO"
                        },
                        new
                        {
                            Id = 3,
                            BrandName = "FORD",
                            ManufactureYear = "1968",
                            Model = "MUSTANG"
                        },
                        new
                        {
                            Id = 4,
                            BrandName = "HONDA",
                            ManufactureYear = "2001",
                            Model = "CIVIC"
                        },
                        new
                        {
                            Id = 5,
                            BrandName = "JEEP",
                            ManufactureYear = "2019",
                            Model = "RUBICON"
                        },
                        new
                        {
                            Id = 6,
                            BrandName = "SUBARU",
                            ManufactureYear = "1999",
                            Model = "IMPREZA"
                        },
                        new
                        {
                            Id = 7,
                            BrandName = "CHEVROLET",
                            ManufactureYear = "2004",
                            Model = "CORVETTE"
                        },
                        new
                        {
                            Id = 8,
                            BrandName = "FERRARI",
                            ManufactureYear = "1997",
                            Model = "F40"
                        },
                        new
                        {
                            Id = 9,
                            BrandName = "DODGE",
                            ManufactureYear = "2013",
                            Model = "CHARGER"
                        },
                        new
                        {
                            Id = 10,
                            BrandName = "MAZDA",
                            ManufactureYear = "1998",
                            Model = "RX-3"
                        },
                        new
                        {
                            Id = 11,
                            BrandName = "MERCEDES",
                            ManufactureYear = "2010",
                            Model = "G-CLASS"
                        },
                        new
                        {
                            Id = 12,
                            BrandName = "DODGE",
                            ManufactureYear = "2002",
                            Model = "VIPER SRT"
                        },
                        new
                        {
                            Id = 13,
                            BrandName = "TOYOTA",
                            ManufactureYear = "1999",
                            Model = "Supra"
                        },
                        new
                        {
                            Id = 14,
                            BrandName = "HONDA",
                            ManufactureYear = "2002",
                            Model = "S2000"
                        },
                        new
                        {
                            Id = 15,
                            BrandName = "BMW",
                            ManufactureYear = "2022",
                            Model = "M5"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
