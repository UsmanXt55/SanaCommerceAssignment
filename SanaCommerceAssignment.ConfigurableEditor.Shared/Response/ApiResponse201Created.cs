using System.Net;
namespace SanaCommerceAssignment.ConfigurableEditor.Shared.Response;
public record ApiResponse201Created() : ApiBaseResponse(HttpStatusCode.Created, "Created");