using System;
using System.Text.Json;

namespace Titan.Core.Json
{
    internal class JsonSerializerWrapper : IJsonSerializer
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        public T Deserialize<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                throw new ArgumentNullException(nameof(json));
            }

            var deserialize = JsonSerializer.Deserialize<T>(json, _options);
            return deserialize;
        }
    }
}
