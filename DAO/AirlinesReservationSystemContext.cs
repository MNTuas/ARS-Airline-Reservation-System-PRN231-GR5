using System;
using System.Collections.Generic;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAO;

public partial class AirlinesReservationSystemContext : DbContext
{
    public AirlinesReservationSystemContext()
    {
    }

    public AirlinesReservationSystemContext(DbContextOptions<AirlinesReservationSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Airline> Airlines { get; set; }

    public virtual DbSet<Airplane> Airplanes { get; set; }

    public virtual DbSet<Airport> Airports { get; set; }

    public virtual DbSet<BookingInformation> BookingInformations { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<FlightClass> FlightClasses { get; set; }

    public virtual DbSet<Passenger> Passengers { get; set; }

    public virtual DbSet<PassengerOfBooking> PassengerOfBookings { get; set; }

    public virtual DbSet<PaymentRecord> PaymentRecords { get; set; }

    public virtual DbSet<Rank> Ranks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    private string GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
        return configuration["ConnectionStrings:DefaultConnectionString"];
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Airline>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Airlines__3214EC074CD04C18");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<Airplane>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Airplane__3214EC079988EC63");

            entity.ToTable("Airplane");

            entity.HasIndex(e => e.Code, "IDX_Airplane_Code");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AirlinesId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.Airlines).WithMany(p => p.Airplanes)
                .HasForeignKey(d => d.AirlinesId)
                .HasConstraintName("FK__Airplane__Airlin__2D27B809");
        });

        modelBuilder.Entity<Airport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Airport__3214EC0785BADA5E");

            entity.ToTable("Airport");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.Country).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<BookingInformation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookingI__3214EC074C6DE59E");

            entity.ToTable("BookingInformation");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FlightClassId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FlightId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.FlightClass).WithMany(p => p.BookingInformations)
                .HasForeignKey(d => d.FlightClassId)
                .HasConstraintName("FK__BookingIn__Fligh__3B75D760");

            entity.HasOne(d => d.Flight).WithMany(p => p.BookingInformations)
                .HasForeignKey(d => d.FlightId)
                .HasConstraintName("FK__BookingIn__Fligh__3A81B327");

            entity.HasOne(d => d.User).WithMany(p => p.BookingInformations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__BookingIn__UserI__3C69FB99");
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Flight__3214EC07505E3162");

            entity.ToTable("Flight");

            entity.HasIndex(e => e.ArrivalTime, "IDX_Flight_ArrivalTime");

            entity.HasIndex(e => e.DepartureTime, "IDX_Flight_DepartureTime");

            entity.HasIndex(e => e.Status, "IDX_Flight_Status");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AirplaneId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ArrivalTime).HasColumnType("datetime");
            entity.Property(e => e.DepartureTime).HasColumnType("datetime");
            entity.Property(e => e.From)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.To)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Airplane).WithMany(p => p.Flights)
                .HasForeignKey(d => d.AirplaneId)
                .HasConstraintName("FK__Flight__Airplane__300424B4");

            entity.HasOne(d => d.FromNavigation).WithMany(p => p.FlightFromNavigations)
                .HasForeignKey(d => d.From)
                .HasConstraintName("FK__Flight__From__30F848ED");

            entity.HasOne(d => d.ToNavigation).WithMany(p => p.FlightToNavigations)
                .HasForeignKey(d => d.To)
                .HasConstraintName("FK__Flight__To__31EC6D26");
        });

        modelBuilder.Entity<FlightClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FlightCl__3214EC07898C4D6E");

            entity.ToTable("FlightClass");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Class).HasMaxLength(50);
            entity.Property(e => e.FlightId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Flight).WithMany(p => p.FlightClasses)
                .HasForeignKey(d => d.FlightId)
                .HasConstraintName("FK__FlightCla__Fligh__34C8D9D1");
        });

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Passenge__3214EC07988D1FA8");

            entity.ToTable("Passenger");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Country).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.User).WithMany(p => p.Passengers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Passenger__UserI__37A5467C");
        });

        modelBuilder.Entity<PassengerOfBooking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Passenge__3214EC0735083FD1");

            entity.ToTable("PassengerOfBooking");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.BookingId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PassengerId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Booking).WithMany(p => p.PassengerOfBookings)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__Passenger__Booki__403A8C7D");

            entity.HasOne(d => d.Passenger).WithMany(p => p.PassengerOfBookings)
                .HasForeignKey(d => d.PassengerId)
                .HasConstraintName("FK__Passenger__Passe__3F466844");
        });

        modelBuilder.Entity<PaymentRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentR__3214EC07F2E0A50C");

            entity.ToTable("PaymentRecord");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.BookingId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Discount).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.FinalPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PayDate).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Booking).WithMany(p => p.PaymentRecords)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__PaymentRe__Booki__4316F928");

            entity.HasOne(d => d.User).WithMany(p => p.PaymentRecords)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__PaymentRe__UserI__440B1D61");
        });

        modelBuilder.Entity<Rank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rank__3214EC0769CAD3EA");

            entity.ToTable("Rank");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Discount).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Type).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07A9467254");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "IDX_User_Email");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Avatar).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.RankId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Rank).WithMany(p => p.Users)
                .HasForeignKey(d => d.RankId)
                .HasConstraintName("FK__User__RankId__2A4B4B5E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
