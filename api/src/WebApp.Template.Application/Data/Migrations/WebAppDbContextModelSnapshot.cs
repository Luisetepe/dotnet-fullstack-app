﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApp.Template.Application.Data.DbContexts;

#nullable disable

namespace WebApp.Template.Application.Data.Migrations
{
    [DbContext(typeof(WebAppDbContext))]
    partial class WebAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AppRouteUserRole", b =>
                {
                    b.Property<string>("RoleRoutesId")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("role_routes_id");

                    b.Property<string>("UserRolesId")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("user_roles_id");

                    b.HasKey("RoleRoutesId", "UserRolesId")
                        .HasName("pk_app_route_user_role");

                    b.HasIndex("UserRolesId")
                        .HasDatabaseName("ix_app_route_user_role_user_roles_id");

                    b.ToTable("app_route_user_role", (string)null);
                });

            modelBuilder.Entity("PlantPortfolio", b =>
                {
                    b.Property<string>("PlantsId")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("plants_id");

                    b.Property<string>("PortfoliosId")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("portfolios_id");

                    b.HasKey("PlantsId", "PortfoliosId")
                        .HasName("pk_plant_portfolio");

                    b.HasIndex("PortfoliosId")
                        .HasDatabaseName("ix_plant_portfolio_portfolios_id");

                    b.ToTable("plant_portfolio", (string)null);
                });

            modelBuilder.Entity("WebApp.Template.Application.Data.DbEntities.Identity.AppRoute", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("path");

                    b.HasKey("Id")
                        .HasName("pk_app_routes");

                    b.HasIndex("Path")
                        .IsUnique()
                        .HasDatabaseName("ix_app_routes_path");

                    b.ToTable("app_routes", (string)null);
                });

            modelBuilder.Entity("WebApp.Template.Application.Data.DbEntities.Identity.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("password_hash");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("password_salt");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("role_id");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("user_name");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_users_email");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_users_role_id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("WebApp.Template.Application.Data.DbEntities.Identity.UserRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_user_roles");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_user_roles_name");

                    b.ToTable("user_roles", (string)null);
                });

            modelBuilder.Entity("WebApp.Template.Application.Data.DbEntities.Identity.UserSession", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamptz")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("timestamptz")
                        .HasColumnName("expires_at");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_sessions");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_sessions_user_id");

                    b.ToTable("user_sessions", (string)null);
                });

            modelBuilder.Entity("WebApp.Template.Application.Data.DbEntities.Location", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("id");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("numeric")
                        .HasColumnName("latitude");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("numeric")
                        .HasColumnName("longitude");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_locations");

                    b.ToTable("locations", (string)null);
                });

            modelBuilder.Entity("WebApp.Template.Application.Data.DbEntities.Plant", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("id");

                    b.Property<string>("AssetManager")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("asset_manager");

                    b.Property<decimal>("CapacityAc")
                        .HasColumnType("numeric")
                        .HasColumnName("capacity_ac");

                    b.Property<decimal>("CapacityDc")
                        .HasColumnType("numeric")
                        .HasColumnName("capacity_dc");

                    b.Property<string>("LocationId")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("location_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("varchar(500)")
                        .HasColumnName("notes");

                    b.Property<string>("PlantId")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("plant_id");

                    b.Property<string>("PlantTypeId")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("plant_type_id");

                    b.Property<string>("ProjectCompany")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("project_company");

                    b.Property<string>("ResourceTypeId")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("resource_type_id");

                    b.Property<string>("StatusId")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("status_id");

                    b.Property<decimal>("StorageCapacity")
                        .HasColumnType("numeric")
                        .HasColumnName("storage_capacity");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("tags");

                    b.Property<string>("UtilityCompany")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("utility_company");

                    b.Property<int>("Voltage")
                        .HasColumnType("integer")
                        .HasColumnName("voltage");

                    b.HasKey("Id")
                        .HasName("pk_plants");

                    b.HasIndex("LocationId")
                        .HasDatabaseName("ix_plants_location_id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_plants_name");

                    b.HasIndex("PlantTypeId")
                        .HasDatabaseName("ix_plants_plant_type_id");

                    b.HasIndex("ResourceTypeId")
                        .HasDatabaseName("ix_plants_resource_type_id");

                    b.HasIndex("StatusId")
                        .HasDatabaseName("ix_plants_status_id");

                    b.ToTable("plants", (string)null);
                });

            modelBuilder.Entity("WebApp.Template.Application.Data.DbEntities.PlantStatus", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_plant_statuses");

                    b.ToTable("plant_statuses", (string)null);
                });

            modelBuilder.Entity("WebApp.Template.Application.Data.DbEntities.PlantType", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_plant_types");

                    b.ToTable("plant_types", (string)null);
                });

            modelBuilder.Entity("WebApp.Template.Application.Data.DbEntities.Portfolio", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_portfolios");

                    b.ToTable("portfolios", (string)null);
                });

            modelBuilder.Entity("WebApp.Template.Application.Data.DbEntities.ResourceType", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_resource_types");

                    b.ToTable("resource_types", (string)null);
                });

            modelBuilder.Entity("AppRouteUserRole", b =>
                {
                    b.HasOne("WebApp.Template.Application.Data.DbEntities.Identity.AppRoute", null)
                        .WithMany()
                        .HasForeignKey("RoleRoutesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_app_route_user_role_app_routes_role_routes_id");

                    b.HasOne("WebApp.Template.Application.Data.DbEntities.Identity.UserRole", null)
                        .WithMany()
                        .HasForeignKey("UserRolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_app_route_user_role_user_roles_user_roles_id");
                });

            modelBuilder.Entity("PlantPortfolio", b =>
                {
                    b.HasOne("WebApp.Template.Application.Data.DbEntities.Plant", null)
                        .WithMany()
                        .HasForeignKey("PlantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_plant_portfolio_plants_plants_id");

                    b.HasOne("WebApp.Template.Application.Data.DbEntities.Portfolio", null)
                        .WithMany()
                        .HasForeignKey("PortfoliosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_plant_portfolio_portfolios_portfolios_id");
                });

            modelBuilder.Entity("WebApp.Template.Application.Data.DbEntities.Identity.User", b =>
                {
                    b.HasOne("WebApp.Template.Application.Data.DbEntities.Identity.UserRole", "UserRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_users_user_roles_role_id");

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("WebApp.Template.Application.Data.DbEntities.Identity.UserSession", b =>
                {
                    b.HasOne("WebApp.Template.Application.Data.DbEntities.Identity.User", "User")
                        .WithMany("UserSessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_sessions_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApp.Template.Application.Data.DbEntities.Plant", b =>
                {
                    b.HasOne("WebApp.Template.Application.Data.DbEntities.Location", "Location")
                        .WithMany("Plants")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_plants_locations_location_id");

                    b.HasOne("WebApp.Template.Application.Data.DbEntities.PlantType", "PlantType")
                        .WithMany("Plants")
                        .HasForeignKey("PlantTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_plants_plant_types_plant_type_id");

                    b.HasOne("WebApp.Template.Application.Data.DbEntities.ResourceType", "ResourceType")
                        .WithMany("Plants")
                        .HasForeignKey("ResourceTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_plants_resource_types_resource_type_id");

                    b.HasOne("WebApp.Template.Application.Data.DbEntities.PlantStatus", "Status")
                        .WithMany("Plants")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_plants_plant_statuses_status_id");

                    b.Navigation("Location");

                    b.Navigation("PlantType");

                    b.Navigation("ResourceType");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("WebApp.Template.Application.Data.DbEntities.Identity.User", b =>
                {
                    b.Navigation("UserSessions");
                });

            modelBuilder.Entity("WebApp.Template.Application.Data.DbEntities.Identity.UserRole", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("WebApp.Template.Application.Data.DbEntities.Location", b =>
                {
                    b.Navigation("Plants");
                });

            modelBuilder.Entity("WebApp.Template.Application.Data.DbEntities.PlantStatus", b =>
                {
                    b.Navigation("Plants");
                });

            modelBuilder.Entity("WebApp.Template.Application.Data.DbEntities.PlantType", b =>
                {
                    b.Navigation("Plants");
                });

            modelBuilder.Entity("WebApp.Template.Application.Data.DbEntities.ResourceType", b =>
                {
                    b.Navigation("Plants");
                });
#pragma warning restore 612, 618
        }
    }
}
