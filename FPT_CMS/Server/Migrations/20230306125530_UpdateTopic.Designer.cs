﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server.Entity;

#nullable disable

namespace Server.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20230306125530_UpdateTopic")]
    partial class UpdateTopic
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Server.Entity.Account", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Server.Entity.Course", b =>
                {
                    b.Property<string>("CourseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SemesterId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Slot")
                        .HasColumnType("int");

                    b.Property<string>("SubjectCode")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CourseId");

                    b.HasIndex("SemesterId");

                    b.HasIndex("SubjectCode");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Server.Entity.Curriculum", b =>
                {
                    b.Property<int>("CurriculumId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CurriculumId"), 1L, 1);

                    b.Property<string>("CurriculumName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Majors")
                        .HasColumnType("int");

                    b.HasKey("CurriculumId");

                    b.ToTable("Curricula");
                });

            modelBuilder.Entity("Server.Entity.CurriculumDetail", b =>
                {
                    b.Property<int>("CurriculumId")
                        .HasColumnType("int");

                    b.Property<string>("SubjectCode")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CurriculumId", "SubjectCode");

                    b.HasIndex("SubjectCode");

                    b.ToTable("CurriculumDetails");
                });

            modelBuilder.Entity("Server.Entity.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentId"), 1L, 1);

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Server.Entity.Grade", b =>
                {
                    b.Property<int>("GradeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GradeId"), 1L, 1);

                    b.Property<string>("CourseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("GradeId");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("Server.Entity.Semester", b =>
                {
                    b.Property<string>("SemesterId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("SemesterId");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("Server.Entity.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CurriculumId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Majors")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentRollNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("CurriculumId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Server.Entity.Subject", b =>
                {
                    b.Property<string>("SubjectCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<decimal>("Fee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubjectCode");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Server.Entity.Submission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SubmitDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TopicId")
                        .HasColumnType("int");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("TopicId");

                    b.ToTable("Submissions");
                });

            modelBuilder.Entity("Server.Entity.Teacher", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("Server.Entity.Topic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CourseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Due")
                        .HasColumnType("datetime2");

                    b.Property<string>("Requirement")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("Server.Entity.Course", b =>
                {
                    b.HasOne("Server.Entity.Semester", null)
                        .WithMany("Courses")
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Entity.Subject", null)
                        .WithMany("Courses")
                        .HasForeignKey("SubjectCode");
                });

            modelBuilder.Entity("Server.Entity.CurriculumDetail", b =>
                {
                    b.HasOne("Server.Entity.Curriculum", "Curriculum")
                        .WithMany()
                        .HasForeignKey("CurriculumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Entity.Subject", "Subject")
                        .WithMany("Details")
                        .HasForeignKey("SubjectCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_CurriculumDetail_Subject");

                    b.Navigation("Curriculum");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Server.Entity.Grade", b =>
                {
                    b.HasOne("Server.Entity.Course", null)
                        .WithMany("Grades")
                        .HasForeignKey("CourseId");

                    b.HasOne("Server.Entity.Student", null)
                        .WithMany("Grades")
                        .HasForeignKey("StudentId");
                });

            modelBuilder.Entity("Server.Entity.Student", b =>
                {
                    b.HasOne("Server.Entity.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Entity.Curriculum", null)
                        .WithMany("Students")
                        .HasForeignKey("CurriculumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Server.Entity.Subject", b =>
                {
                    b.HasOne("Server.Entity.Department", null)
                        .WithMany("Subjects")
                        .HasForeignKey("DepartmentId");
                });

            modelBuilder.Entity("Server.Entity.Submission", b =>
                {
                    b.HasOne("Server.Entity.Student", null)
                        .WithMany("Submissions")
                        .HasForeignKey("StudentId");

                    b.HasOne("Server.Entity.Topic", null)
                        .WithMany("Submissions")
                        .HasForeignKey("TopicId");
                });

            modelBuilder.Entity("Server.Entity.Teacher", b =>
                {
                    b.HasOne("Server.Entity.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Server.Entity.Topic", b =>
                {
                    b.HasOne("Server.Entity.Course", null)
                        .WithMany("Topics")
                        .HasForeignKey("CourseId");
                });

            modelBuilder.Entity("Server.Entity.Course", b =>
                {
                    b.Navigation("Grades");

                    b.Navigation("Topics");
                });

            modelBuilder.Entity("Server.Entity.Curriculum", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Server.Entity.Department", b =>
                {
                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("Server.Entity.Semester", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("Server.Entity.Student", b =>
                {
                    b.Navigation("Grades");

                    b.Navigation("Submissions");
                });

            modelBuilder.Entity("Server.Entity.Subject", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Details");
                });

            modelBuilder.Entity("Server.Entity.Topic", b =>
                {
                    b.Navigation("Submissions");
                });
#pragma warning restore 612, 618
        }
    }
}
