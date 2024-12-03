using System.Text.Json.Serialization;

namespace SymphonyEquilibriAPI.Models.Project
{
    [JsonConverter(typeof(JsonStringEnumConverter))]

    public enum ProjectIndustry
    {
        TechMediaTelecom = 0,
        VentureCapital = 1,
        Industrial = 2,
        Healthcare = 3,
        ConsumerRetail = 4
    }
}
