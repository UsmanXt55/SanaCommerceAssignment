using Microsoft.AspNetCore.Mvc;
using SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Attributes;
using SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Services;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Enums;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Requests.Data;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Response;
namespace SanaCommerceAssignment.ConfigurableEditor.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DataController(
    IDataService dataService) : ControllerBase
{
    [HttpPost]
    [AuthorizeUserType(UserTypeEnum.Client)]
    public async Task<IActionResult> Post(List<CreateData> request, CancellationToken cancellationToken)
    {
        var result = await dataService.PostAsync(request, cancellationToken);
        if (!result.Success)
            return BadRequest(new ApiResponse400BadRequest(result.Message));

        return Created(nameof(DataController), new ApiResponse201Created());
    }
}
