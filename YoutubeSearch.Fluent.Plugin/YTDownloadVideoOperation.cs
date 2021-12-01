using Avalonia.Input;
using Blast.Core.Results;

namespace YoutubeSearch.Fluent.Plugin
{
	public  class YTDownloadVideoOperation : SearchOperationBase
	{
		internal const string IconGlyph = "\uEA39"; // I stolen this glyph from another plugin. No idea how it looks like lol.
		public YTDownloadVideoOperation() : base("Download video", "Downloads the specified video to disk", IconGlyph)
		{
			HideMainWindow = true;
			KeyGesture = new(Key.D, KeyModifiers.Control);
		}
	}
}
