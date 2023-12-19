using System;
using System.Collections.Generic;
using System.Security.Authentication;
using KrutangerHighSchoolDB.Models;
using KrutangerHighSchoolDB.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KrutangerHighSchoolDB.Data;

public partial class KrutångerHighSchoolContext : DbContext
{
    public KrutångerHighSchoolContext()
    {
    }

    public KrutångerHighSchoolContext(DbContextOptions<KrutångerHighSchoolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ClassList> ClassLists { get; set; }

    public virtual DbSet<ClassMentor> ClassMentors { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseRegistration> CourseRegistrations { get; set; }

    public virtual DbSet<CourseTeacher> CourseTeachers { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<GradeType> GradeTypes { get; set; }

    public virtual DbSet<JobTitle> JobTitles { get; set; }

    public virtual DbSet<Personnel> Personnel { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<LatestMonthGradesView> LatestMonthGradesViews { get; set; }

    public virtual DbSet<CoursesWithAverageGradeView> CoursesWithAverageGradeViews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Check if the DbContext options are not already configured.
        if (!optionsBuilder.IsConfigured)
        {
            try
            {
                // Use the ConfigurationsBuilder to build a configuration from appsettings.json.
                IConfiguration config = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                // Get the connection string from appsettings.json.
                string connectionString = config.GetConnectionString("KrutångerDbContext")!;

                // Configure DbContext to use SQL Server with the obtained connection string.
                optionsBuilder.UseSqlServer(connectionString);
            }
            catch (Exception ex)
            {
                // If an exception occurs during configuration, print the error message to the console.
                Console.WriteLine(ex.Message);
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClassList>(entity =>
        {
            entity.HasKey(e => e.ClassId);

            entity.ToTable("ClassList");

            entity.Property(e => e.Branch).HasMaxLength(50);
            entity.Property(e => e.ClassName)
                .HasMaxLength(2)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ClassMentor>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ClassMentor");

            entity.Property(e => e.FkClassId).HasColumnName("FK_ClassId");
            entity.Property(e => e.FkPersonnelId).HasColumnName("FK_PersonnelId");

            entity.HasOne(d => d.FkClass).WithMany()
                .HasForeignKey(d => d.FkClassId)
                .HasConstraintName("FK_ClassMentor_ClassList");

            entity.HasOne(d => d.FkPersonnel).WithMany()
                .HasForeignKey(d => d.FkPersonnelId)
                .HasConstraintName("FK_ClassMentor_Personnel");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.Property(e => e.Course1)
                .HasMaxLength(50)
                .HasColumnName("Course");
        });

        modelBuilder.Entity<CourseRegistration>(entity =>
        {
            entity.HasKey(e => e.CourseRegId);

            entity.ToTable("CourseRegistration");

            entity.Property(e => e.FkCourseId).HasColumnName("FK_CourseId");
            entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentId");

            entity.HasOne(d => d.FkCourse).WithMany(p => p.CourseRegistrations)
                .HasForeignKey(d => d.FkCourseId)
                .HasConstraintName("FK_CourseRegistration_Courses");

            entity.HasOne(d => d.FkStudent).WithMany(p => p.CourseRegistrations)
                .HasForeignKey(d => d.FkStudentId)
                .HasConstraintName("FK_CourseRegistration_Students");
        });

        modelBuilder.Entity<CourseTeacher>(entity =>
        {
            entity.ToTable("CourseTeacher");

            entity.Property(e => e.FkCourseId).HasColumnName("FK_CourseId");
            entity.Property(e => e.FkPersonnelId).HasColumnName("FK_PersonnelId");

            entity.HasOne(d => d.FkCourse).WithMany(p => p.CourseTeachers)
                .HasForeignKey(d => d.FkCourseId)
                .HasConstraintName("FK_CourseTeacher_Courses");

            entity.HasOne(d => d.FkPersonnel).WithMany(p => p.CourseTeachers)
                .HasForeignKey(d => d.FkPersonnelId)
                .HasConstraintName("FK_CourseTeacher_Personnel");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("PK__Genders__4E24E9F7FF3B3015");

            entity.Property(e => e.GenderId).ValueGeneratedOnAdd();
            entity.Property(e => e.Gender1)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Gender");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.GradedId);

            entity.Property(e => e.FkCourseRegId).HasColumnName("FK_CourseRegId");
            entity.Property(e => e.FkCourseTeacherId).HasColumnName("FK_CourseTeacherId");
            entity.Property(e => e.FkGradeId).HasColumnName("FK_GradeId");
            entity.Property(e => e.GradedDate).HasColumnType("date");

            entity.HasOne(d => d.FkCourseReg).WithMany(p => p.Grades)
                .HasForeignKey(d => d.FkCourseRegId)
                .HasConstraintName("FK_Grades_CourseRegistration");

            entity.HasOne(d => d.FkCourseTeacher).WithMany(p => p.Grades)
                .HasForeignKey(d => d.FkCourseTeacherId)
                .HasConstraintName("FK_Grades_CourseTeacher");

            entity.HasOne(d => d.FkGrade).WithMany(p => p.Grades)
                .HasForeignKey(d => d.FkGradeId)
                .HasConstraintName("FK_Grades_GradeType");
        });

        modelBuilder.Entity<GradeType>(entity =>
        {
            entity.HasKey(e => e.GradeId);

            entity.ToTable("GradeType");

            entity.Property(e => e.GradeId).ValueGeneratedOnAdd();
            entity.Property(e => e.Grade)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<JobTitle>(entity =>
        {
            entity.Property(e => e.JobTitleId).ValueGeneratedOnAdd();
            entity.Property(e => e.JobTitle1)
                .HasMaxLength(50)
                .HasColumnName("JobTitle");
        });

        modelBuilder.Entity<Personnel>(entity =>
        {
            entity.HasIndex(e => e.Ssn, "UQ_PersonnelSSN").IsUnique();

            entity.HasIndex(e => e.Ssn, "UQ_Personnel_SSN").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(75);
            entity.Property(e => e.FkGenderId).HasColumnName("FK_GenderId");
            entity.Property(e => e.FkJobTitleId).HasColumnName("FK_JobTitleId");
            entity.Property(e => e.PhoneNr)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.Ssn)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("SSN");
            entity.Property(e => e.Surname).HasMaxLength(75);

            entity.HasOne(d => d.FkGender).WithMany(p => p.Personnel)
                .HasForeignKey(d => d.FkGenderId)
                .HasConstraintName("FK_Personnel_Genders");

            entity.HasOne(d => d.FkJobTitle).WithMany(p => p.Personnel)
                .HasForeignKey(d => d.FkJobTitleId)
                .HasConstraintName("FK_Personnel_JobTitles");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasIndex(e => e.Ssn, "UQ_StudentSSN").IsUnique();

            entity.HasIndex(e => e.Ssn, "UQ_Students_SSN").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(75);
            entity.Property(e => e.FkClassId).HasColumnName("FK_ClassId");
            entity.Property(e => e.FkGenderId).HasColumnName("FK_GenderId");
            entity.Property(e => e.PhoneNr)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.SchoolStart).HasColumnType("date");
            entity.Property(e => e.Ssn)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("SSN");
            entity.Property(e => e.Surname).HasMaxLength(75);

            entity.HasOne(d => d.FkClass).WithMany(p => p.Students)
                .HasForeignKey(d => d.FkClassId)
                .HasConstraintName("FK_Students_ClassList");

            entity.HasOne(d => d.FkGender).WithMany(p => p.Students)
                .HasForeignKey(d => d.FkGenderId)
                .HasConstraintName("FK_Students_Genders");
        });

        modelBuilder.Entity<LatestMonthGradesView>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("LatestMonthGradesView");
        });

        modelBuilder.Entity<CoursesWithAverageGradeView>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("CoursesWithAverageGradeView");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
