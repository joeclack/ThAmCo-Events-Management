﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThAmCo.Events.Data;

#nullable disable

namespace ThAmCo.Events.Data.Migrations
{
    [DbContext(typeof(EventsDbContext))]
    partial class EventsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("ThAmCo.Events.Models.Events", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("EventTypeId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("FoodBookingId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ReservationId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("EventId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("ThAmCo.Events.Models.Guests", b =>
                {
                    b.Property<int>("GuestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("GuestId");

                    b.ToTable("Guests");

                    b.HasData(
                        new
                        {
                            GuestId = 1,
                            Email = "alice.green@example.com",
                            FirstName = "Alice",
                            LastName = "Green"
                        },
                        new
                        {
                            GuestId = 2,
                            Email = "bob.smith@example.com",
                            FirstName = "Bob",
                            LastName = "Smith"
                        },
                        new
                        {
                            GuestId = 3,
                            Email = "carol.johnson@example.com",
                            FirstName = "Carol",
                            LastName = "Johnson"
                        },
                        new
                        {
                            GuestId = 4,
                            Email = "david.brown@example.com",
                            FirstName = "David",
                            LastName = "Brown"
                        },
                        new
                        {
                            GuestId = 5,
                            Email = "ella.davis@example.com",
                            FirstName = "Ella",
                            LastName = "Davis"
                        },
                        new
                        {
                            GuestId = 6,
                            Email = "frank.wilson@example.com",
                            FirstName = "Frank",
                            LastName = "Wilson"
                        },
                        new
                        {
                            GuestId = 7,
                            Email = "grace.martinez@example.com",
                            FirstName = "Grace",
                            LastName = "Martinez"
                        },
                        new
                        {
                            GuestId = 8,
                            Email = "henry.anderson@example.com",
                            FirstName = "Henry",
                            LastName = "Anderson"
                        },
                        new
                        {
                            GuestId = 9,
                            Email = "ivy.thomas@example.com",
                            FirstName = "Ivy",
                            LastName = "Thomas"
                        },
                        new
                        {
                            GuestId = 10,
                            Email = "jack.moore@example.com",
                            FirstName = "Jack",
                            LastName = "Moore"
                        });
                });

            modelBuilder.Entity("ThAmCo.Events.Models.GuestBooking", b =>
                {
                    b.Property<int>("GuestId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EventId")
                        .HasColumnType("INTEGER");

                    b.HasKey("GuestId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("GuestBookings");
                });

            modelBuilder.Entity("ThAmCo.Events.Models.StaffMembers", b =>
                {
                    b.Property<int>("StaffId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("StaffId");

                    b.ToTable("StaffMembers");

                    b.HasData(
                        new
                        {
                            StaffId = 1,
                            FirstName = "John",
                            LastName = "Doe",
                            Role = "Chef"
                        },
                        new
                        {
                            StaffId = 2,
                            FirstName = "Jane",
                            LastName = "Smith",
                            Role = "Server"
                        },
                        new
                        {
                            StaffId = 3,
                            FirstName = "Emily",
                            LastName = "Johnson",
                            Role = "Manager"
                        },
                        new
                        {
                            StaffId = 4,
                            FirstName = "Michael",
                            LastName = "Williams",
                            Role = "Chef"
                        },
                        new
                        {
                            StaffId = 5,
                            FirstName = "Sarah",
                            LastName = "Brown",
                            Role = "Server"
                        },
                        new
                        {
                            StaffId = 6,
                            FirstName = "David",
                            LastName = "Jones",
                            Role = "Bartender"
                        },
                        new
                        {
                            StaffId = 7,
                            FirstName = "Laura",
                            LastName = "Garcia",
                            Role = "Host"
                        },
                        new
                        {
                            StaffId = 8,
                            FirstName = "Daniel",
                            LastName = "Martinez",
                            Role = "Sous Chef"
                        },
                        new
                        {
                            StaffId = 9,
                            FirstName = "Sophia",
                            LastName = "Anderson",
                            Role = "Server"
                        },
                        new
                        {
                            StaffId = 10,
                            FirstName = "James",
                            LastName = "Taylor",
                            Role = "Manager"
                        });
                });

            modelBuilder.Entity("ThAmCo.Events.Models.Staffing", b =>
                {
                    b.Property<int>("EventId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StaffId")
                        .HasColumnType("INTEGER");

                    b.HasKey("EventId", "StaffId");

                    b.HasIndex("StaffId");

                    b.ToTable("Staffing");
                });

            modelBuilder.Entity("ThAmCo.Events.Models.GuestBooking", b =>
                {
                    b.HasOne("ThAmCo.Events.Models.Events", "Events")
                        .WithMany("GuestBookings")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ThAmCo.Events.Models.Guests", "Guests")
                        .WithMany("GuestBookings")
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Events");

                    b.Navigation("Guests");
                });

            modelBuilder.Entity("ThAmCo.Events.Models.Staffing", b =>
                {
                    b.HasOne("ThAmCo.Events.Models.Events", "Events")
                        .WithMany("Staffings")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ThAmCo.Events.Models.StaffMembers", "StaffMembers")
                        .WithMany("Staffings")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Events");

                    b.Navigation("StaffMembers");
                });

            modelBuilder.Entity("ThAmCo.Events.Models.Events", b =>
                {
                    b.Navigation("GuestBookings");

                    b.Navigation("Staffings");
                });

            modelBuilder.Entity("ThAmCo.Events.Models.Guests", b =>
                {
                    b.Navigation("GuestBookings");
                });

            modelBuilder.Entity("ThAmCo.Events.Models.StaffMembers", b =>
                {
                    b.Navigation("Staffings");
                });
#pragma warning restore 612, 618
        }
    }
}
