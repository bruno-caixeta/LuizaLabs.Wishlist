﻿// <auto-generated />
using System;
using LuizaLabs.Wishlist.Domain.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LuizaLabs.Wishlist.Domain.Migrations
{
    [DbContext(typeof(PostgresContext))]
    [Migration("20210113152011_V1_InitialMigration")]
    partial class V1_InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Wishlist")
                .UseSerialColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("LuizaLabs.Wishlist.Domain.Entities.Client", b =>
                {
                    b.Property<Guid>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("ClientId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("LuizaLabs.Wishlist.Domain.Entities.Favorite", b =>
                {
                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.HasKey("ClientId");

                    b.HasIndex("ClientId", "ProductId")
                        .IsUnique();

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("LuizaLabs.Wishlist.Domain.Entities.Favorite", b =>
                {
                    b.HasOne("LuizaLabs.Wishlist.Domain.Entities.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });
#pragma warning restore 612, 618
        }
    }
}
