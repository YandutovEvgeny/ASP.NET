﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PromoCodeFactory.DataAccess;

#nullable disable

namespace PromoCodeFactory.DataAccess.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.Administration.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<int>("AppliedPromocodesCount")
                        .HasColumnType("INTEGER")
                        .HasColumnName("applied_promocodes_count");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("last_name");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("TEXT")
                        .HasColumnName("role_id");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.Administration.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasMaxLength(150)
                        .HasColumnType("TEXT")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.PromoCodeManagement.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("last_name");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.PromoCodeManagement.CustomerPreference", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .HasColumnType("TEXT")
                        .HasColumnName("customer_id");

                    b.Property<Guid>("PreferenceId")
                        .HasColumnType("TEXT")
                        .HasColumnName("preference_id");

                    b.HasKey("CustomerId", "PreferenceId");

                    b.HasIndex("PreferenceId");

                    b.ToTable("CustomerPreference");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.PromoCodeManagement.Preference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("Preferences");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.PromoCodeManagement.PromoCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<DateTime>("BeginDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("begin_date");

                    b.Property<string>("Code")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("code");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("end_date");

                    b.Property<Guid>("PartnerId")
                        .HasColumnType("TEXT")
                        .HasColumnName("partner_id");

                    b.Property<Guid>("PartnerManagerId")
                        .HasColumnType("TEXT")
                        .HasColumnName("partner_manager_id");

                    b.Property<string>("PartnerName")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT")
                        .HasColumnName("partner_name");

                    b.Property<Guid?>("PreferenceId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ServiceInfo")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("servie_info");

                    b.HasKey("Id");

                    b.HasIndex("PartnerManagerId");

                    b.HasIndex("PreferenceId");

                    b.ToTable("PromoCodes");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.Administration.Employee", b =>
                {
                    b.HasOne("PromoCodeFactory.Core.Domain.Administration.Role", "Role")
                        .WithMany("Employees")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.PromoCodeManagement.CustomerPreference", b =>
                {
                    b.HasOne("PromoCodeFactory.Core.Domain.PromoCodeManagement.Customer", "Customer")
                        .WithMany("Preferences")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PromoCodeFactory.Core.Domain.PromoCodeManagement.Preference", "Preference")
                        .WithMany("Customers")
                        .HasForeignKey("PreferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Preference");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.PromoCodeManagement.PromoCode", b =>
                {
                    b.HasOne("PromoCodeFactory.Core.Domain.Administration.Employee", "PartnerManager")
                        .WithMany()
                        .HasForeignKey("PartnerManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PromoCodeFactory.Core.Domain.PromoCodeManagement.Preference", "Preference")
                        .WithMany()
                        .HasForeignKey("PreferenceId");

                    b.Navigation("PartnerManager");

                    b.Navigation("Preference");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.Administration.Role", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.PromoCodeManagement.Customer", b =>
                {
                    b.Navigation("Preferences");
                });

            modelBuilder.Entity("PromoCodeFactory.Core.Domain.PromoCodeManagement.Preference", b =>
                {
                    b.Navigation("Customers");
                });
#pragma warning restore 612, 618
        }
    }
}
