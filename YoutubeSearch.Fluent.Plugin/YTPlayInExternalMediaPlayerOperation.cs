using Avalonia.Input;
using Blast.Core.Results;

namespace YoutubeSearch.Fluent.Plugin
{
	public class YTPlayInExternalMediaPlayerOperation : SearchOperationBase
	{
		internal const string IconGlyph = "\uEA39"; // I stolen this glyph from another plugin. No idea how it looks like lol.
		public YTPlayInExternalMediaPlayerOperation() : base("Play in external media player", "Plays the currently specified video in an external media player, either audio or video", IconGlyph)
		{
			HideMainWindow = true;
			KeyGesture = new(Key.Enter, KeyModifiers.Shift);
		}
	}
}
