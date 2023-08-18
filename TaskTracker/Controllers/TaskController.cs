using Microsoft.AspNetCore.Mvc;
using TaskTracker.Services;
using Task = TaskTracker.Models.Task;

namespace TaskTracker.Controllers
{
    [ApiController]
    [Route("api/task")]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(Task task, int userId)
        {
            if (task == null)
            {
                return BadRequest("Task is null"); // todo: create error's collection to returns
            }

            task.UserId = userId;

            var createTask = await _taskService.CreateTask(task);

            return CreatedAtRoute(nameof(GetTaskById), new { id = createTask.TaskId }, createTask);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            try
            {
                var task = await _taskService.GetTaskById(id);

                return Ok(task);
            }
            catch (ItemByIdNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                return Ok(await _taskService.GetAllTasks());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskById(int id)
        {
            try
            {
                await _taskService.DeleteTaskById(id);
                return NoContent();
            }
            catch (ItemByIdNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask(Task task)
        {
            try
            {
                await _taskService.UpdateTask(task);
                return Ok();
            }
            catch (ItemByIdNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}