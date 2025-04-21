using ManyWhoCan.Models;
using Microsoft.EntityFrameworkCore;

namespace ManyWhoCan
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Отношение один ко многим между Teacher и Course
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            // Отношение многие ко многим между Student и Course
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Students)
                .WithMany(s => s.Courses)
                /*.UsingEntity<Dictionary<string, object>>(
                    "StudentCourse",
                    j => j
                        .HasOne<Student>()
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK_StudentCourse_Student")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<Course>()
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .HasConstraintName("FK_StudentCourse_Course")
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("StudentId", "CourseId");
                        j.ToTable("StudentCourses");
                    });*/


        }

    }
}
