using Education.Core.Models;
using Education.Data.Configurations;
using Education.Data.Seeds;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnswerConfiguration());
            modelBuilder.ApplyConfiguration(new ClassroomConfiguration());
            modelBuilder.ApplyConfiguration(new LessonConfiguration());
            modelBuilder.ApplyConfiguration(new ExamConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new SchoolConfiguration());
            modelBuilder.ApplyConfiguration(new TopicConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<MapClassroomLesson>().HasKey(c => new { c.ClassroomId, c.LessonId });
            modelBuilder.Entity<MapExamQuestion>().HasKey(c => new { c.ExamId, c.QuestionId });
            modelBuilder.Entity<MapRolePermission>().HasKey(c => new { c.RoleId, c.PermissionId });
            modelBuilder.Entity<MapUserClassroom>().HasKey(c => new { c.UserId, c.ClassroomId });
            modelBuilder.Entity<MapUserRole>().HasKey(c => new { c.UserId, c.RoleId });
            modelBuilder.Entity<MapUserSchool>().HasKey(c => new { c.UserId, c.SchoolId });

            //modelBuilder.ApplyConfiguration(new UserSeed());
            //modelBuilder.ApplyConfiguration(new RoleSeed());
            //modelBuilder.ApplyConfiguration(new PermissionSeed());
            //modelBuilder.ApplyConfiguration(new SchoolSeed());
            //modelBuilder.ApplyConfiguration(new ClassroomSeed());
            //modelBuilder.ApplyConfiguration(new LessonSeed());
            //modelBuilder.ApplyConfiguration(new TopicSeed());
            //modelBuilder.ApplyConfiguration(new ExamSeed());
        }
    }
}
