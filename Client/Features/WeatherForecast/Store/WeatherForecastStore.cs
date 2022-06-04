namespace BlazorApp.Client.Features.WeatherForecast.Store
{
    using BlazorApp.Client.Services;
    using BlazorApp.Shared;
    using System.Net.Http.Json;

    /// <summary>
    /// The state record definition
    /// </summary>
    public record WeatherForecastState
    {
        public WeatherForecast[] Forecasts { get; init; } = Array.Empty<WeatherForecast>();

        public bool IsLoading { get; init; }
        public bool Initialized { get; init; }
        public DateTime LastLoadCompleted { get; init; }
    }

    /// <summary>
    /// Reducer methods
    /// </summary>
    public static class WeatherForecastReducers
    {
        [ReducerMethod(typeof(WeatherLoadForecastAction))]
        public static WeatherForecastState OnLoadForecasts(WeatherForecastState state)
        {
            return state with
            {
                IsLoading = true
            };
        }

        [ReducerMethod]
        public static WeatherForecastState OnSetForecasts(WeatherForecastState state, WeatherSetForecastsAction action)
        {
            return state with
            {
                Forecasts = action.Forecasts,
                IsLoading = false,
                Initialized = true,
                LastLoadCompleted = DateTime.Now
            };
        }
    }

    /// <summary>
    /// Feature class
    /// </summary>
    public class Feature : Feature<WeatherForecastState>
    {
        public override string GetName() => nameof(BlazorApp.Shared.WeatherForecast);

        protected override WeatherForecastState GetInitialState()
        {
            return new WeatherForecastState
            {
                Initialized = false,
                IsLoading = false,
                Forecasts = Array.Empty<WeatherForecast>(),
                LastLoadCompleted = DateTime.MinValue
            };
        }
    }

    /// <summary>
    /// Effects methods
    /// </summary>
    public class WeatherForecastEffects
    {
        private readonly HttpClient? _http;

        public WeatherForecastEffects(AzFunctionHttpClient azFunctionService)
        {
            _http = azFunctionService?.HttpClient ?? new HttpClient();
        }

        [EffectMethod(typeof(WeatherLoadForecastAction))]
        public async Task LoadForecasts(IDispatcher dispatcher)
        {
            var forecasts = await _http?.GetFromJsonAsync<WeatherForecast[]>("/api/WeatherForecast")! ?? Array.Empty<WeatherForecast>();
            dispatcher.Dispatch(new WeatherSetForecastsAction(forecasts));
        }
    }

    #region WeatherActions

    public record WeatherLoadForecastAction();
    public record WeatherLoadForecastsSuccessAction();
    public record WeatherSetForecastsAction(WeatherForecast[] Forecasts);
    //public record WeatherSetStateAction(WeatherForecastState WeatherState);
    //public record WeatherLoadStateAction();
    //public record WeatherLoadStateSuccessAction();
    //public record WeatherLoadStateFailureAction(string ErrorMessage);
    //public record WeatherPersistStateAction(WeatherState WeatherState);
    //public record WeatherPersistStateSuccessAction();
    //public record WeatherPersistStateFailureAction(string ErrorMessage);
    //public record WeatherClearStateAction();
    //public record WeatherClearStateSuccessAction();
    //public record WeatherClearStateFailureAction(string ErrorMessage);

    #endregion WeatherActions
}