using SanaCommerceAssignment.ConfigurableEditor.Shared.Enums;
namespace SanaCommerceAssignment.ConfigurableEditor.Shared.DTOs.Users;
public record BaseDto(string Username, string Password, UserTypeEnum Type);
