using Microsoft.AspNetCore.Mvc;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Infrastructure.Attributes;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Infrastructure.Services;
using SanaCommerceAssignment.ConfigurableEditor.Portal.Models.ViewModels;
using SanaCommerceAssignment.ConfigurableEditor.Shared.DTOs.Templates;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Enums;
using SanaCommerceAssignment.ConfigurableEditor.Shared.Requests.Data;
namespace SanaCommerceAssignment.ConfigurableEditor.Portal.Controllers;
public class HomeController(
    ITemplateService templateService,
    IDataService dataService) : Controller
{
    private readonly string __PageId = "UserProfile";

    [XAuthorize(UserTypeEnum.Client)]
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var result = await templateService.GetAsync(__PageId, cancellationToken);
        if (!result.Success)
            throw new Exception();

        var fields = (List<GetDto>)result.Obj!;
        var fieldsViewModel = new List<FieldViewEntryViewModel>();
        fields.ForEach(field =>
        {
            fieldsViewModel.Add(new(field.FieldTitle, field.FieldType, field.Readonly, ""));
        });

        return View(fieldsViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Index(List<FieldViewEntryViewModel> model, CancellationToken cancellationToken)
    {
        var dataList = new List<CreateData>();
        model.ForEach(m =>
        {
            dataList.Add(new(__PageId, m.Name, m.Value));
        });

        var result = await dataService.PostAsync(dataList, cancellationToken);
        if (!result.Success)
        {
            ViewBag.Error = result.Message;
        }
        else
        {
            ViewBag.Message = "Data Posted";
        }
        return View(model);
    }

    [XAuthorize(UserTypeEnum.Admin)]
    public IActionResult Admin()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Admin([FromBody] List<FieldViewModel> fields, CancellationToken cancellationToken)
    {
        var result = await templateService.PostAsync(__PageId, fields, cancellationToken);
        if (!result.Success)
            return new JsonResult(new { success = false, message = result.Message });

        return new JsonResult(new { success = true, message = result.Message });
    }
}
