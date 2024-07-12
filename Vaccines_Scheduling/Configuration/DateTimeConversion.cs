using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vaccines_Scheduling.Webapi.Configuration
{
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        private readonly string _format;

        public DateTimeJsonConverter(string format = "yyyy-MM-dd")
        {
            _format = format;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return DateTime.ParseExact(value, _format, System.Globalization.CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_format));
        }
    }
}
