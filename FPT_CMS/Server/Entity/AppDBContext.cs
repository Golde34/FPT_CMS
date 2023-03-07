using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Server.Entity
{
    public class AppDBContext : DbContext
    {
        public AppDBContext()
        {
        }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Curriculum> Curricula { get; set; }
        public DbSet<CurriculumDetail> CurriculumDetails { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CurriculumDetail>(entity =>
            {
                entity.HasKey(x => new { x.CurriculumId, x.SubjectCode });
                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Details)
                    .HasForeignKey(d => d.SubjectCode)
                    .HasConstraintName("FK_CurriculumDetail_Subject");
            });

            builder.Entity<Grade>().HasKey(x => x.GradeId);

            builder.Entity<Course>(entity =>
            {
                entity.HasOne(d => d.Subject).WithMany(p => p.Courses).HasForeignKey(d => d.SubjectCode);
                entity.HasOne(d => d.Teacher).WithMany(p => p.Courses).HasForeignKey(d => d.TeacherId);
                entity.HasOne(d => d.Semester).WithMany(p => p.Courses).HasForeignKey(d => d.SemesterId);
            });

            builder.Entity<Topic>(entity =>
            {
                entity.HasOne(d => d.Course).WithMany(p => p.Topics).HasForeignKey(d => d.CourseId);
            });

            builder.Entity<Submission>(entity =>
            {
                entity.HasOne(d => d.Topic).WithMany(p => p.Submissions).HasForeignKey(d => d.TopicId);
            });

            builder.Entity<Notification>(entity =>
            {
                entity.HasOne(d => d.Course).WithMany(p => p.Notifications).HasForeignKey(d => d.CourseId);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("DBString");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}