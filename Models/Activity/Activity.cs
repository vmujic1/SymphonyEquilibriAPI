﻿using System.Text.Json.Serialization;

namespace SymphonyEquilibriAPI.Dtos.Activity
{
    [JsonConverter(typeof(JsonStringEnumConverter))]

    public enum Activity
    {
        Running = 0,
        Swimming = 1,
        WeightLifting = 2,
        PlayingGames = 3,
    }
}
