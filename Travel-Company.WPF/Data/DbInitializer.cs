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
        var countries = SeedCountries(db);
        SeedStreets(db);
        SeedHotels(db);
        SeedPopulatedPlaces(db, countries);
    }

    private static List<Country> SeedCountries(TravelCompanyDbContext db)
    {
        if (db.Countries.Any())
        {
            return db.Countries.ToList();
        }

        var countries = new List<Country>
        {
            new Country() { Name = "United Kingdom" },
            new Country() { Name = "Canada" },
            new Country() { Name = "Russia" },
            new Country() { Name = "Germany" },
            new Country() { Name = "France" },
            new Country() { Name = "Japan" },
            new Country() { Name = "Australia" },
            new Country() { Name = "Brazil" },
            new Country() { Name = "India" },
            new Country() { Name = "South Africa" },
        };
        db.AddRange(countries);
        db.SaveChanges();
        return countries;
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
            new Hotel() { Name = "The Royal Crown Hotel", Class = "Luxury" },
            new Hotel() { Name = "Windsor Manor", Class = "Budget" },
            new Hotel() { Name = "Thamesview Hotel & Spa", Class = "Budget" },
            new Hotel() { Name = "Highland Retreat", Class = "Budget" },
            new Hotel() { Name = "Coastal Haven Inn", Class = "Resort" },
            new Hotel() { Name = "Victoria Grand Hotel", Class = "Luxury" },
            new Hotel() { Name = "Greenwich Park Lodge", Class = "Budget" },
            new Hotel() { Name = "The Lakeside Retreat", Class = "Luxury" },
            new Hotel() { Name = "Cambridge Riverside Inn", Class = "Resort" },
            new Hotel() { Name = "Edinburgh Castle Hotel", Class = "Luxury" },
        };
        db.AddRange(hotels);
        db.SaveChanges();
    }

    private static void SeedPopulatedPlaces(TravelCompanyDbContext db, List<Country> countries)
    {
        if (db.PopulatedPlaces.Any())
        {
            return;
        }

        var populatedPlaces = new List<PopulatedPlace>
        {
            new PopulatedPlace()
            {
                Name = "London",
                CountryId = countries.FirstOrDefault(c => c.Name == "United Kingdom")!.Id
            },
            new PopulatedPlace() 
            { 
                Name = "Manchester",
                CountryId = countries.FirstOrDefault(c => c.Name == "Canada")!.Id 
            },
            new PopulatedPlace() 
            { 
                Name = "Birmingham",
                CountryId = countries.FirstOrDefault(c => c.Name == "Russia")!.Id 
            },
            new PopulatedPlace() 
            { 
                Name = "Liverpool", 
                CountryId = countries.FirstOrDefault(c => c.Name == "Germany")!.Id 
            },
            new PopulatedPlace() 
            { 
                Name = "Glasgow", 
                CountryId = countries.FirstOrDefault(c => c.Name == "France")!.Id
            },
            new PopulatedPlace() 
            { 
                Name = "Leeds", 
                CountryId = countries.FirstOrDefault(c => c.Name == "Japan")!.Id
            },
            new PopulatedPlace() 
            { Name = "Bristol", 
                CountryId = countries.FirstOrDefault(c => c.Name == "Australia")!.Id 
            },
            new PopulatedPlace() 
            { 
                Name = "Sheffield",
                CountryId = countries.FirstOrDefault(c => c.Name == "Brazil")!.Id 
            },
            new PopulatedPlace() 
            { 
                Name = "Edinburgh", 
                CountryId = countries.FirstOrDefault(c => c.Name == "India")!.Id
            },
            new PopulatedPlace() 
            { 
                Name = "Cardiff", 
                CountryId = countries.FirstOrDefault(c => c.Name == "South Africa")!.Id
            },
        };
        db.AddRange(populatedPlaces);
        db.SaveChanges();
    }
}