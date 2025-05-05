using PapersApi.Models;
using PapersApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace PapersApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UsersService _usersService;

    public UsersController(UsersService usersService) =>
        _usersService = usersService;

    [HttpGet]
    public async Task<List<User>> Get() =>
        await _usersService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<User>> Get(string id)
    {
        var user = await _usersService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpGet("us")]
    public async Task<ActionResult<User>> GetUserByName(string username)
    {
        var user = await _usersService.GetUserByName(username);

        if (user is null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpGet("open")]
    public async Task<ActionResult<User>> GetUserByOpenAlexId(string id)
    {
        var user = await _usersService.GetUserByOpenAlexId(id);

        if (user is null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpGet("proj")]
    public async Task<ActionResult<List<User?>>> GetUserByProject(string project)
    {
        var user = await _usersService.GetUserByProject(project);

        if (user is null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpPost]
    public async Task<IActionResult> Post(User newUser)
    {
        await _usersService.CreateAsync(newUser);

        return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, User updatedUser)
    {
        var user = await _usersService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        updatedUser.Id = user.Id;

        await _usersService.UpdateAsync(id, updatedUser);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _usersService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        await _usersService.RemoveAsync(id);

        return NoContent();
    }
}