﻿// <auto-generated />
using System;
using EFUApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFUApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240605215238_AddedInstructorToCourseFix")]
    partial class AddedInstructorToCourseFix
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EFUApi.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CourseNum")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Instructor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CourseId");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            CourseId = 1,
                            Code = "ENG101",
                            CourseNum = 1,
                            Description = "Poetry from 1900",
                            Name = "English"
                        },
                        new
                        {
                            CourseId = 2,
                            Code = "MATH101",
                            CourseNum = 2,
                            Description = "Calculus",
                            Name = "Math"
                        },
                        new
                        {
                            CourseId = 3,
                            Code = "PHIL101",
                            CourseNum = 3,
                            Description = "Logic",
                            Name = "Philosophy"
                        },
                        new
                        {
                            CourseId = 4,
                            Code = "PSYCH101",
                            CourseNum = 4,
                            Description = "Psychology of Women",
                            Name = "Psychology"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
