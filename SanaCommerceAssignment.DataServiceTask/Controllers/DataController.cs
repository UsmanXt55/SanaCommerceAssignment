using Microsoft.AspNetCore.Mvc;
using SanaCommerceAssignment.DataServiceTask.Infrastructure.Services.Interfaces;
namespace SanaCommerceAssignment.DataServiceTask.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DataController(
    IDataService dataService) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var lines = dataService.GetLines();
        return Ok(lines);
    }
}
