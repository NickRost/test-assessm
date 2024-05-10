using CsvHelper.Configuration.Attributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

public class DataContext : DbContext
{
    public DbSet<Data> Data { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AssessmentDB;Trusted_Connection=True");
    } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Data>().HasKey(p => p.id);
    }
}

public class Data
{
    [Ignore]
    public int id { get; set; }
    [Optional]
    public DateTime? tpep_pickup_datetime { get; set; }
    [Optional]
    public DateTime? tpep_dropoff_datetime { get; set; }
    [Optional]
    public int? passenger_count { get; set; }
    [Optional]
    public float? trip_distance { get; set; }
    [Optional]
    public string store_and_fwd_flag { get; set; }
    [Optional]
    public int? PULocationID { get; set; }
    [Optional]
    public int? DOLocationID { get; set; }
    [Optional]
    public double? fare_amount { get; set; }
    [Optional]
    public double? tip_amount { get; set; }
}

