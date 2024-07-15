using System.Net;
namespace SanaCommerceAssignment.ConfigurableEditor.Shared.Response;
public record ApiBaseResponse(HttpStatusCode Code, string Message);
