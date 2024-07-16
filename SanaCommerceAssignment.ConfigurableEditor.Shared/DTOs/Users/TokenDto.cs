using SanaCommerceAssignment.ConfigurableEditor.Shared.Enums;
namespace SanaCommerceAssignment.ConfigurableEditor.Shared.DTOs.Users;
public record TokenDto(string Token, UserTypeEnum Type, DateTime ExpiresOn);