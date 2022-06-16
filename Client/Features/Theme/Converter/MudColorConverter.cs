using MudBlazor.Utilities;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorApp.Client.Features.Theme.Converter
{
    public class MudColorConverter : JsonConverter<MudColor>
    {
        public override MudColor? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            MudColor? returnColor = null;
            while (reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    returnColor = new MudColor(reader.GetString()!);
                }
                reader.Read();
            }
            if (returnColor is not null)
            {
                return returnColor;
            }

            throw new JsonException("MudColorConverter.Read: Unexpected end of object");
        }

        public override void Write(Utf8JsonWriter writer, MudColor value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        private void LogToConsole(Utf8JsonReader reader)
        {
            try
            {
                Debug.WriteLine($"{Enum.GetName(reader.TokenType)} -> {reader.GetString()!}");
            }
            catch { }
        }
    }
}