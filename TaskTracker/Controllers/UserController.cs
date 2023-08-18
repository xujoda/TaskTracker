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
        public async Task<IActionResult> AddUser(User user)
        {
            if (user == null)
            {
                return BadRequest("User is null"); // todo: create error's collection to returns
            }

            var newUser = await _userService.AddUser(user);

            return CreatedAtRoute(nameof(GetUserById), new { id = newUser.UserId}, newUser);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var response = await _userService.GetUserById(id);
                return Ok(response);
            }
            catch (ItemByIdNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                return Ok(await _userService.GetAllUsers());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            try
            {
                return Ok(await _userService.GetUserByEmail(email));
            }
            catch (ItemByStringNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserById(int userId)
        {
            try
            {
                await _userService.DeleteUserById(userId);

                return NoContent();
            }
            catch (ItemByIdNotFoundException ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(User user)
        {
            try
            {
                await _userService.UpdateUser(user);

                return Ok();
            }
            catch (ItemByIdNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
