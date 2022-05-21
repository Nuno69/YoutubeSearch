using Avalonia.Input;
using Blast.Core.Results;

namespace YoutubeSearch.Fluent.Plugin
{
    /// <inheritdoc />
    public class YTDownloadVideoOperation : SearchOperationBase
    {
        /// <inheritdoc />
        public YTDownloadVideoOperation() : base("Download video", "Downloads the specified video to disk", "\uE896")
        {
            HideMainWindow = true;
            KeyGesture = new(Key.D, KeyModifiers.Control);
        }
    }
}