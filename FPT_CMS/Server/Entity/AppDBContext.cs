using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Server.Entity
{
    public class AppDBContext : DbContext
    {
        public AppDBContext() { }
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Curriculum> Curricula { get; set; }
        public DbSet<CurriculumDetail> CurriculumDetails { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }

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
