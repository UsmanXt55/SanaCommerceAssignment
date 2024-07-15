using System.Net;
namespace SanaCommerceAssignment.ConfigurableEditor.Shared.Response;
public record ApiResponse200Ok(string Message) : ApiBaseResponse(HttpStatusCode.OK, Message);
