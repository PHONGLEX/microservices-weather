﻿// <auto-generated />
using System;
using CloudWeather.Report.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CloudWeather.Report.Migrations
{
    [DbContext(typeof(WeatherReportDbContext))]
    [Migration("20220411091553_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("CloudWeather.Report.DataAccess.WeatherReport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("AverageHighF")
                        .HasColumnType("numeric");

                    b.Property<decimal>("AverageLowF")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("RainfallTotalInches")
                        .HasColumnType("numeric");

                    b.Property<decimal>("SnowTotalInches")
                        .HasColumnType("numeric");

                    b.Property<string>("ZipCode")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("weather_report");
                });
#pragma warning restore 612, 618
        }
    }
}
