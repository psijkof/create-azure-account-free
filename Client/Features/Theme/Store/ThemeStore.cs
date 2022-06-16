using BlazorApp.Client.Features.Theme.Converter;
using BlazorApp.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorApp.Client.Features.Theme.Store
{
    /// <summary>
    /// Theme State
    /// </summary>
    /// <param name="MudTheme"></param>
    /// <param name="File"></param>
    /// <param name="IsLoading"></param>
    /// <param name="IsInitialized"></param>
    public record ThemeState(MudTheme MudTheme, IBrowserFile? File, bool IsLoading, bool IsInitialized);

    /// <summary>
    /// Theme Reducers
    /// </summary>
    public static class ThemeReducers
    {
        [ReducerMethod]
        public static ThemeState OnLoadTheme(ThemeState state, ThemeStateLoadAction action)
        {
            return state with
            {
                IsLoading = true,
                File = action.BrowserFile
            };
        }

        [ReducerMethod]
        public static ThemeState OnSetTheme(ThemeState state, ThemeStateSetAction action)
        {
            return state with
            {
                IsInitialized = true,
                IsLoading = false,
                MudTheme = action.MudTheme
            };
        }
    }

    /// <summary>
    /// Theme Effects
    /// </summary>
    public class ThemeEffect
    {
        private readonly LocalhostHttpClient _localhostService;
        private readonly NavigationManager _navigationManager;

        public ThemeEffect(LocalhostHttpClient localhostService, NavigationManager navigationManager)
        {
            _localhostService = localhostService;
            _navigationManager = navigationManager;
        }

        /// <summary>
        /// Load the theme file (or default if none specified)
        /// </summary>
        /// <param name="action"></param>
        /// <param name="dispatcher"></param>
        /// <returns></returns>
        [EffectMethod]
        public async Task ThemeStateLoadEffect(ThemeStateLoadAction action, IDispatcher dispatcher)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            options.Converters.Add(new MudColorConverter());
            MudTheme theme;

            if (action.BrowserFile is null)
            {
                theme = await _localhostService?.HttpClient?.GetFromJsonAsync<MudTheme>("_mudBlazorTheme.json", options)! ?? new MudTheme();
                dispatcher.Dispatch(new ThemeStateSetAction(theme));
            }
            else
            {
                var json = await new StreamReader(action.BrowserFile!.OpenReadStream(maxAllowedSize: 10485760)).ReadToEndAsync();
                theme = JsonSerializer.Deserialize<MudTheme>(json, options) ?? new MudTheme();
                dispatcher.Dispatch(new ThemeStateSetAction(theme));
            }
        }

        /// <summary>
        /// Hack effect to enforce theme refresh after loading it
        /// </summary>
        /// <param name="action"></param>
        /// <param name="dispatcher"></param>
        /// <returns></returns>
        [EffectMethod]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task ThemeStateSetEffect(ThemeStateSetAction action, IDispatcher dispatcher)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            _navigationManager.NavigateTo("/");
        }
    }

    /// <summary>
    /// Theme load feature
    /// </summary>
    public class ThemeLoadFeature : Feature<ThemeState>
    {
        public override string GetName() => nameof(ThemeLoadFeature);

        protected override ThemeState GetInitialState()
        {
            return new ThemeState(new MudTheme(), null, false, false);
        }
    }

    #region ThemeStateLoader Actions

    public record ThemeStateLoadAction(IBrowserFile? BrowserFile);
    public record ThemeStateSetAction(MudTheme MudTheme);

    #endregion ThemeStateLoader Actions
}