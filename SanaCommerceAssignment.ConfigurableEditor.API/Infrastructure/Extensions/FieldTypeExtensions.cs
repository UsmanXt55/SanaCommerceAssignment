using SanaCommerceAssignment.ConfigurableEditor.Shared.Enums;
using System.Globalization;
using System.Text.RegularExpressions;
namespace SanaCommerceAssignment.ConfigurableEditor.API.Infrastructure.Extensions;
public static class FieldTypeExtensions
{
    public static string GetInputType(this FieldTypeEnum type)
    {
        return type switch
        {
            FieldTypeEnum.Text => "text",
            FieldTypeEnum.Numeric => "number",
            FieldTypeEnum.Date => "date",
            FieldTypeEnum.TextReadOnly => "text",
            FieldTypeEnum.NumericReadOnly => "number",
            FieldTypeEnum.DateReadOnly => "date",
            _ => ""
        };
    }

    public static bool GetInputReadonly(this FieldTypeEnum type)
    {
        return type switch
        {
            FieldTypeEnum.TextReadOnly => true,
            FieldTypeEnum.NumericReadOnly => true,
            FieldTypeEnum.DateReadOnly => true,
            _ => false
        };
    }

    public static bool ValidateValue(this FieldTypeEnum type, string? value)
    {
        return type switch
        {
            FieldTypeEnum.Text => Regex.IsMatch(value, @"^[A-Za-z ]+$"),
            FieldTypeEnum.Numeric => Regex.IsMatch(value, @"^(?:\d+|\d*\.\d+)$"),
            FieldTypeEnum.Date => ValidateDate(value),
            FieldTypeEnum.DateReadOnly => true,
            FieldTypeEnum.TextReadOnly => true,
            FieldTypeEnum.NumericReadOnly => true,
            _ => false
        };
    }

    private static bool ValidateDate(string value)
    {
        try
        {
            return DateTime.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
