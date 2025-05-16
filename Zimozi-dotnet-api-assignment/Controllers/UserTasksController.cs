using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Zimozi_dotnet_api_assignment.data;
using Zimozi_dotnet_api_assignment.Identity;
using Zimozi_dotnet_api_assignment.Models.DTO;
using Zimozi_dotnet_api_assignment.Models.Entities;


namespace Zimozi_dotnet_api_assignment.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class UserTasksController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public UserTasksController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        // GET: /api/tasks/
        [Authorize]
        [HttpGet]
        public ActionResult GetAllTasks()
        {
            // Fetch all tasks from database
            List<UserTask> allTasks = _dbContext.UserTasks.Include(task => task.AssignedUser).ToList();

            // Map domain objects to DTO objects
            List<TaskDto> tasks = new List<TaskDto>();
            foreach (UserTask task in allTasks)
            {
                TaskDto taskDto = new TaskDto()
                {
                    Id = task.Id,
                    Name = task.Name,
                    Description = task.Description,
                    AssignedUsername = task.AssignedUser.Username,
                };
                tasks.Add(taskDto);
            }

            return Ok(tasks);
        }

        // GET: /api/tasks/{id}
        [Authorize]
        [HttpGet]
        [Route("{id:Guid}")]
        public ActionResult GetTaskById([FromRoute] Guid id)
        {
            // Fetch task from database
            UserTask? task = _dbContext.UserTasks.Include(task => task.AssignedUser)
                    .Include(task => task.TaskComments)
                    .FirstOrDefault(task => task.Id == id);

            // Return error 404 if not found
            if (task == null)
            {
                return NotFound();
            }

            // Map to DTO object
            TaskDto taskDto = new TaskDto()
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                AssignedUsername = task.AssignedUser.Username,
                Comments = new List<TaskCommentDto>(),
            };

            // Include comments for individual task
            foreach (TaskComment comment in task.TaskComments)
            {
                TaskCommentDto commentDto = new TaskCommentDto()
                {
                    Id = comment.Id,
                    Message = comment.Message,
                    TaskId = comment.TaskId,
                    UserId = comment.UserId,
                };
            }

            return Ok(taskDto);
        }

        // POST: /api/tasks/
        [Authorize(Policy = IdentityData.AdminRolePolicyName)]
        [HttpPost]
        public ActionResult CreateTask([FromBody] AddTaskDto taskDto)
        {
            // Fetch user to be assigned the new task
            User? assignedUser = _dbContext.Users.FirstOrDefault(u => u.Username == taskDto.Username);

            if (assignedUser == null)
            {
                return BadRequest($"User {taskDto.Username} to be assigned not found");
            }

            // Create task object
            UserTask task = new UserTask()
            {
                Name = taskDto.Name,
                Description = taskDto.Description,
                AssignedUser = assignedUser,
                AssignedUserId = assignedUser.Id,
            };

            // Save object to database
            _dbContext.UserTasks.Add(task);
            _dbContext.SaveChanges();

            // Create return object
            TaskDto createdTaskDto = new TaskDto()
            {
                Id = task.Id,
                Name = taskDto.Name,
                Description = taskDto.Description,
                AssignedUsername = task.AssignedUser.Username,
            };

            // Return Dto and id of created object
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, createdTaskDto);
        }

    }
}
