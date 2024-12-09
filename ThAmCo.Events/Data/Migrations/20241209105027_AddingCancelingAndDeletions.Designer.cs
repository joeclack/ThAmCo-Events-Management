﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThAmCo.Events.Data;

#nullable disable

namespace ThAmCo.Events.Data.Migrations
{
    [DbContext(typeof(EventsDbContext))]
    [Migration("20241209105027_AddingCancelingAndDeletions")]
    partial class AddingCancelingAndDeletions
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("ThAmCo.Events.Models.Event", b =>
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

                    b.Property<bool>("IsCanceled")
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

            modelBuilder.Entity("ThAmCo.Events.Models.Guest", b =>
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

                    b.Property<bool>("IsAnonymised")
                        .HasColumnType("INTEGER");

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
                            IsAnonymised = false,
                            LastName = "Green"
                        },
                        new
                        {
                            GuestId = 2,
                            Email = "bob.smith@example.com",
                            FirstName = "Bob",
                            IsAnonymised = false,
                            LastName = "Smith"
                        },
                        new
                        {
                            GuestId = 3,
                            Email = "carol.johnson@example.com",
                            FirstName = "Carol",
                            IsAnonymised = false,
                            LastName = "Johnson"
                        },
                        new
                        {
                            GuestId = 4,
                            Email = "david.brown@example.com",
                            FirstName = "David",
                            IsAnonymised = false,
                            LastName = "Brown"
                        },
                        new
                        {
                            GuestId = 5,
                            Email = "ella.davis@example.com",
                            FirstName = "Ella",
                            IsAnonymised = false,
                            LastName = "Davis"
                        },
                        new
                        {
                            GuestId = 6,
                            Email = "frank.wilson@example.com",
                            FirstName = "Frank",
                            IsAnonymised = false,
                            LastName = "Wilson"
                        },
                        new
                        {
                            GuestId = 7,
                            Email = "grace.martinez@example.com",
                            FirstName = "Grace",
                            IsAnonymised = false,
                            LastName = "Martinez"
                        },
                        new
                        {
                            GuestId = 8,
                            Email = "henry.anderson@example.com",
                            FirstName = "Henry",
                            IsAnonymised = false,
                            LastName = "Anderson"
                        },
                        new
                        {
                            GuestId = 9,
                            Email = "ivy.thomas@example.com",
                            FirstName = "Ivy",
                            IsAnonymised = false,
                            LastName = "Thomas"
                        },
                        new
                        {
                            GuestId = 10,
                            Email = "jack.moore@example.com",
                            FirstName = "Jack",
                            IsAnonymised = false,
                            LastName = "Moore"
                        });
                });

            modelBuilder.Entity("ThAmCo.Events.Models.GuestBooking", b =>
                {
                    b.Property<int>("GuestId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EventId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsCancled")
                        .HasColumnType("INTEGER");

                    b.HasKey("GuestId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("GuestBookings");
                });

            modelBuilder.Entity("ThAmCo.Events.Models.Staff", b =>
                {
                    b.Property<int>("StaffId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAnonymised")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsFirstAider")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("StaffId");

                    b.ToTable("Staff");

                    b.HasData(
                        new
                        {
                            StaffId = 1,
                            FirstName = "John",
                            IsAnonymised = false,
                            IsFirstAider = false,
                            LastName = "Doe",
                            Role = "Chef"
                        },
                        new
                        {
                            StaffId = 2,
                            FirstName = "Jane",
                            IsAnonymised = false,
                            IsFirstAider = false,
                            LastName = "Smith",
                            Role = "Server"
                        },
                        new
                        {
                            StaffId = 3,
                            FirstName = "Emily",
                            IsAnonymised = false,
                            IsFirstAider = true,
                            LastName = "Johnson",
                            Role = "Manager"
                        },
                        new
                        {
                            StaffId = 4,
                            FirstName = "Michael",
                            IsAnonymised = false,
                            IsFirstAider = true,
                            LastName = "Williams",
                            Role = "Chef"
                        },
                        new
                        {
                            StaffId = 5,
                            FirstName = "Sarah",
                            IsAnonymised = false,
                            IsFirstAider = true,
                            LastName = "Brown",
                            Role = "Server"
                        },
                        new
                        {
                            StaffId = 6,
                            FirstName = "David",
                            IsAnonymised = false,
                            IsFirstAider = false,
                            LastName = "Jones",
                            Role = "Bartender"
                        },
                        new
                        {
                            StaffId = 7,
                            FirstName = "Laura",
                            IsAnonymised = false,
                            IsFirstAider = false,
                            LastName = "Garcia",
                            Role = "Host"
                        },
                        new
                        {
                            StaffId = 8,
                            FirstName = "Daniel",
                            IsAnonymised = false,
                            IsFirstAider = true,
                            LastName = "Martinez",
                            Role = "Sous Chef"
                        },
                        new
                        {
                            StaffId = 9,
                            FirstName = "Sophia",
                            IsAnonymised = false,
                            IsFirstAider = true,
                            LastName = "Anderson",
                            Role = "Server"
                        },
                        new
                        {
                            StaffId = 10,
                            FirstName = "James",
                            IsAnonymised = false,
                            IsFirstAider = true,
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

                    b.Property<bool>("IsCancled")
                        .HasColumnType("INTEGER");

                    b.HasKey("EventId", "StaffId");

                    b.HasIndex("StaffId");

                    b.ToTable("Staffing");
                });

            modelBuilder.Entity("ThAmCo.Events.Models.GuestBooking", b =>
                {
                    b.HasOne("ThAmCo.Events.Models.Event", "Event")
                        .WithMany("GuestBookings")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ThAmCo.Events.Models.Guest", "Guest")
                        .WithMany("GuestBookings")
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Guest");
                });

            modelBuilder.Entity("ThAmCo.Events.Models.Staffing", b =>
                {
                    b.HasOne("ThAmCo.Events.Models.Event", "Event")
                        .WithMany("Staffings")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ThAmCo.Events.Models.Staff", "Staff")
                        .WithMany("Staffings")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("ThAmCo.Events.Models.Event", b =>
                {
                    b.Navigation("GuestBookings");

                    b.Navigation("Staffings");
                });

            modelBuilder.Entity("ThAmCo.Events.Models.Guest", b =>
                {
                    b.Navigation("GuestBookings");
                });

            modelBuilder.Entity("ThAmCo.Events.Models.Staff", b =>
                {
                    b.Navigation("Staffings");
                });
#pragma warning restore 612, 618
        }
    }
}
