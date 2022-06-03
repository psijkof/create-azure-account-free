namespace BlazorApp.Client.Interop
{
    public class SharedState
    {
        private bool _isSaving;
        private string? _savedString;
        private Dictionary<Guid, string>? _renderCache = new();

        public event Action? OnChange;

        public string SharedString
        {
            get => _savedString ?? string.Empty;
            set
            {
                _savedString = value;
                NotifyStateChanged();
            }
        }

        public bool IsPerformingBackgroundTask
        {
            get => _isSaving;
            set
            {
                _isSaving = value;
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
