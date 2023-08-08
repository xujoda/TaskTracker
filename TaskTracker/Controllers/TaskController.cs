using Microsoft.AspNetCore.Mvc;
using TaskTracker.Services;
using Task = TaskTracker.Models.Task;

namespace TaskTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public IActionResult CreateTask(Task task)
        {
            if (task == null)
            {
                return BadRequest("Task is null"); // todo: create error's collection to returns
            }

            var createTask = _taskService.CreateTask(task);

            return CreatedAtRoute(nameof(GetTaskById), new { id = createTask.TaskId }, createTask);
        }

        [HttpGet] 
        public IActionResult GetTaskById(int id)
        {
            try
            {
                var task = _taskService.GetTaskById(id);

                return Ok(task);
            }
            catch (ItemByIdNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTaskById(int id)
        {
            try
            {
                _taskService.DeleteTaskById(id);
                return NoContent();
            }
            catch (ItemByIdNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateTask(Task task)
        {
            try
            {
                _taskService.UpdateTask(task);
                return Ok();
            }
            catch (ItemByIdNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}