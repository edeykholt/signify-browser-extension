using System.Text.Json;

namespace KeriAuth.SignifyExtension.Helper;

/// <summary>
/// Custom policy to convert enums to uppercase when serializing to Json
/// </summary>
public class UpperCase : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        return name.ToUpperInvariant();
    }
}