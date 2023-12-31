﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Context_Layer.Models
{
    public partial class edulmsContext : DbContext
    {
        public edulmsContext()
        {
        }

        public edulmsContext(DbContextOptions<edulmsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Assessment> Assessments { get; set; }
        public virtual DbSet<AssessmentAnswer> AssessmentAnswers { get; set; }
        public virtual DbSet<AssessmentDatum> AssessmentData { get; set; }
        public virtual DbSet<AssessmentDepartment> AssessmentDepartments { get; set; }
        public virtual DbSet<AssessmentEnrol> AssessmentEnrols { get; set; }
        public virtual DbSet<AssessmentMatch> AssessmentMatches { get; set; }
        public virtual DbSet<AssessmentMetum> AssessmentMeta { get; set; }
        public virtual DbSet<AssessmentOption> AssessmentOptions { get; set; }
        public virtual DbSet<AssessmentQuestion> AssessmentQuestions { get; set; }
        public virtual DbSet<AssessmentQuestionsRelation> AssessmentQuestionsRelations { get; set; }
        public virtual DbSet<AssessmentSection> AssessmentSections { get; set; }
        public virtual DbSet<AssessmentText> AssessmentTexts { get; set; }
        public virtual DbSet<AssessmentTrueFalse> AssessmentTrueFalses { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=192.168.1.6;Initial Catalog=edulms;Persist Security Info=True;User ID=sa;Password=Nabwy12345");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assessment>(entity =>
            {
                entity.Property(e => e.Duration).HasDefaultValueSql("('7')");

                entity.Property(e => e.Published).HasDefaultValueSql("('1')");
            });

            modelBuilder.Entity<AssessmentAnswer>(entity =>
            {
                entity.Property(e => e.Score).HasDefaultValueSql("('0')");

                entity.HasOne(d => d.Assessment)
                    .WithMany(p => p.AssessmentAnswers)
                    .HasForeignKey(d => d.AssessmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_assessment_answers_assessments");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.AssessmentAnswers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_assessment_answers_assessment_questions");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AssessmentAnswers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_assessment_answers_users");
            });

            modelBuilder.Entity<AssessmentDatum>(entity =>
            {
                entity.HasOne(d => d.Assessment)
                    .WithMany(p => p.AssessmentData)
                    .HasForeignKey(d => d.AssessmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_assessment_data_assessments");
            });

            modelBuilder.Entity<AssessmentDepartment>(entity =>
            {
                entity.HasOne(d => d.Assessment)
                    .WithMany(p => p.AssessmentDepartments)
                    .HasForeignKey(d => d.AssessmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_assessment_department_assessments");
            });

            modelBuilder.Entity<AssessmentEnrol>(entity =>
            {
                entity.Property(e => e.Score).HasDefaultValueSql("('0')");

                entity.HasOne(d => d.Assessment)
                    .WithMany(p => p.AssessmentEnrols)
                    .HasForeignKey(d => d.AssessmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_assessment_enrols_assessments");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AssessmentEnrols)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_assessment_enrols_users");
            });

            modelBuilder.Entity<AssessmentMatch>(entity =>
            {
                entity.Property(e => e.AnswerIdKey).IsFixedLength();

                entity.Property(e => e.Order).HasDefaultValueSql("('0')");

                entity.Property(e => e.QuestionIdKey).IsFixedLength();

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.AssessmentMatches)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_assessment_match_assessment_questions");
            });

            modelBuilder.Entity<AssessmentMetum>(entity =>
            {
                entity.HasOne(d => d.Assessment)
                    .WithMany(p => p.AssessmentMeta)
                    .HasForeignKey(d => d.AssessmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_assessment_meta_assessments");
            });

            modelBuilder.Entity<AssessmentOption>(entity =>
            {
                entity.Property(e => e.Correct).HasDefaultValueSql("('0')");

                entity.Property(e => e.Order).HasDefaultValueSql("('0')");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.AssessmentOptions)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_assessment_options_assessment_questions");
            });

            modelBuilder.Entity<AssessmentQuestion>(entity =>
            {
                entity.Property(e => e.Level).HasDefaultValueSql("('0')");

                entity.Property(e => e.Order).HasDefaultValueSql("('0')");
            });

            modelBuilder.Entity<AssessmentQuestionsRelation>(entity =>
            {
                entity.HasOne(d => d.Assessment)
                    .WithMany(p => p.AssessmentQuestionsRelations)
                    .HasForeignKey(d => d.AssessmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_assessment_questions_relation_assessments");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.AssessmentQuestionsRelations)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_assessment_questions_relation_assessment_questions");
            });

            modelBuilder.Entity<AssessmentSection>(entity =>
            {
                entity.Property(e => e.Order).HasDefaultValueSql("('0')");

                entity.HasOne(d => d.Assessment)
                    .WithMany(p => p.AssessmentSections)
                    .HasForeignKey(d => d.AssessmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_assessment_sections_assessments");
            });

            modelBuilder.Entity<AssessmentText>(entity =>
            {
                entity.Property(e => e.Order).HasDefaultValueSql("('0')");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.AssessmentTexts)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_assessment_text_assessment_questions");
            });

            modelBuilder.Entity<AssessmentTrueFalse>(entity =>
            {
                entity.Property(e => e.IsTrue).HasDefaultValueSql("('0')");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.AssessmentTrueFalses)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_assessment_true_false_assessment_questions");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.ApiKey).IsFixedLength();

                entity.Property(e => e.ConfirmCode).IsFixedLength();

                entity.Property(e => e.IsBanned).HasDefaultValueSql("('0')");

                entity.Property(e => e.IsLdap).HasDefaultValueSql("('0')");

                entity.Property(e => e.IsVerified).HasDefaultValueSql("('0')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}