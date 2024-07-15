﻿using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vaccines_Scheduling.Webapi.Configuration
{
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private readonly string _format;

        public DateOnlyJsonConverter(string format = "yyyy-MM-dd")
        {
            _format = format;
        }

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return DateOnly.ParseExact(value, _format);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_format));
        }
    }
}
