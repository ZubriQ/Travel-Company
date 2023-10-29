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
        SeedHotels(db);
        SeedPopulatedPlaces(db);
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

    private static void SeedHotels(TravelCompanyDbContext db)
    {
        if (db.Hotels.Any())
        {
            return;
        }

        var hotels = new List<Hotel>
        {
            new Hotel() { Name = "The Royal Crown Hotel" },
            new Hotel() { Name = "Windsor Manor" },
            new Hotel() { Name = "Thamesview Hotel & Spa" },
            new Hotel() { Name = "Highland Retreat" },
            new Hotel() { Name = "Coastal Haven Inn" },
            new Hotel() { Name = "Victoria Grand Hotel" },
            new Hotel() { Name = "Greenwich Park Lodge" },
            new Hotel() { Name = "The Lakeside Retreat" },
            new Hotel() { Name = "Cambridge Riverside Inn" },
            new Hotel() { Name = "Edinburgh Castle Hotel" },
        };

        db.AddRange(hotels);
        db.SaveChanges();
    }

    private static void SeedPopulatedPlaces(TravelCompanyDbContext db)
    {
        if (db.Hotels.Any())
        {
            return;
        }

        var populatedPlaces = new List<PopulatedPlace>
        {
            new PopulatedPlace() { Name = "London" },
            new PopulatedPlace() { Name = "Manchester" },
            new PopulatedPlace() { Name = "Birmingham" },
            new PopulatedPlace() { Name = "Liverpool" },
            new PopulatedPlace() { Name = "Glasgow" },
            new PopulatedPlace() { Name = "Leeds" },
            new PopulatedPlace() { Name = "Bristol" },
            new PopulatedPlace() { Name = "Sheffield" },
            new PopulatedPlace() { Name = "Edinburgh" },
            new PopulatedPlace() { Name = "Cardiff" },
        };

        db.AddRange(populatedPlaces);
        db.SaveChanges();
    }
}