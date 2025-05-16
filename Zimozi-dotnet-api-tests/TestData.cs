using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zimozi_dotnet_api_assignment.Models.Entities;

namespace Zimozi_dotnet_api_tests
{
    /// <summary>
    /// Helper class to organize static test data
    /// </summary>
    static internal class TestData
    {
        private static readonly (string username, string password)[] AdminUserSeedList = new[]
        {
            ("Admin", "Adminpass"), ("Admin2", "Admin2pass"), ("Admin3", "Admin3pass")
        };

        private static readonly (string username, string password)[] UserSeedList = new[]
        {
            ("User", "Userpass"), ("User2", "User2pass"), ("User3", "User3pass")
        };

        private static readonly (string name, string description)[] TaskSeedList = new[]
            {
                ("Task 1", "Demo task"),
                ("Task 2", "Demo task 2"),
                ("Task 3", "Demo task 3"),
                ("Task 4", "Demo task 4"),
                ("Task 5", "Demo task 5"),
                ("Task 6", "Demo task 6"),
            };
        public static List<User> Users = [];
        public static List<UserTask> Tasks = [];
        static TestData()
        {
            foreach (var userSeed in AdminUserSeedList)
                Users.Add(CreateUser(userSeed, UserRole.Admin));
            foreach (var userSeed in UserSeedList)
                Users.Add(CreateUser(userSeed, UserRole.User));

            Random r = new Random();
            foreach (var taskSeed in TaskSeedList)
            {
                User randomUser = Users[r.Next(Users.Count)];
                Tasks.Add(CreateTask(randomUser, taskSeed));
            }


        }

        private static User CreateUser((string username, string password) userSeedData, UserRole role)
        {
            User user = new User
            {
                Username = userSeedData.username,
                Password = userSeedData.password,
                Role = role,
            };
            return user;
        }

        private static UserTask CreateTask(User user, (string name, string description) seedTaskData)
        {
            UserTask task = new UserTask
            {
                Name = seedTaskData.name,
                Description = seedTaskData.description,
                AssignedUser = user,
                AssignedUserId = user.Id,
            };
            return task;
        }
    }
}

