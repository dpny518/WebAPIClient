using System;
using System.Text.Json.Serialization;

// Repository class to represent GitHub repositories
public sealed record class Repository(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("html_url")] Uri GitHubHomeUrl,
    [property: JsonPropertyName("homepage")] Uri Homepage,
    [property: JsonPropertyName("watchers")] int Watchers,
    [property: JsonPropertyName("pushed_at")] DateTime LastPushUtc)
{
    // Method to convert LastPush time from UTC to local time
    public DateTime LastPush => LastPushUtc.ToLocalTime();
}