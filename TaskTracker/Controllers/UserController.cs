using Microsoft.AspNetCore.Mvc;
using TaskTracker.Services;
using User = TaskTracker.Models.User;

namespace TaskTracker.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            if (user == null)
            {
                return BadRequest("User is null"); // todo: create error's collection to returns
            }

            var newUser = _userService.AddUser(user);

            return CreatedAtRoute(nameof(GetUserById), new { id = newUser.Result.UserId}, newUser.Result);
        }

        [HttpGet("id/{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                return Ok(_userService.GetUserById(id));
            }
            catch (ItemByIdNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                return Ok(_userService.GetAllUsers());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("email/{email}")]
        public IActionResult GetUserByEmail(string email)
        {
            try
            {
                return Ok(_userService.GetUserByEmail(email));
            }
            catch (ItemByStringNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUserById(int userId)
        {
            try
            {
                _userService.DeleteUserById(userId);

                return NoContent();
            }
            catch (ItemByIdNotFoundException ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{userId}")]
        public IActionResult UpdateUser(User user)
        {
            try
            {
                _userService.UpdateUser(user);

                return Ok();
            }
            catch (ItemByIdNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
