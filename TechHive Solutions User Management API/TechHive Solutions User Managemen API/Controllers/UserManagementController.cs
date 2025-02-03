using Microsoft.AspNetCore.Mvc;
using TechHive_Solutions_User_Management_API.Models;
using TechHive_Solutions_User_Management_API.Repositories;

namespace TechHive_Solutions_User_Management_API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserManagementController(IUserRepository userRepository, ILogger<UserManagementController> logger) : ControllerBase
{
    [HttpGet]
    [Route(nameof(GetUser))]
    public ActionResult<User> GetUser([FromQuery] int id)
    {
        User? user = userRepository.GetUser(id);
        
        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpPost]
    [Route(nameof(CreateUser))]
    public ActionResult<int> CreateUser([FromQuery] string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return BadRequest("Error: Name cannot be empty.");
        }

        if (name.Length < 5)
        {
            return BadRequest("Error: Name must be at least 5 characters long.");
        }

        if (name.Length > 40)
        {
            return BadRequest("Error: Name cannot exceed 40 characters.");
        }


        return userRepository.AddUser(name);
    }

    [HttpPut]
    [Route(nameof(UpdateUser))]
    public ActionResult UpdateUser([FromQuery] int id, [FromQuery] string name)
    {
        try
        {
            userRepository.UpdateUser(id, name);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }

    [HttpDelete]
    [Route(nameof(DeleteUser))]
    public ActionResult DeleteUser([FromQuery] int id)
    {
        try
        {
            userRepository.DeleteUser(id);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }
}
