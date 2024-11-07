﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ThAmCo.Catering.Data.Migrations
{
    [DbContext(typeof(CateringDbContext))]
    partial class CateringDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("ThAmCo.Catering.Data.FoodBooking", b =>
                {
                    b.Property<int>("FoodBookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClientReferenceId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MenuId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberOfGuests")
                        .HasColumnType("INTEGER");

                    b.HasKey("FoodBookingId");

                    b.HasIndex("MenuId");

                    b.ToTable("FoodBookings");

                    b.HasData(
                        new
                        {
                            FoodBookingId = 1,
                            ClientReferenceId = 101,
                            MenuId = 1,
                            NumberOfGuests = 50
                        },
                        new
                        {
                            FoodBookingId = 2,
                            ClientReferenceId = 102,
                            MenuId = 2,
                            NumberOfGuests = 30
                        },
                        new
                        {
                            FoodBookingId = 3,
                            ClientReferenceId = 103,
                            MenuId = 3,
                            NumberOfGuests = 75
                        },
                        new
                        {
                            FoodBookingId = 4,
                            ClientReferenceId = 104,
                            MenuId = 4,
                            NumberOfGuests = 100
                        },
                        new
                        {
                            FoodBookingId = 5,
                            ClientReferenceId = 105,
                            MenuId = 5,
                            NumberOfGuests = 60
                        });
                });

            modelBuilder.Entity("ThAmCo.Catering.Data.FoodItem", b =>
                {
                    b.Property<int>("FoodItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<float>("UnitPrice")
                        .HasColumnType("REAL");

                    b.HasKey("FoodItemId");

                    b.ToTable("FoodItems");

                    b.HasData(
                        new
                        {
                            FoodItemId = 1,
                            Description = "Chicken Sandwich",
                            UnitPrice = 5.99f
                        },
                        new
                        {
                            FoodItemId = 2,
                            Description = "Veggie Wrap",
                            UnitPrice = 4.49f
                        },
                        new
                        {
                            FoodItemId = 3,
                            Description = "Caesar Salad",
                            UnitPrice = 3.99f
                        },
                        new
                        {
                            FoodItemId = 4,
                            Description = "Steak",
                            UnitPrice = 12.99f
                        },
                        new
                        {
                            FoodItemId = 5,
                            Description = "Turkey Club",
                            UnitPrice = 6.49f
                        },
                        new
                        {
                            FoodItemId = 6,
                            Description = "Ham and Cheese",
                            UnitPrice = 5.19f
                        },
                        new
                        {
                            FoodItemId = 7,
                            Description = "Grilled Cheese",
                            UnitPrice = 4.99f
                        },
                        new
                        {
                            FoodItemId = 8,
                            Description = "BLT Sandwich",
                            UnitPrice = 5.79f
                        },
                        new
                        {
                            FoodItemId = 9,
                            Description = "Fish Tacos",
                            UnitPrice = 7.99f
                        },
                        new
                        {
                            FoodItemId = 10,
                            Description = "Spaghetti Bolognese",
                            UnitPrice = 8.99f
                        },
                        new
                        {
                            FoodItemId = 11,
                            Description = "Penne Arrabbiata",
                            UnitPrice = 7.49f
                        },
                        new
                        {
                            FoodItemId = 12,
                            Description = "Mushroom Risotto",
                            UnitPrice = 9.49f
                        },
                        new
                        {
                            FoodItemId = 13,
                            Description = "Chicken Alfredo",
                            UnitPrice = 10.99f
                        },
                        new
                        {
                            FoodItemId = 14,
                            Description = "Beef Burritos",
                            UnitPrice = 6.99f
                        },
                        new
                        {
                            FoodItemId = 15,
                            Description = "Vegetarian Tacos",
                            UnitPrice = 5.49f
                        },
                        new
                        {
                            FoodItemId = 16,
                            Description = "BBQ Chicken Pizza",
                            UnitPrice = 12.99f
                        },
                        new
                        {
                            FoodItemId = 17,
                            Description = "Pepperoni Pizza",
                            UnitPrice = 11.49f
                        },
                        new
                        {
                            FoodItemId = 18,
                            Description = "Margherita Pizza",
                            UnitPrice = 9.99f
                        },
                        new
                        {
                            FoodItemId = 19,
                            Description = "Cheeseburger",
                            UnitPrice = 6.49f
                        },
                        new
                        {
                            FoodItemId = 20,
                            Description = "Veggie Burger",
                            UnitPrice = 5.99f
                        });
                });

            modelBuilder.Entity("ThAmCo.Catering.Data.Menu", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("MenuName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("MenuId");

                    b.ToTable("Menus");

                    b.HasData(
                        new
                        {
                            MenuId = 1,
                            MenuName = "Basic Lunch"
                        },
                        new
                        {
                            MenuId = 2,
                            MenuName = "Vegetarian Feast"
                        },
                        new
                        {
                            MenuId = 3,
                            MenuName = "Deluxe Dinner"
                        },
                        new
                        {
                            MenuId = 4,
                            MenuName = "Italian Specialties"
                        },
                        new
                        {
                            MenuId = 5,
                            MenuName = "Mexican Fiesta"
                        });
                });

            modelBuilder.Entity("ThAmCo.Catering.Data.MenuFoodItem", b =>
                {
                    b.Property<int>("MenuId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FoodItemId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MenuId", "FoodItemId");

                    b.HasIndex("FoodItemId");

                    b.ToTable("MenuFoodItems");

                    b.HasData(
                        new
                        {
                            MenuId = 1,
                            FoodItemId = 1
                        },
                        new
                        {
                            MenuId = 1,
                            FoodItemId = 3
                        },
                        new
                        {
                            MenuId = 1,
                            FoodItemId = 5
                        },
                        new
                        {
                            MenuId = 1,
                            FoodItemId = 6
                        },
                        new
                        {
                            MenuId = 2,
                            FoodItemId = 2
                        },
                        new
                        {
                            MenuId = 2,
                            FoodItemId = 3
                        },
                        new
                        {
                            MenuId = 2,
                            FoodItemId = 7
                        },
                        new
                        {
                            MenuId = 2,
                            FoodItemId = 15
                        },
                        new
                        {
                            MenuId = 3,
                            FoodItemId = 4
                        },
                        new
                        {
                            MenuId = 3,
                            FoodItemId = 10
                        },
                        new
                        {
                            MenuId = 3,
                            FoodItemId = 14
                        },
                        new
                        {
                            MenuId = 3,
                            FoodItemId = 13
                        },
                        new
                        {
                            MenuId = 4,
                            FoodItemId = 12
                        },
                        new
                        {
                            MenuId = 4,
                            FoodItemId = 16
                        },
                        new
                        {
                            MenuId = 4,
                            FoodItemId = 17
                        },
                        new
                        {
                            MenuId = 4,
                            FoodItemId = 18
                        },
                        new
                        {
                            MenuId = 5,
                            FoodItemId = 19
                        },
                        new
                        {
                            MenuId = 5,
                            FoodItemId = 20
                        },
                        new
                        {
                            MenuId = 5,
                            FoodItemId = 9
                        },
                        new
                        {
                            MenuId = 5,
                            FoodItemId = 15
                        });
                });

            modelBuilder.Entity("ThAmCo.Catering.Data.FoodBooking", b =>
                {
                    b.HasOne("ThAmCo.Catering.Data.Menu", null)
                        .WithMany("FoodBookings")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ThAmCo.Catering.Data.MenuFoodItem", b =>
                {
                    b.HasOne("ThAmCo.Catering.Data.FoodItem", null)
                        .WithMany("MenuFoodItems")
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ThAmCo.Catering.Data.Menu", null)
                        .WithMany("MenuFoodItems")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ThAmCo.Catering.Data.FoodItem", b =>
                {
                    b.Navigation("MenuFoodItems");
                });

            modelBuilder.Entity("ThAmCo.Catering.Data.Menu", b =>
                {
                    b.Navigation("FoodBookings");

                    b.Navigation("MenuFoodItems");
                });
#pragma warning restore 612, 618
        }
    }
}
