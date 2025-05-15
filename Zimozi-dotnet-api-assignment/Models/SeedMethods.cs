using Microsoft.EntityFrameworkCore;
using Zimozi_dotnet_api_assignment.Models.Entities;

namespace Zimozi_dotnet_api_assignment.Models
{
    public static class SeedMethods
    {
        // Hard coded seed data for the sake of clarity
        // To be moved to more secure storage for prod
        private static readonly (string username, string password)[] AdminUserSeedList = new[]
        {
            ("Admin", "Adminpass"), ("Admin2", "Admin2pass"), ("Admin3", "Admin3pass")
        };

        private static readonly (string username, string password)[] UserSeedList = new[]
        {
            ("User", "Userpass"), ("User2", "User2pass"), ("User3", "User3pass")
        };

        private static readonly (string assignedUserName, string name, string description)[] TaskSeedList = new[]
            {
                ("Admin", "Task 1", "Demo task assigned to Admin"),
                ("Admin", "Task 2", "Demo task 2 assigned to Admin"),
                ("Admin", "Task 3", "Demo task 3 assigned to Admin"),
                ("User", "Task 4", "Demo task 4 assigned to User"),
                ("User", "Task 5", "Demo task 5 assigned to User"),
                ("User", "Task 6", "Demo task 6 assigned to User"),
            };

        public static void SeedMainSync(DbContext context, bool _)
        {
            // Create seed users if not present in db
            foreach (var admin in AdminUserSeedList)
                context.CreateSeedUserIfNotExists(admin, UserRole.Admin);
            foreach (var user in UserSeedList)
                context.CreateSeedUserIfNotExists(user, UserRole.User);

            // Save users to db before seeding tasks
            context.SaveChanges();
            
            // Create seed task if no tasks are present 
            var tasksExist = context.Set<UserTask>().Any(task => true);
            if (!tasksExist)
            {
                foreach (var task in TaskSeedList)
                {
                    context.CreateSeedTask(task);
                }
            }
            context.SaveChanges();
        }

        /// <summary>
        /// Helper extension method to create seed user if not present in database
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userSeedData"></param>
        /// <param name="role"></param>
        private static void CreateSeedUserIfNotExists(this DbContext context, (string username, string password) userSeedData, UserRole role)
        {
            var userExists = context.Set<User>().Any(u => u.Username == userSeedData.username);
            if (!userExists)
            {
                User user = new User
                {
                    Username = userSeedData.username,
                    Role = role,
                };
                context.Set<User>().Add(user);
            }
        }

        /// <summary>
        /// Helper extension method to create seed task
        /// </summary>
        /// <param name="context"></param>
        /// <param name="seedTaskData"></param>
        private static void CreateSeedTask(this DbContext context, (string assignedUserName, string name, string description) seedTaskData)
        {
            var user = context.Set<User>().Single(u => u.Username == seedTaskData.assignedUserName);
            if (user!=null)
            {
                UserTask task = new UserTask
                {
                    Name = seedTaskData.name,
                    Description = seedTaskData.description,
                    AssignedUser = user,
                    AssignedUserName = seedTaskData.assignedUserName,
                };
                context.Set<UserTask>().Add(task);
            }
        }
    }
}
