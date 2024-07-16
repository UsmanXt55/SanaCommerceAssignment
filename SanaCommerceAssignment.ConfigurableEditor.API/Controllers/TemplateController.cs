using Microsoft.AspNetCore.Mvc;
using SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Attributes;
using SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Services;
using SanaCommerceAssignment.ConfigurableEditor.Shared.DTOs.Templates;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Enums;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Requests.Templates;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Response;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Response.Templates;
namespace SanaCommerceAssignment.ConfigurableEditor.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TemplateController(
    ITemplateService templateService) : BaseController
{
    [HttpGet("{pageId}")]
    [AuthorizeUserType(UserTypeEnum.Client)]
    public async Task<IActionResult> Get(string pageId, CancellationToken cancellationToken)
    {
        var result = await templateService.GetAsync(pageId, cancellationToken);
        if (!result.Success)
            return BadRequest(new ApiResponse400BadRequest(result.Message));

        var dtoList = (List<GetDto>)result.Obj!;
        return Ok(new Get200(dtoList));
    }

    [HttpPost]
    [AuthorizeUserType(UserTypeEnum.Admin)]
    public async Task<IActionResult> Post(CreateTemplateRequest request, CancellationToken cancellationToken)
    {
        List<BaseDto> dtoList = new();
        foreach (var field in request.Fields.ToList())
        {
            dtoList.Add(new(0, request.PageId, field.FieldId, field.FieldTitle, field.Type));
        }

        var result = await templateService.CreateAsync(dtoList, cancellationToken);
        if(!result.Success)
            return BadRequest(new ApiResponse400BadRequest(result.Message));
        return Created(nameof(TemplateController), new ApiResponse201Created());
    }
}
