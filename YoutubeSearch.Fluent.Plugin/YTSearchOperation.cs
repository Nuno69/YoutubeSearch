using Blast.Core.Results;

namespace YoutubeSearch.Fluent.Plugin
{
	public class YTSearchOperation : SearchOperationBase
	{
		public YTOperationType YTOperation{get; }
		public static YTSearchOperation OpenInBrowserOperation { get; } = new(YTOperationType.OPENINBROWSER);

	}
}
