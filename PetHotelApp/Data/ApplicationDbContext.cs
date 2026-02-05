using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PetHotelApp.Models.DBObjects;

namespace PetHotelApp.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Animal> Animals { get; set; } = null!;
        public virtual DbSet<Owner> Owners { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<RoomAllocation> RoomAllocations { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Animal>(entity =>
            {
                entity.HasKey(e => e.IdAnimal);

                entity.ToTable("Animal");

                entity.Property(e => e.IdAnimal)
                    .ValueGeneratedNever()
                    .HasColumnName("idAnimal");

                entity.Property(e => e.Breed)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("breed");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("dateOfBirth");

                entity.Property(e => e.IdOwner).HasColumnName("idOwner");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Notes)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("notes");

                entity.Property(e => e.Photo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("photo");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Animals)
                    .HasForeignKey(d => d.IdOwner)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Animal_Owner");
            });

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.HasKey(e => e.IdOwner);

                entity.ToTable("Owner");

                entity.Property(e => e.IdOwner)
                    .ValueGeneratedNever()
                    .HasColumnName("idOwner");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lastName");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phoneNumber");

                entity.HasMany(o => o.Animals)
                      .WithOne(a => a.Owner)
                      .HasForeignKey(a => a.IdOwner)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.IdReservation);

                entity.ToTable("Reservation");

                entity.Property(e => e.IdReservation)
                    .ValueGeneratedNever()
                    .HasColumnName("idReservation");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("endDate");

                entity.Property(e => e.IdAnimal).HasColumnName("idAnimal");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("startDate");

                entity.Property(e => e.Status)
                    .HasConversion<string>()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.HasOne(d => d.Animal)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.IdAnimal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_Animal");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.IdRoom);

                entity.ToTable("Room");

                entity.Property(e => e.IdRoom)
                    .ValueGeneratedNever()
                    .HasColumnName("idRoom");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.PricePerDay)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("price_per_day");

                entity.Property(e => e.RoomType)
                    .HasMaxLength(50)
                    .HasColumnName("room_type");
            });

            modelBuilder.Entity<RoomAllocation>(entity =>
            {
                entity.HasKey(e => e.IdAllocation);

                entity.ToTable("RoomAllocation");

                entity.Property(e => e.IdAllocation)
                    .ValueGeneratedNever()
                    .HasColumnName("idAllocation");

                entity.Property(e => e.CheckInDate)
                    .HasColumnType("date")
                    .HasColumnName("checkInDate");

                entity.Property(e => e.CheckOutDate)
                    .HasColumnType("date")
                    .HasColumnName("checkOutDate");

                entity.Property(e => e.IdAnimal).HasColumnName("idAnimal");

                entity.Property(e => e.IdRoom).HasColumnName("idRoom");

                entity.HasOne(d => d.IdAnimalNavigation)
                    .WithMany(p => p.RoomAllocations)
                    .HasForeignKey(d => d.IdAnimal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomAllocation_Animal");

                entity.HasOne(d => d.IdRoomNavigation)
                    .WithMany(p => p.RoomAllocations)
                    .HasForeignKey(d => d.IdRoom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomAllocation_Room");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
