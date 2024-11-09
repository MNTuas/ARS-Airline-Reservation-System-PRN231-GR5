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

    public virtual DbSet<AirplaneSeat> AirplaneSeats { get; set; }

    public virtual DbSet<Airport> Airports { get; set; }

    public virtual DbSet<BookingInformation> BookingInformations { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<Passenger> Passengers { get; set; }

    public virtual DbSet<Rank> Ranks { get; set; }

    public virtual DbSet<RefundBankAccount> RefundBankAccounts { get; set; }

    public virtual DbSet<SeatClass> SeatClasses { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketClass> TicketClasses { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

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
            entity.HasKey(e => e.Id).HasName("PK__Airlines__3214EC071C774312");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Airplane>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Airplane__3214EC079DDD9D3F");

            entity.ToTable("Airplane");

            entity.HasIndex(e => e.AirlinesId, "IDX_FK_Airplane_Airlines");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AirlinesId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CodeNumber).HasMaxLength(50);

            entity.HasOne(d => d.Airlines).WithMany(p => p.Airplanes)
                .HasForeignKey(d => d.AirlinesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Airplane_Airlines");
        });

        modelBuilder.Entity<AirplaneSeat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Airplane__3214EC074C61D289");

            entity.ToTable("AirplaneSeat");

            entity.HasIndex(e => e.AirplaneId, "IDX_FK_AirplaneSeat_Airplane");

            entity.HasIndex(e => e.SeatClassId, "IDX_FK_AirplaneSeat_SeatClass");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AirplaneId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SeatClassId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Airplane).WithMany(p => p.AirplaneSeats)
                .HasForeignKey(d => d.AirplaneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AirplaneSeat_Airplane");

            entity.HasOne(d => d.SeatClass).WithMany(p => p.AirplaneSeats)
                .HasForeignKey(d => d.SeatClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AirplaneSeat_SeatClass");
        });

        modelBuilder.Entity<Airport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Airport__3214EC07743BAF4E");

            entity.ToTable("Airport");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<BookingInformation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookingI__3214EC07A2E4AB37");

            entity.ToTable("BookingInformation");

            entity.HasIndex(e => e.UserId, "IDX_FK_BookingInformation_User");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CancelDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.User).WithMany(p => p.BookingInformations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookingInformation_User");
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Flight__3214EC07A0A2C2BE");

            entity.ToTable("Flight");

            entity.HasIndex(e => e.AirplaneId, "IDX_FK_Flight_Airplane");

            entity.HasIndex(e => e.From, "IDX_FK_Flight_Airport_From");

            entity.HasIndex(e => e.To, "IDX_FK_Flight_Airport_To");

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
            entity.Property(e => e.FlightNumber).HasMaxLength(50);
            entity.Property(e => e.From)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.To)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Airplane).WithMany(p => p.Flights)
                .HasForeignKey(d => d.AirplaneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Flight_Airplane");

            entity.HasOne(d => d.FromNavigation).WithMany(p => p.FlightFromNavigations)
                .HasForeignKey(d => d.From)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ToNavigation).WithMany(p => p.FlightToNavigations)
                .HasForeignKey(d => d.To)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Passenge__3214EC074FD50069");

            entity.ToTable("Passenger");

            entity.HasIndex(e => e.UserId, "IDX_FK_Passenger_User");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.User).WithMany(p => p.Passengers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Passenger_User");
        });

        modelBuilder.Entity<Rank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rank__3214EC07DB09C6EE");

            entity.ToTable("Rank");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Discount).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<RefundBankAccount>(entity =>
        {
            entity.ToTable("RefundBankAccount");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AccountName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AccountNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BankName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BookingId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Booking).WithMany(p => p.RefundBankAccounts)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefundBankAccount_BookingInformation");
        });

        modelBuilder.Entity<SeatClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SeatClas__3214EC07CB9003FE");

            entity.ToTable("SeatClass");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ticket__3214EC0723F9FC1C");

            entity.ToTable("Ticket");

            entity.HasIndex(e => e.BookingId, "IDX_FK_Ticket_BookingInformation");

            entity.HasIndex(e => e.TicketClassId, "IDX_FK_Ticket_TicketClass");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.BookingId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TicketClassId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Booking).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_BookingInformation");

            entity.HasOne(d => d.TicketClass).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.TicketClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_TicketClass");
        });

        modelBuilder.Entity<TicketClass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TicketCl__3214EC07F0F9CC3D");

            entity.ToTable("TicketClass");

            entity.HasIndex(e => e.FlightId, "IDX_FK_TicketClass_Flight");

            entity.HasIndex(e => e.SeatClassId, "IDX_FK_TicketClass_SeatClass");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FlightId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SeatClassId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Flight).WithMany(p => p.TicketClasses)
                .HasForeignKey(d => d.FlightId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TicketClass_Flight");

            entity.HasOne(d => d.SeatClass).WithMany(p => p.TicketClasses)
                .HasForeignKey(d => d.SeatClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TicketClass_SeatClass");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC072F6C7077");

            entity.ToTable("Transaction");

            entity.HasIndex(e => e.BookingId, "IDX_FK_Transaction_BookingInformation");

            entity.HasIndex(e => e.UserId, "IDX_FK_Transaction_User");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.BookingId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FinalPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PayDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Booking).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transaction_BookingInformation");

            entity.HasOne(d => d.User).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transaction_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07FFD92900");

            entity.ToTable("User");

            entity.HasIndex(e => e.RankId, "IDX_FK_User_Rank");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.RankId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Rank).WithMany(p => p.Users)
                .HasForeignKey(d => d.RankId)
                .HasConstraintName("FK_User_Rank");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
