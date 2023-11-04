using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Travel_Company.WPF.Models;

public partial class TravelCompanyDbContext : DbContext
{
    public TravelCompanyDbContext()
    {
    }

    public TravelCompanyDbContext(DbContextOptions<TravelCompanyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<Object> Objects { get; set; }

    public virtual DbSet<Penalty> Penalties { get; set; }

    public virtual DbSet<PopulatedPlace> PopulatedPlaces { get; set; }

    public virtual DbSet<Route> Routes { get; set; }

    public virtual DbSet<RoutesPopulatedPlace> RoutesPopulatedPlaces { get; set; }

    public virtual DbSet<Street> Streets { get; set; }

    public virtual DbSet<TourGuide> TourGuides { get; set; }

    public virtual DbSet<TouristGroup> TouristGroups { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersObject> UsersObjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlite("Data Source=TravelCompany.db;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Birthdate)
                .HasColumnType("date")
                .HasColumnName("birthdate");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.PassportIssueDate)
                .HasColumnType("date")
                .HasColumnName("passport_issue_date");
            entity.Property(e => e.PassportIssuingAuthority)
                .HasMaxLength(255)
                .HasColumnName("passport_issuing_authority");
            entity.Property(e => e.PassportNumber)
                .HasMaxLength(50)
                .HasColumnName("passport_number");
            entity.Property(e => e.PassportSeries)
                .HasMaxLength(50)
                .HasColumnName("passport_series");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(255)
                .HasColumnName("patronymic");
            entity.Property(e => e.Photograph)
                .HasColumnType("image")
                .HasColumnName("photograph");
            entity.Property(e => e.StreetId).HasColumnName("street_id");

            entity.HasOne(d => d.Street).WithMany(p => p.Clients)
                .HasForeignKey(d => d.StreetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Client_Street");

            entity.HasMany(d => d.TouristGroups).WithMany(p => p.Clients)
                .UsingEntity<Dictionary<string, object>>(
                    "ClientsTouristGroup",
                    r => r.HasOne<TouristGroup>().WithMany()
                        .HasForeignKey("TouristGroupId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ClientsTouristGroups_TouristGroup"),
                    l => l.HasOne<Client>().WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ClientsTouristGroups_Client"),
                    j =>
                    {
                        j.HasKey("ClientId", "TouristGroupId");
                        j.ToTable("ClientsTouristGroups");
                        j.IndexerProperty<long>("ClientId").HasColumnName("client_id");
                        j.IndexerProperty<long>("TouristGroupId").HasColumnName("tourist_group_id");
                    });
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Country");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.ToTable("Hotel");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Class)
                .HasMaxLength(50)
                .HasColumnName("class");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Object>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Penalty>(entity =>
        {
            entity.ToTable("Penalty");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.CompensationAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("compensation_amount");
            entity.Property(e => e.CompensationDate)
                .HasColumnType("date")
                .HasColumnName("compensation_date");
            entity.Property(e => e.CompensationDescription)
                .HasMaxLength(500)
                .HasColumnName("compensation_description");
            entity.Property(e => e.TourGuideId).HasColumnName("tour_guide_id");

            entity.HasOne(d => d.Client).WithMany(p => p.Penalties)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Penalty_Client");

            entity.HasOne(d => d.TourGuide).WithMany(p => p.Penalties)
                .HasForeignKey(d => d.TourGuideId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Penalty_TourGuide");
        });

        modelBuilder.Entity<PopulatedPlace>(entity =>
        {
            entity.ToTable("PopulatedPlace");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.HasOne(d => d.Country).WithMany(p => p.PopulatedPlaces)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PopulatedPlace_Country");
        });

        modelBuilder.Entity<Route>(entity =>
        {
            entity.ToTable("Route");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cost)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("cost");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.EndDatetime)
                .HasColumnType("datetime")
                .HasColumnName("end_datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.StartDatetime)
                .HasColumnType("datetime")
                .HasColumnName("start_datetime");

            entity.HasOne(d => d.Country).WithMany(p => p.Routes)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Route_Country");
        });

        modelBuilder.Entity<RoutesPopulatedPlace>(entity =>
        {
            entity.HasKey(e => new { e.RouteId, e.PopulatedPlaceId });

            entity.Property(e => e.RouteId).HasColumnName("route_id");
            entity.Property(e => e.PopulatedPlaceId).HasColumnName("populated_place_id");
            entity.Property(e => e.ExcursionProgram)
                .HasMaxLength(1000)
                .HasColumnName("excursion_program");
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.StayEndDatetime)
                .HasColumnType("datetime")
                .HasColumnName("stay_end_datetime");
            entity.Property(e => e.StayStartDatetime)
                .HasColumnType("datetime")
                .HasColumnName("stay_start_datetime");

            entity.HasOne(d => d.Hotel).WithMany(p => p.RoutesPopulatedPlaces)
                .HasForeignKey(d => d.HotelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoutesPopulatedPlaces_Hotel");

            entity.HasOne(d => d.PopulatedPlace).WithMany(p => p.RoutesPopulatedPlaces)
                .HasForeignKey(d => d.PopulatedPlaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoutesPopulatedPlaces_PopulatedPlace");

            entity.HasOne(d => d.Route).WithMany(p => p.RoutesPopulatedPlaces)
                .HasForeignKey(d => d.RouteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoutesPopulatedPlaces_Route");
        });

        modelBuilder.Entity<Street>(entity =>
        {
            entity.ToTable("Street");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TourGuide>(entity =>
        {
            entity.ToTable("TourGuide");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Birthdate)
                .HasColumnType("date")
                .HasColumnName("birthdate");
            entity.Property(e => e.FiredDate)
                .HasColumnType("date")
                .HasColumnName("fired_date");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.IsFired).HasColumnName("is_fired");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(255)
                .HasColumnName("patronymic");
            entity.Property(e => e.Salary)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("salary");
            entity.Property(e => e.StreetId).HasColumnName("street_id");

            entity.HasOne(d => d.Street).WithMany(p => p.TourGuides)
                .HasForeignKey(d => d.StreetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TourGuide_Street");
        });

        modelBuilder.Entity<TouristGroup>(entity =>
        {
            entity.ToTable("TouristGroup");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.RouteId).HasColumnName("route_id");
            entity.Property(e => e.TourGuideId).HasColumnName("tour_guide_id");

            entity.HasOne(d => d.Route).WithMany(p => p.TouristGroups)
                .HasForeignKey(d => d.RouteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TouristGroup_Route");

            entity.HasOne(d => d.TourGuide).WithMany(p => p.TouristGroups)
                .HasForeignKey(d => d.TourGuideId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TouristGroup_TourGuide");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UsersObject>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ObjectId });

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ObjectId).HasColumnName("object_id");
            entity.Property(e => e.CanCreate).HasColumnName("can_create");
            entity.Property(e => e.CanDelete).HasColumnName("can_delete");
            entity.Property(e => e.CanRead).HasColumnName("can_read");
            entity.Property(e => e.CanUpdate).HasColumnName("can_update");

            entity.HasOne(d => d.Object).WithMany(p => p.UsersObjects)
                .HasForeignKey(d => d.ObjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsersObjects_Objects");

            entity.HasOne(d => d.User).WithMany(p => p.UsersObjects)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsersObjects_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
