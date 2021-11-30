using Avalonia.Input;
using Blast.Core.Results;
namespace YoutubeSearch.Fluent.Plugin
{
	public class YTOpenInBrowserOperation : SearchOperationBase
	{
		internal const string IconGlyph = "\uEA39"; // I stolen this glyph from another plugin. No idea how it looks like lol.
		public YTOpenInBrowserOperation() : base("Open in browser", "Opens the selected video in the default browser", IconGlyph)
		{
			HideMainWindow = true;
			KeyGesture = new(Key.Enter, KeyModifiers.Control);
		}
	}
}
