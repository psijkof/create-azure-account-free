using BlazorApp.Client.Services;

namespace BlazorApp.Client.Features.AzureFree.Store
{
    /// <summary>
    /// AzureFree Effects
    /// </summary>
    public class AzureFreeEffects
    {
        private readonly HttpClient _httpClient;

        public AzureFreeEffects(LocalhostHttpClient client)
        {
            _httpClient = client.HttpClient ?? new HttpClient();
        }

        [EffectMethod(typeof(AzureFreeLoadAction))]
        public async Task LoadAzureFreeHtml(IDispatcher dispatcher)
        {
            var response = await _httpClient.GetStringAsync("/azure.html");
            dispatcher.Dispatch(new AzureFreeSetAction(response));
        }
    }

    /// <summary>
    /// AzureFree Reducers
    /// </summary>
    public class AzureFreeReducers
    {
        [ReducerMethod(typeof(AzureFreeLoadAction))]
        public static AzureFreeState OnLoadAzureFree(AzureFreeState state)
        {
            return state with
            {
                IsLoading = true
            };
        }

        [ReducerMethod]
        public static AzureFreeState OnSetAzureFreeHtml(AzureFreeState state, AzureFreeSetAction action)
        {
            return state with
            {
                AzureFreeHtml = action.AzureFreeHtml,
                IsInitialized = true,
                IsLoading = false
            };
        }
    }

    /// <summary>
    /// AzureFree Feature
    /// </summary>
    public class Feature : Feature<AzureFreeState>
    {
        public override string GetName() => "AzureFree";

        protected override AzureFreeState GetInitialState()
        {
            return new AzureFreeState
            {
                IsLoading = false,
                IsInitialized = false,
                AzureFreeHtml = String.Empty
            };
        }
    }

    /// <summary>
    /// AzureFree State
    /// </summary>
    /// <param name="AzureFreeHtml"></param>
    public record AzureFreeState()
    {
        public string AzureFreeHtml { get; init; } = String.Empty;
        public bool IsLoading { get; init; }
        public bool IsInitialized { get; init; }
    }

    #region AzureFreeActions

    public record AzureFreeLoadAction();
    public record AzureFreeSetAction(string AzureFreeHtml);

    #endregion AzureFreeActions
}