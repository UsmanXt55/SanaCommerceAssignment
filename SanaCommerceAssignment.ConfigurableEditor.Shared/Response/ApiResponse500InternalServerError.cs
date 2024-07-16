using System.Net;
namespace SanaCommerceAssignment.ConfigurableEditor.Shared.Response;
public record ApiResponse500InternalServerError(string Message) : ApiBaseResponse(HttpStatusCode.InternalServerError, Message);