using Avalonia.Input;
using Blast.Core.Results;

namespace YoutubeSearch.Fluent.Plugin
{
	public class YTCopyURLOperation : SearchOperationBase
	{
		internal const string IconGlyph = "\uEA39"; // I stolen this glyph from another plugin. No idea how it looks like lol.
		public YTCopyURLOperation() : base("Copy URL", "Copies the URL of the currently selected video", IconGlyph)
		{
			HideMainWindow = false;
			KeyGesture = new(Key.C, KeyModifiers.Control);
		}
	}
}
