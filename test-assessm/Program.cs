using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Globalization;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        Program p = new Program();

        using var db = new DataContext();
            string dir = System.IO.Path.GetDirectoryName(
      System.Reflection.Assembly.GetExecutingAssembly().Location);

        string file = dir + "\\sample-cab-data.csv";
        List<Data> csvList = new List<Data>();
        using (var reader = new StreamReader(file))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<Data>();
            csvList.AddRange(records);

        }

        db.Data.AddRange(csvList);
        db.SaveChanges();


        Console.WriteLine();
    }

    public static int highestAmount()
    {
        using var db = new DataContext();


        var avgTipByLocation = db.Data.GroupBy(trip => trip.PULocationID)
                                    .Select(group => new
                                    {
                                        LocationID = group.Key,
                                        AvgTip = group.Average(trip => trip.tip_amount)
                                    });

        var maxAvgTipLocation = avgTipByLocation.OrderByDescending(item => item.AvgTip)
                                                .FirstOrDefault();

        return (int)maxAvgTipLocation.LocationID;
    }

    static List<Data> Top100LongestFares()
    {
        using var db = new DataContext();

        var top100LongestFares = db.Data.OrderByDescending(trip => trip.trip_distance)
                                      .Take(100)
                                      .ToList();

        return top100LongestFares;
    }

    static List<Data> Top100LongestTravel()
    {
        using var db = new DataContext();

        var top100LongestFares = db.Data.OrderByDescending(trip => trip.tpep_dropoff_datetime - trip.tpep_pickup_datetime)
                                      .Take(100)
                                      .ToList();

        return top100LongestFares;
    }

    static void DistinctAndWriteToCSV()
    {
        using var db = new DataContext();

        string dir = System.IO.Path.GetDirectoryName(
      System.Reflection.Assembly.GetExecutingAssembly().Location);
        string file = dir + "\\duplicates.csv";

        var duplicateTrips = db.Data.GroupBy(trip => new { trip.tpep_pickup_datetime, trip.tpep_dropoff_datetime, trip.passenger_count })
                                      .Where(group => group.Count() > 1)
                                      .SelectMany(group => group.Skip(1))
                                      .ToList();
        using (var writer = new StreamWriter(file))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(duplicateTrips);
        }
    }

    static void ConvertStoreAndForwardFlag(List<Data> trips)
    {
        foreach (var trip in trips)
        {
            trip.store_and_fwd_flag = trip.store_and_fwd_flag == "N" ? "No" : trip.store_and_fwd_flag;
            trip.store_and_fwd_flag = trip.store_and_fwd_flag == "Y" ? "Yes" : trip.store_and_fwd_flag;
        }
    }

    static void TrimWhitespaces(List<Data> trips)
    {
        foreach (var trip in trips)
        {
            trip.store_and_fwd_flag.Trim();
        }
    }
    static void BulkAdd(List<Data> trips)
    {
        using var db = new DataContext();

        db.Data.AddRange(trips);
        db.SaveChanges();
    }

}