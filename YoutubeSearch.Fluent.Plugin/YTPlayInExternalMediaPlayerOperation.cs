using Avalonia.Input;
using Blast.Core.Results;

namespace YoutubeSearch.Fluent.Plugin
{
    /// <inheritdoc />
    public class YTPlayInExternalMediaPlayerOperation : SearchOperationBase
    {
        /// <inheritdoc />
        public YTPlayInExternalMediaPlayerOperation() : base("Play in external media player",
            "Plays the currently specified video in an external media player, either audio or video", "\uE768")
        {
            HideMainWindow = true;
            KeyGesture = new(Key.Enter, KeyModifiers.Shift);
        }
    }
}