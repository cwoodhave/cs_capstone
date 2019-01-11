﻿// <auto-generated />
using System;
using CCS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CCS.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20181205102918_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CCS.Models.Address", b =>
                {
                    b.Property<int>("AddressID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityName");

                    b.Property<string>("StateShortName");

                    b.Property<string>("StreetAddress1");

                    b.Property<string>("StreetAddress2");

                    b.Property<string>("Zipcode");

                    b.HasKey("AddressID");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("CCS.Models.Adjustment", b =>
                {
                    b.Property<int>("AdjustmentID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CCSUserID");

                    b.Property<int>("Cases");

                    b.Property<string>("FoodCategory");

                    b.Property<bool>("IsUSDA");

                    b.Property<string>("Location");

                    b.Property<string>("USDANumber");

                    b.Property<string>("UserId");

                    b.Property<decimal>("Weight");

                    b.HasKey("AdjustmentID");

                    b.HasIndex("UserId");

                    b.ToTable("Adjustment");
                });

            modelBuilder.Entity("CCS.Models.Agency", b =>
                {
                    b.Property<int>("AgencyID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressID");

                    b.Property<string>("AgencyName");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDonor");

                    b.HasKey("AgencyID");

                    b.HasIndex("AddressID");

                    b.ToTable("Agencies");
                });

            modelBuilder.Entity("CCS.Models.CCSUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<int>("IsAdmin");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserIDName")
                        .IsRequired();

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<string>("UserPassword")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("CCS.Models.Container", b =>
                {
                    b.Property<int>("ContainerID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BinNumber");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description");

                    b.Property<int>("FoodCategoryID");

                    b.Property<int>("LocationID");

                    b.Property<decimal>("Weight");

                    b.HasKey("ContainerID");

                    b.HasIndex("FoodCategoryID");

                    b.HasIndex("LocationID");

                    b.ToTable("Container");
                });

            modelBuilder.Entity("CCS.Models.DonationTransaction", b =>
                {
                    b.Property<int>("DonationTransactionID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AgencyID");

                    b.Property<int>("FoodCategory");

                    b.Property<DateTime>("TimeStamp");

                    b.Property<int>("TransactionType");

                    b.HasKey("DonationTransactionID");

                    b.HasIndex("AgencyID");

                    b.ToTable("DonationTransactions");
                });

            modelBuilder.Entity("CCS.Models.DonationType", b =>
                {
                    b.Property<int>("DonationTypeID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type");

                    b.HasKey("DonationTypeID");

                    b.ToTable("DonationTypes");
                });

            modelBuilder.Entity("CCS.Models.FoodCategory", b =>
                {
                    b.Property<int>("FoodCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int>("FoodCategoryType");

                    b.HasKey("FoodCategoryID");

                    b.ToTable("FoodCategories");
                });

            modelBuilder.Entity("CCS.Models.Location", b =>
                {
                    b.Property<int>("LocationID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoomName");

                    b.HasKey("LocationID");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("CCS.Models.Log", b =>
                {
                    b.Property<int>("LogID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CCSUserID");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("UserId");

                    b.HasKey("LogID");

                    b.HasIndex("UserId");

                    b.ToTable("Log");
                });

            modelBuilder.Entity("CCS.Models.TransactionLineItem", b =>
                {
                    b.Property<int>("TransactionLineItemID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Cases");

                    b.Property<int>("DonationTransactionID");

                    b.Property<int>("DonationTypeID");

                    b.Property<int>("FoodCategoryID");

                    b.Property<int?>("Quantity");

                    b.Property<decimal>("Weight");

                    b.HasKey("TransactionLineItemID");

                    b.HasIndex("DonationTransactionID");

                    b.HasIndex("DonationTypeID");

                    b.HasIndex("FoodCategoryID");

                    b.ToTable("TransactionLineItems");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CCS.Models.Adjustment", b =>
                {
                    b.HasOne("CCS.Models.CCSUser", "User")
                        .WithMany("Adjustments")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CCS.Models.Agency", b =>
                {
                    b.HasOne("CCS.Models.Address", "Address")
                        .WithMany("Agencies")
                        .HasForeignKey("AddressID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CCS.Models.Container", b =>
                {
                    b.HasOne("CCS.Models.FoodCategory", "FoodCategory")
                        .WithMany("Containers")
                        .HasForeignKey("FoodCategoryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CCS.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CCS.Models.DonationTransaction", b =>
                {
                    b.HasOne("CCS.Models.Agency", "Agency")
                        .WithMany("DonationTransactions")
                        .HasForeignKey("AgencyID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CCS.Models.Log", b =>
                {
                    b.HasOne("CCS.Models.CCSUser", "User")
                        .WithMany("Logs")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CCS.Models.TransactionLineItem", b =>
                {
                    b.HasOne("CCS.Models.DonationTransaction", "DonationTransaction")
                        .WithMany("TransactionLineItems")
                        .HasForeignKey("DonationTransactionID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CCS.Models.DonationType", "DonationType")
                        .WithMany("TransactionLineItems")
                        .HasForeignKey("DonationTypeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CCS.Models.FoodCategory", "FoodCategory")
                        .WithMany("TransactionLineItems")
                        .HasForeignKey("FoodCategoryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CCS.Models.CCSUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CCS.Models.CCSUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CCS.Models.CCSUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CCS.Models.CCSUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
