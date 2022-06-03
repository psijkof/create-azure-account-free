using BlazorApp.Client.Converter;
using MudBlazor;
using System.Text.Json;

namespace TestJson
{
    public class TestConverter
    {
        [Fact]
        public void MudColor_Converter_Deserializes()
        {
            var serializerOptions = new JsonSerializerOptions { WriteIndented = true };
            serializerOptions.Converters.Add(new MudColorConverter());
            var json = File.ReadAllText(Path.Combine("Assets", "_mudBlazorTheme.json"));

            var uploadedPalette = System.Text.Json.JsonSerializer.Deserialize<MudTheme>(json, serializerOptions) ?? default;

            Assert.NotNull(uploadedPalette);
            Assert.Equal("#ffd14dff", uploadedPalette.Palette.AppbarBackground.Value);

        }
    }
}