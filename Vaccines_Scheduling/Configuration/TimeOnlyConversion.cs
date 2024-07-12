using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vaccines_Scheduling.Webapi.Configuration
{
    public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
    {
        private readonly string _format;

        public TimeOnlyJsonConverter(string format = "HH")
        {
            _format = format;
        }

        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return TimeOnly.ParseExact(value, _format);
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_format));
        }
    }

}
