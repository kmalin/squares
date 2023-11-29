using Microsoft.AspNetCore.Mvc;
using SquareApi.BusinessDomain;

namespace SquareApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{
    private readonly ISquareService _service;

    public ApiController(ISquareService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("Squares")]
    public async Task<IEnumerable<int[][]>> GetSquares()
    {
        var squares = await _service.GetSquares();
        return squares.Select(ApiMapper.SquareToArrays);
    }

    /// <summary>
    /// Expects an array of coordinates, that each is represented by a coordinate array (length of 2).
    /// </summary>
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Import(int[][] points)
    {
        await _service.SetList(points.Select(ApiMapper.ArrayToPoint).ToList());
        return Ok();
    }
    
    /// <summary>
    /// Expects coordinate array (length of 2).
    /// </summary>
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Add(int[] point)
    {
        await _service.AddPoint(ApiMapper.ArrayToPoint(point));
        return Ok();
    }

    /// <summary>
    /// Expects coordinate array (length of 2).
    /// </summary>
    [HttpDelete]
    [Route("[action]")]
    public async Task<IActionResult> Delete(int[] point)
    {
        await _service.DeletePoint(ApiMapper.ArrayToPoint(point));
        return Ok();
    }
}