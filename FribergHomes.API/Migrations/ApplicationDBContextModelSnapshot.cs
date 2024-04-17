﻿// <auto-generated />
using System;
using FribergHomes.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FribergHomes.API.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FribergHomes.API.Models.Agency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Presentation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Agencies");
                });

            modelBuilder.Entity("FribergHomes.API.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("IconUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("FribergHomes.API.Models.County", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Counties");
                });

            modelBuilder.Entity("FribergHomes.API.Models.Realtor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AgencyId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AgencyId");

                    b.ToTable("Realtors");
                });

            modelBuilder.Entity("FribergHomes.API.Models.SalesObject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("AncillaryArea")
                        .HasColumnType("float");

                    b.Property<int>("BuildYear")
                        .HasColumnType("int");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ChangeDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ChangeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CountyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CurrentPrice")
                        .HasColumnType("int");

                    b.Property<string>("ImageLinks")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Level")
                        .HasColumnType("int");

                    b.Property<bool?>("Lift")
                        .HasColumnType("bit");

                    b.Property<int>("ListingPrice")
                        .HasColumnType("int");

                    b.Property<double>("LivingArea")
                        .HasColumnType("float");

                    b.Property<double?>("MonthlyFee")
                        .HasColumnType("float");

                    b.Property<string>("ObjectDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("PlotArea")
                        .HasColumnType("float");

                    b.Property<int?>("RealtorId")
                        .HasColumnType("int");

                    b.Property<int>("Rooms")
                        .HasColumnType("int");

                    b.Property<string>("ViewingDates")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("YearlyCost")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CountyId");

                    b.HasIndex("RealtorId");

                    b.ToTable("SalesObjects");
                });

            modelBuilder.Entity("FribergHomes.API.Models.Realtor", b =>
                {
                    b.HasOne("FribergHomes.API.Models.Agency", "Agency")
                        .WithMany("Realtors")
                        .HasForeignKey("AgencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agency");
                });

            modelBuilder.Entity("FribergHomes.API.Models.SalesObject", b =>
                {
                    b.HasOne("FribergHomes.API.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("FribergHomes.API.Models.County", "County")
                        .WithMany()
                        .HasForeignKey("CountyId");

                    b.HasOne("FribergHomes.API.Models.Realtor", "Realtor")
                        .WithMany("SalesObjects")
                        .HasForeignKey("RealtorId");

                    b.Navigation("Category");

                    b.Navigation("County");

                    b.Navigation("Realtor");
                });

            modelBuilder.Entity("FribergHomes.API.Models.Agency", b =>
                {
                    b.Navigation("Realtors");
                });

            modelBuilder.Entity("FribergHomes.API.Models.Realtor", b =>
                {
                    b.Navigation("SalesObjects");
                });
#pragma warning restore 612, 618
        }
    }
}
