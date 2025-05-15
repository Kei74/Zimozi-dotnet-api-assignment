using Microsoft.EntityFrameworkCore;
using Zimozi_dotnet_api_assignment.Models.Entities;

namespace Zimozi_dotnet_api_assignment.data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Store UserRole property as string in database
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion(
                r => r.ToString(),
                r => (UserRole)Enum.Parse(typeof(UserRole), r));

            // Define One-To-Many relationship between;
            // User and UserTasks
            modelBuilder.Entity<User>()
                .HasMany<UserTask>(u => u.AssignedTasks)
                .WithOne(t => t.AssignedUser)
                .OnDelete(DeleteBehavior.Cascade);

            // Users and TaskComments, not navigable from Users, preserve comments and set null on delete
            modelBuilder.Entity<User>()
                .HasMany<TaskComment>()
                .WithOne(tc => tc.User)
                .HasForeignKey(tc => tc.UserId);


            // UserTasks and TaskComments
            modelBuilder.Entity<UserTask>()
                .HasMany<TaskComment>(t => t.TaskComments)
                .WithOne(tc => tc.UserTask)
                .HasForeignKey(t => t.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
