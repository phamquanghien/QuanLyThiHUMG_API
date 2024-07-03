﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuanLyThiHUMG.Data;

#nullable disable

namespace QuanLyThiHUMG.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240703175443_Create_Table_StudentExam")]
    partial class Create_Table_StudentExam
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("QuanLyThiHUMG.Models.Entities.Exam", b =>
                {
                    b.Property<int>("ExamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatePerson")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ExamCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ExamName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAutoGenRegistrationCode")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("IsDelete")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

                    b.Property<int>("StartRegistrationCode")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("ExamId");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("QuanLyThiHUMG.Models.Entities.Student", b =>
                {
                    b.Property<string>("StudentCode")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("BirthDay")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.HasKey("StudentCode");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("QuanLyThiHUMG.Models.Entities.StudentExam", b =>
                {
                    b.Property<Guid>("StudentExamID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ClassName")
                        .HasColumnType("TEXT");

                    b.Property<int>("ExamBag")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ExamId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("IdentificationNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LessonNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LessonStart")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("StudentCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SubjectCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TestDay")
                        .HasColumnType("TEXT");

                    b.Property<string>("TestRoom")
                        .HasColumnType("TEXT");

                    b.HasKey("StudentExamID");

                    b.HasIndex("StudentCode");

                    b.HasIndex("SubjectCode");

                    b.ToTable("StudentExams");
                });

            modelBuilder.Entity("QuanLyThiHUMG.Models.Entities.Subject", b =>
                {
                    b.Property<string>("SubjectCode")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("SubjectCode");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("QuanLyThiHUMG.Models.Entities.StudentExam", b =>
                {
                    b.HasOne("QuanLyThiHUMG.Models.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuanLyThiHUMG.Models.Entities.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });
#pragma warning restore 612, 618
        }
    }
}
