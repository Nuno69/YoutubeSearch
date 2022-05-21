using System.Collections.ObjectModel;
using Blast.API.Search;
using Blast.API.Search.SearchOperations;
using Blast.Core.Interfaces;
using Blast.Core.Results;
using YoutubeExplode.Videos;

namespace YoutubeSearch.Fluent.Plugin
{
    /// <summary>
    /// provides  the Youtube search result to Fluent Search, allowing to then operate on it further.
    /// </summary>
    public sealed class YTSearchResult : SearchResultBase
    {
        internal static readonly ObservableCollection<ISearchOperation> YTSupportedOperations = new()
        {
            new YTOpenInBrowserOperation(), new CopySearchOperation(),
            // new YTDownloadVideoOperation(),
            // new YTPlayInExternalMediaPlayerOperation()
        };

        private static readonly SearchTag[] SearchTags = {new() {Name = "Youtube"}};
        public IVideo Video { get; set; }

        public YTSearchResult(IVideo video, string searchedText) : base(
            video.Title,
            searchedText, "Youtube", video.Title.SearchTokens(searchedText), YTSupportedOperations, SearchTags)
        {
            IsResultLoaded = false;
            Video = video;
            AdditionalInformation = video.Author.ChannelTitle;
            UseIconGlyph = true;
            IconGlyph = "\uE714";
            ShouldCacheResult = false;
            CanPin = false;
            Context = video.Url;
        }

        public override void LoadSearchResult()
        {
            IsResultLoaded = true;
            base.LoadSearchResult();
        }

        protected override void OnSelectedSearchResultChanged()
        {
        }
    }
}