using System.Text.Json.Serialization;
using System.Text.Json;

namespace LibraryService.Infrastructure.Converter
{
    public class DateOnlyConverter : JsonConverter<DateOnly>
    {
        private const string DateFormat = "yyyy-MM-dd";

        public override DateOnly Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    return DateOnly.ParseExact(reader.GetString()!, DateFormat);

                case JsonTokenType.StartObject:
                    using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
                    {
                        var root = doc.RootElement;
                        return new DateOnly(
                            root.GetProperty("year").GetInt32(),
                            root.GetProperty("month").GetInt32(),
                            root.GetProperty("day").GetInt32()
                        );
                    }

                default:
                    throw new JsonException($"Unsupported token type: {reader.TokenType}");
            }
        }

        public override void Write(
            Utf8JsonWriter writer,
            DateOnly value,
            JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(DateFormat));
        }
    }
}
