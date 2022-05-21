using Avalonia.Input;
using Blast.Core.Results;

namespace YoutubeSearch.Fluent.Plugin
{
    /// <inheritdoc />
    public class YTOpenInBrowserOperation : SearchOperationBase
    {
        /// <inheritdoc />
        public YTOpenInBrowserOperation() : base("Open in browser", "Opens the selected video in the default browser",
            "\uE8A7")
        {
            HideMainWindow = true;
            KeyGesture = new(Key.Enter, KeyModifiers.Control);
        }
    }
}