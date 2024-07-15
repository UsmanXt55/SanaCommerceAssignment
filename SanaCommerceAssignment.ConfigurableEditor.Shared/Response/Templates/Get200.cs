using SanaCommerceAssignment.ConfigurableEditor.Shared.DTOs.Templates;
using System.Net;
namespace SanaCommerceAssignment.ConfigurableEditor.Shared.Response.Templates;
public record Get200(IEnumerable<GetDto> Fields) : ApiBaseResponse(HttpStatusCode.OK, "Retrieved");
