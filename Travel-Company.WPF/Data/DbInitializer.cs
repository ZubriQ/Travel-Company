using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Travel_Company.WPF.Models;

namespace Travel_Company.WPF.Data;

public static class DbInitializer
{
    public static void Seed(IServiceProvider serviceProvider)
    {
        using var serviceScope = serviceProvider.CreateScope();
        var db = serviceScope.ServiceProvider.GetService<TravelCompanyDbContext>();
        db!.Database.EnsureCreated();

        SeedAllEntities(db);
    }

    private static void SeedAllEntities(TravelCompanyDbContext db)
    {
        SeedStreets(db);
    }

    private static void SeedStreets(TravelCompanyDbContext db)
    {
        if (db.Streets.Any())
        {
            return;
        }

        var streets = new List<Street>
            {
                new Street() { Name = "Abbey Road" },
                new Street() { Name = "Baker Street" },
                new Street() { Name = "Buckingham Palace Road" },
                new Street() { Name = "Carlisle Street" },
                new Street() { Name = "Cavendish Square" },
                new Street() { Name = "Downing Street" },
                new Street() { Name = "Eaton Square" },
                new Street() { Name = "Fitzroy Street" },
                new Street() { Name = "Great George Street" },
                new Street() { Name = "Hanover Square" },
            };
        db.AddRange(streets);
        db.SaveChanges();
    }
}