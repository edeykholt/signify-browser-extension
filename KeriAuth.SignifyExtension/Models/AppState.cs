using static KeriAuth.SignifyExtension.Services.IStateService;

namespace KeriAuth.SignifyExtension.Models
{
    using System.Text.Json.Serialization;

    public class AppState
    {
        [JsonConstructor]
        public AppState(States currentState)
        {
            CurrentState = currentState;
            WriteUtc = DateTime.UtcNow;
        }

        [JsonPropertyName("cs")] public States CurrentState { get; }
        [JsonPropertyName("wUTC")] public DateTime WriteUtc { get; }
    }
}