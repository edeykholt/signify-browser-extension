using System.Text.Json;

namespace KeriAuth.SignifyExtension.Helper;

/// <summary>
/// Custom policy to convert enums to uppercase when serializing to Json
/// </summary>
public class UpperCase : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        return name.ToUpperInvariant();
    }
}