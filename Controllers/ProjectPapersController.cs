using PapersApi.Models;
using PapersApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Features;

namespace PapersApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectPapersController : ControllerBase
{
    private readonly ProjectPapersService _projectPapersService;

    public ProjectPapersController(ProjectPapersService projectPapersService) =>
        _projectPapersService = projectPapersService;

    [HttpGet]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, KeyLengthLimit = int.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
    public async Task<List<ProjectPapers>> Get() =>
        await _projectPapersService.GetAsync();

    [HttpGet("{id:length(24)}")]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, KeyLengthLimit = int.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
    public async Task<ActionResult<ProjectPapers>> Get(string id)
    {
        var projectPapers = await _projectPapersService.GetAsync(id);

        if (projectPapers is null)
        {
            return NotFound();
        }

        return projectPapers;
    }

    [HttpGet("pr")]
    public async Task<ActionResult<ProjectPapers>> GetProjectPapersByProject(string project)
    {
        var projectPapers = await _projectPapersService.GetProjectPapersByProject(project);

        if (projectPapers is null)
        {
            return NotFound();
        }

        return projectPapers;
    }

    [HttpPost]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, KeyLengthLimit = int.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
    public async Task<IActionResult> Post(ProjectPapers newProjectPapers)
    {
        await _projectPapersService.CreateAsync(newProjectPapers);

        return CreatedAtAction(nameof(Get), new { id = newProjectPapers.Id }, newProjectPapers);
    }

    [HttpPut("{id:length(24)}")]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, KeyLengthLimit = int.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
    public async Task<IActionResult> Update(string id, ProjectPapers updatedProjectPapers)
    {
        var projectPapers = await _projectPapersService.GetAsync(id);

        if (projectPapers is null)
        {
            return NotFound();
        }

        updatedProjectPapers.Id = projectPapers.Id;

        await _projectPapersService.UpdateAsync(id, updatedProjectPapers);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, KeyLengthLimit = int.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
    public async Task<IActionResult> Delete(string id)
    {
        var projectPapers = await _projectPapersService.GetAsync(id);

        if (projectPapers is null)
        {
            return NotFound();
        }

        await _projectPapersService.RemoveAsync(id);

        return NoContent();
    }
}