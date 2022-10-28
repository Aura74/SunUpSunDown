﻿// <auto-generated />
using GetAPISunset.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GetAPISunset.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221028075042_longlat")]
    partial class longlat
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GetAPISunset.Results", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DagenDetGaller")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("OriginalSunrise")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OriginalSunset")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SummerWinter")
                        .HasColumnType("bit");

                    b.Property<string>("sunrise")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sunset")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SunTime");
                });
#pragma warning restore 612, 618
        }
    }
}
