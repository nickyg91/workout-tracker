﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WorkoutTracker.Data.Contexts;

namespace WorkoutTracker.Api.Migrations
{
    [DbContext(typeof(WorkoutTrackerContext))]
    [Migration("20210801191708_EmailVerificationColumns")]
    partial class EmailVerificationColumns
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("workout")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("WorkoutTracker.Data.Entities.LoginAttempt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("IsSuccessful")
                        .HasColumnType("boolean")
                        .HasColumnName("is_successful");

                    b.Property<DateTime>("LastLogonAttemptUtc")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("last_login_attempt_utc");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("login_attempts");
                });

            modelBuilder.Entity("WorkoutTracker.Data.Entities.WorkoutUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("AccountDeactivated")
                        .HasColumnType("boolean")
                        .HasColumnName("account_deactivated");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("birth_date");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("first_name");

                    b.Property<bool>("IsEmailValidated")
                        .HasColumnType("boolean")
                        .HasColumnName("is_email_validated");

                    b.Property<string>("LastName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<decimal>("TargetWeight")
                        .HasColumnType("numeric(4,1)")
                        .HasColumnName("target_weight");

                    b.Property<string>("Username")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("user_name");

                    b.Property<Guid>("ValidationToken")
                        .HasColumnType("uuid")
                        .HasColumnName("validation_token");

                    b.HasKey("Id");

                    b.ToTable("workout_user");
                });

            modelBuilder.Entity("WorkoutTracker.Data.Entities.LoginAttempt", b =>
                {
                    b.HasOne("WorkoutTracker.Data.Entities.WorkoutUser", "User")
                        .WithMany("LoginAttempts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WorkoutTracker.Data.Entities.WorkoutUser", b =>
                {
                    b.Navigation("LoginAttempts");
                });
#pragma warning restore 612, 618
        }
    }
}