using System.Net;
namespace SanaCommerceAssignment.ConfigurableEditor.Shared.Response;
public record ApiResponse400BadRequest(string Message) : ApiBaseResponse(HttpStatusCode.BadRequest, Message);
