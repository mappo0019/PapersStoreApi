using PapersApi.Models;
using PapersApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Features;

namespace PapersApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly LoginService _loginService;

    public LoginController(LoginService loginService) =>
        _loginService = loginService;

    [HttpGet]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, KeyLengthLimit = int.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
    public async Task<List<Login>> Get() =>
        await _loginService.GetAsync();

    [HttpGet("{id:length(24)}")]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, KeyLengthLimit = int.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
    public async Task<ActionResult<Login>> Get(string id)
    {
        var login = await _loginService.GetAsync(id);

        if (login is null)
        {
            return NotFound();
        }

        return login;
    }

    [HttpGet("us")]
    public async Task<ActionResult<Login>> GetLoginByUser(string user)
    {
        var login = await _loginService.GetLoginByUser(user);

        if (login is null)
        {
            return NotFound();
        }

        return login;
    }

    [HttpPost]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, KeyLengthLimit = int.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
    public async Task<IActionResult> Post(Login newLogin)
    {
        await _loginService.CreateAsync(newLogin);

        return CreatedAtAction(nameof(Get), new { id = newLogin.Id }, newLogin);
    }

    [HttpPut("{id:length(24)}")]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, KeyLengthLimit = int.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
    public async Task<IActionResult> Update(string id, Login updatedLogin)
    {
        var login = await _loginService.GetAsync(id);

        if (login is null)
        {
            return NotFound();
        }

        updatedLogin.Id = login.Id;

        await _loginService.UpdateAsync(id, updatedLogin);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, KeyLengthLimit = int.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
    public async Task<IActionResult> Delete(string id)
    {
        var login = await _loginService.GetAsync(id);

        if (login is null)
        {
            return NotFound();
        }

        await _loginService.RemoveAsync(id);

        return NoContent();
    }
}