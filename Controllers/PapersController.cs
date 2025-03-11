using PapersApi.Models;
using PapersApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace PapersApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PapersController : ControllerBase
{
    private readonly PapersService _papersService;

    public PapersController(PapersService papersService) =>
        _papersService = papersService;

    [HttpGet]
    public async Task<List<Paper>> Get() =>
        await _papersService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Paper>> Get(string id)
    {
        var paper = await _papersService.GetAsync(id);

        if (paper is null)
        {
            return NotFound();
        }

        return paper;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Paper newPaper)
    {
        await _papersService.CreateAsync(newPaper);

        return CreatedAtAction(nameof(Get), new { id = newPaper.Id }, newPaper);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Paper updatedPaper)
    {
        var paper = await _papersService.GetAsync(id);

        if (paper is null)
        {
            return NotFound();
        }

        updatedPaper.Id = paper.Id;

        await _papersService.UpdateAsync(id, updatedPaper);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var paper = await _papersService.GetAsync(id);

        if (paper is null)
        {
            return NotFound();
        }

        await _papersService.RemoveAsync(id);

        return NoContent();
    }
}