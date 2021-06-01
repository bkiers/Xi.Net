﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Xi.Database;

namespace Xi.Database.Migrations
{
    [DbContext(typeof(XiContext))]
    partial class XiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Xi.Database.Dtos.GameDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Accepted")
                        .HasColumnType("boolean");

                    b.Property<int?>("AcceptedDrawPlayerId")
                        .HasColumnType("integer");

                    b.Property<int>("BlackPlayerId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ClockRunsOutAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("EloRatingChange")
                        .HasColumnType("integer");

                    b.Property<int?>("GameResultType")
                        .HasColumnType("integer");

                    b.Property<int>("InitiatedPlayerId")
                        .HasColumnType("integer");

                    b.Property<int>("InvitedPlayerId")
                        .HasColumnType("integer");

                    b.Property<int>("RedPlayerId")
                        .HasColumnType("integer");

                    b.Property<int>("SecondsPerMove")
                        .HasColumnType("integer");

                    b.Property<int?>("WinnerPlayerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BlackPlayerId");

                    b.HasIndex("InitiatedPlayerId");

                    b.HasIndex("InvitedPlayerId");

                    b.HasIndex("RedPlayerId");

                    b.HasIndex("WinnerPlayerId");

                    b.HasIndex("AcceptedDrawPlayerId", "WinnerPlayerId", "Accepted");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Xi.Database.Dtos.MoveDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("FromFileIndex")
                        .HasColumnType("integer");

                    b.Property<int>("FromRankIndex")
                        .HasColumnType("integer");

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<int>("ToFileIndex")
                        .HasColumnType("integer");

                    b.Property<int>("ToRankIndex")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Moves");
                });

            modelBuilder.Entity("Xi.Database.Dtos.PlayerDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("EloRating")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("ShowPossibleMoves")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Xi.Database.Dtos.ReminderDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<int>("MoveNumber")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GameId", "MoveNumber");

                    b.ToTable("Reminders");
                });

            modelBuilder.Entity("Xi.Database.Dtos.GameDto", b =>
                {
                    b.HasOne("Xi.Database.Dtos.PlayerDto", "AcceptedDrawPlayer")
                        .WithMany()
                        .HasForeignKey("AcceptedDrawPlayerId");

                    b.HasOne("Xi.Database.Dtos.PlayerDto", "BlackPlayer")
                        .WithMany()
                        .HasForeignKey("BlackPlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Xi.Database.Dtos.PlayerDto", "InitiatedPlayer")
                        .WithMany()
                        .HasForeignKey("InitiatedPlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Xi.Database.Dtos.PlayerDto", "InvitedPlayer")
                        .WithMany()
                        .HasForeignKey("InvitedPlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Xi.Database.Dtos.PlayerDto", "RedPlayer")
                        .WithMany()
                        .HasForeignKey("RedPlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Xi.Database.Dtos.PlayerDto", "WinnerPlayer")
                        .WithMany()
                        .HasForeignKey("WinnerPlayerId");

                    b.Navigation("AcceptedDrawPlayer");

                    b.Navigation("BlackPlayer");

                    b.Navigation("InitiatedPlayer");

                    b.Navigation("InvitedPlayer");

                    b.Navigation("RedPlayer");

                    b.Navigation("WinnerPlayer");
                });

            modelBuilder.Entity("Xi.Database.Dtos.MoveDto", b =>
                {
                    b.HasOne("Xi.Database.Dtos.GameDto", "Game")
                        .WithMany("Moves")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("Xi.Database.Dtos.ReminderDto", b =>
                {
                    b.HasOne("Xi.Database.Dtos.GameDto", "Game")
                        .WithMany("Reminders")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("Xi.Database.Dtos.GameDto", b =>
                {
                    b.Navigation("Moves");

                    b.Navigation("Reminders");
                });
#pragma warning restore 612, 618
        }
    }
}
