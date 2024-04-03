using System.Text.Json.Serialization;

namespace KeriAuth.SignifyExtension.Models
{
    public class WalletCleartext
    {
        [JsonConstructor]
        public WalletCleartext(Preferences preferencesConfig, KeriaConfiguration KeriaConfigs, List<Identifier> identifiers, List<Credential> credentials, List<Website> websites, List<WebsiteInteractionThread> websiteInteractions)
        {
            Prefs = preferencesConfig;
            KeriaConfig = KeriaConfigs;
            Identifiers = identifiers;
            Credentials = credentials;
            Websites = websites;
            WebsiteInteractions = websiteInteractions;
            TimeCreatedUtc = DateTime.UtcNow;
        }

        [JsonPropertyName("prefs")]
        public Preferences Prefs { get; }

        [JsonPropertyName("keria")]
        public KeriaConfiguration KeriaConfig { get; }

        [JsonPropertyName("ids")]
        public List<Identifier> Identifiers { get; }

        [JsonPropertyName("creds")]
        public List<Credential> Credentials { get; }

        [JsonPropertyName("websites")]
        public List<Website> Websites { get; }

        [JsonPropertyName("wis")]
        public List<WebsiteInteractionThread> WebsiteInteractions { get; }

        [JsonPropertyName("tcUTC")]
        public DateTime TimeCreatedUtc { get; }
    }
}
