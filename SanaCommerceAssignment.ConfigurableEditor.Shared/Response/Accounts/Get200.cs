using SanaCommerceAssignment.ConfigurableEditor.Shared.DTOs.Users;
using System.Net;
namespace SanaCommerceAssignment.ConfigurableEditor.Shared.Response.Accounts;
public record Get200(TokenDto Token) : ApiBaseResponse(HttpStatusCode.OK, "Retrieved");