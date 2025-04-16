using PapersApi.Models;
using PapersApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Features;

namespace PapersApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GraphDataController : ControllerBase
{
    private readonly GraphDataService _graphDataService;

    public GraphDataController(GraphDataService graphDataService) =>
        _graphDataService = graphDataService;

    [HttpGet]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, KeyLengthLimit = int.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
    public async Task<List<GraphData>> Get() =>
        await _graphDataService.GetAsync();

    [HttpGet("{id:length(24)}")]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, KeyLengthLimit = int.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
    public async Task<ActionResult<GraphData>> Get(string id)
    {
        var graphData = await _graphDataService.GetAsync(id);

        if (graphData is null)
        {
            return NotFound();
        }

        return graphData;
    }

    [HttpGet("us")]
    public async Task<ActionResult<List<GraphData?>>> GetGraphDataByUser(string user)
    {
        return await _graphDataService.GetGraphDataByUser(user);
    }

    [HttpPost]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, KeyLengthLimit = int.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
    public async Task<IActionResult> Post(GraphData newGraphData)
    {
        await _graphDataService.CreateAsync(newGraphData);

        return CreatedAtAction(nameof(Get), new { id = newGraphData.Id }, newGraphData);
    }

    [HttpPut("{id:length(24)}")]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, KeyLengthLimit = int.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
    public async Task<IActionResult> Update(string id, GraphData updatedGraphData)
    {
        var graphData = await _graphDataService.GetAsync(id);

        if (graphData is null)
        {
            return NotFound();
        }

        updatedGraphData.Id = graphData.Id;

        await _graphDataService.UpdateAsync(id, updatedGraphData);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, KeyLengthLimit = int.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
    public async Task<IActionResult> Delete(string id)
    {
        var graphData = await _graphDataService.GetAsync(id);

        if (graphData is null)
        {
            return NotFound();
        }

        await _graphDataService.RemoveAsync(id);

        return NoContent();
    }
}