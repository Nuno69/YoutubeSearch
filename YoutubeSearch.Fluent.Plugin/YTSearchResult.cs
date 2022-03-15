using Blast.API.Search;
using Blast.Core.Interfaces;
using Blast.Core.Objects;
using Blast.Core.Results;
using System.Collections.ObjectModel;
using System.Net;
using YoutubeExplode;
using YoutubeExplode.Videos;
namespace YoutubeSearch.Fluent.Plugin
{
	/// <summary>
	/// provides  the Youtube search result to Fluent Search, allowing to then operate on it further.
	/// </summary>
	public sealed class YTSearchResult : SearchResultBase
	{
		internal static readonly ObservableCollection<ISearchOperation> SupportedOperations = new() { new YTOpenInBrowserOperation(), new YTDownloadVideoOperation(), new YTCopyURLOperation(), new YTPlayInExternalMediaPlayerOperation() };
		private static readonly SearchTag[] SearchTags = { new() { Name = "Youtube" } };
		private WebClient _client;
		public IVideo Video { get; set; }
		public YTSearchResult(IVideo video, string searchedText, ProcessInfo processInfo = null) : base(video.Title, searchedText, "Youtube",video.Title.SearchTokens(searchedText), SupportedOperations, SearchTags, processInfo)
		{
			_client = new WebClient();
			Video = video;
			AdditionalInformation = video.Author.Title;
			PreviewImage = new BitmapImageResult( new MemoryStream(_client.DownloadData(video.Thumbnails.FirstOrDefault().Url)));
			ShouldCacheResult = true;
		}

		protected override void OnSelectedSearchResultChanged()
		{

		}
	}
}