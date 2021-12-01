using Blast.API.Search;
using Blast.Core.Interfaces;
using Blast.Core.Objects;
using Blast.Core.Results;
using System.Collections.ObjectModel;
using YoutubeExplode;
using YoutubeExplode.Videos;
namespace YoutubeSearch.Fluent.Plugin
{
	/// <summary>
	/// provides  the Youtube search result to Fluent Search, allowing to then operate on it further.
	/// </summary>
	public sealed class YTSearchResult : SearchResultBase
	{
		internal static readonly ObservableCollection<ISearchOperation> SupportedOperations = new() { new YTOpenInBrowserOperation(), new YTDownloadVideoOperation(), new YTCopyURLOperation() };
		private static readonly SearchTag[] SearchTags = { new() { Name = "Youtube" } };
		public IVideo Video { get; set; }
		public YTSearchResult(IVideo video, string searchedText, ProcessInfo processInfo = null) : base(video.Title, searchedText, "Youtube",video.Title.SearchTokens(searchedText), SupportedOperations, SearchTags, processInfo)
		{
			Video = video;
			AdditionalInformation = video.Author.Title;
			ShouldCacheResult = true;
		}

		protected override void OnSelectedSearchResultChanged()
		{

		}
	}
}