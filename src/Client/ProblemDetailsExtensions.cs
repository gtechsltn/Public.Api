﻿using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Client
{
    public static class ProblemDetailsExtensions
    {
        static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            WriteIndented = true
        };

        public static string ToJsonString(this ProblemDetails problemDetails)
        {
            return JsonSerializer.Serialize(problemDetails, JsonSerializerOptions);
        }
    }
}
