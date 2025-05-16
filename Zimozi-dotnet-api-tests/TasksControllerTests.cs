using Microsoft.AspNetCore.Mvc;
using Moq;
using Zimozi_dotnet_api_assignment.Controllers;
using Zimozi_dotnet_api_assignment.data;
using Zimozi_dotnet_api_assignment.Models.DTO;
using Zimozi_dotnet_api_assignment.Models.Entities;
using Zimozi_dotnet_api_assignment.Repositories;

namespace Zimozi_dotnet_api_tests
{
    /// <summary>
    /// Unit Tests for TasksController
    /// </summary>
    /// 
    // To run tests, Build the solution from Visual Studio and open Test Explorer
    public class TasksControllerTests
    {
        private readonly UserTasksController _userTasksController;
        private readonly Mock<IUserRepository> _usersMock = new Mock<IUserRepository>();
        private readonly Mock<IUserTaskRepository> _tasksMock = new Mock<IUserTaskRepository>();

        public TasksControllerTests()
        {
            _userTasksController = new UserTasksController(_usersMock.Object, _tasksMock.Object);
        }



        [SetUp]
        public void Setup()
        {
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _userTasksController.Dispose();
        }

        [Test]
        public void GetAllTasks_ShouldReturnOk()
        {

            _tasksMock.Setup(x => x.GetAllWithUsers())
                    .Returns(TestData.Tasks);

            var result = _userTasksController.GetAllTasks();

            Assert.That(result.GetType().Name, Is.EqualTo("OkObjectResult"));
        }

        [Test]
        public void GetTaskById_ShouldReturnTask_WhenTaskExists()
        {
            UserTask task = TestData.Tasks[0];
            TaskDto taskDto = new TaskDto
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                AssignedUsername = task.AssignedUser.Username, 
            };


            _tasksMock.Setup(x => x.GetByIdWithUserAndComment(It.Is<Guid>(x => x == task.Id)))
                    .Returns(task);

            var result = _userTasksController.GetTaskById(task.Id);
            Assert.That(result.GetType().Name, Is.EqualTo("OkObjectResult"));
            var resultObject = (OkObjectResult)result;
            Assert.That(resultObject.Value.ToString(), Is.EqualTo(taskDto.ToString()));
        }

        // Negative test case for get by Id
        [Test]
        public void GetTaskById_ShouldReturnNothing_WhenTaskDoesNotExists()
        {
            _tasksMock.Setup(x => x.GetByIdWithUserAndComment(It.IsAny<Guid>()))
                    .Returns(() => null);

            var result = _userTasksController.GetTaskById(new Guid());
            Assert.That(result.GetType().Name, Is.EqualTo("NotFoundResult"));
        }

        [Test]
        public void CreateTask_ShouldCreateTask_WhenInputValid()
        {

            UserTask task = TestData.Tasks[0];
            AddTaskDto addTaskDto = new AddTaskDto
            {
                Description = task.Description,
                Name = task.Name,
                Username = task.AssignedUser.Username
            };

            TaskDto taskDto = new TaskDto
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                AssignedUsername = task.AssignedUser.Username,
            };

            _usersMock.Setup(x => x.GetByUsername(It.Is<string>(x => x == task.AssignedUser.Username)))
                    .Returns(() => task.AssignedUser);

            var result = _userTasksController.CreateTask(addTaskDto);
            Assert.That(result.GetType().Name, Is.EqualTo("CreatedAtActionResult"));
            var resultObject = (CreatedAtActionResult)result;
            Assert.That(resultObject.Value.ToString(), Is.EqualTo(taskDto.ToString()));
        }

        // Negative test case for create task
        [Test]
        public void CreateTask_ShouldFail_WhenUserNotValid()
        {
            UserTask task = TestData.Tasks[0];
            AddTaskDto addTaskDto = new AddTaskDto
            {
                Description = task.Description,
                Name = task.Name,
                Username = task.AssignedUser.Username
            };
            _usersMock.Setup(x => x.GetByUsername(It.IsAny<string>()))
                    .Returns(() => null);

            var result = _userTasksController.CreateTask(addTaskDto);
            Assert.That(result.GetType().Name, Is.EqualTo("BadRequestObjectResult"));
        }
    }
}