using System.Text.Json.Serialization;

namespace SymphonyEquilibriAPI.Models.User
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserRole
    {
        Admin = 0,
        ProjectManager = 1,
    }
}
