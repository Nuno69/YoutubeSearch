using System.Runtime.CompilerServices;
using Blast.API.Core.UI;
using Blast.API.Processes;
using Blast.API.Search.SearchOperations;
using Blast.Core.Interfaces;
using Blast.Core.Objects;
using Blast.Core.Results;
using TextCopy;
using YoutubeExplode;
using YoutubeExplode.Search;
using ISearchResult = Blast.Core.Interfaces.ISearchResult;

namespace YoutubeSearch.Fluent.Plugin
{
    public class YTSearchApp : ISearchApplication
    {
        private readonly SearchApplicationInfo _searchApplicationInfo;
        private readonly YoutubeClient _client;

        public YTSearchApp()
        {
            _client = new YoutubeClient();
            _searchApplicationInfo = new SearchApplicationInfo("YoutubeSearch",
                "Allows to search Youtube without leaving Fluent Search", YTSearchResult.YTSupportedOperations)
            {
                SearchTagName = "Youtube",
                MinimumSearchLength = 3,
                MinimumTagSearchLength = 3,
                SearchTagOnly = true,
                IsProcessSearchEnabled = false,
                IsProcessSearchOffline = false,
                ApplicationIconGlyph = "\uE714",
                SearchAllTime = ApplicationSearchTime.Slow,
                DefaultSearchTags = new List<SearchTag>
                {
                    new()
                    {
                        Name = "Youtube"
                    }
                }
            };
        }

        public SearchApplicationInfo GetApplicationInfo()
        {
            return _searchApplicationInfo;
        }

        public async IAsyncEnumerable<ISearchResult> SearchAsync(SearchRequest searchRequest,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            string text = searchRequest.SearchedText;
            string tag = searchRequest.SearchedTag;
            if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(tag) ||
                !tag.Equals("Youtube", StringComparison.OrdinalIgnoreCase))
            {
                yield break;
            }

            int i = 0;
            await foreach (VideoSearchResult video in _client.Search.GetVideosAsync(text, cancellationToken))
            {
                if (i >= 15 || cancellationToken.IsCancellationRequested)
                    yield break;

                var ytSearchResult = new YTSearchResult(video, text);
                _ = Task.Run(async () =>
                {
                    if (cancellationToken.IsCancellationRequested)
                        return;
                    using var httpClient = new HttpClient();
                    BitmapImageResult? bitmapImageResult = null;
                    string? requestUri = video.Thumbnails.FirstOrDefault()?.Url;
                    if (!string.IsNullOrEmpty(requestUri))
                    {
                        var buffer = await httpClient.GetByteArrayAsync(requestUri, cancellationToken);
                        bitmapImageResult = new BitmapImageResult(new MemoryStream(buffer));
                    }

                    void UpdatePreviewImage()
                    {
                        ytSearchResult.PreviewImage = bitmapImageResult;
                        ytSearchResult.UseIconGlyph = false;
                    }

                    if (ytSearchResult.IsResultLoaded)
                    {
                        UiUtilities.UiDispatcher.Post(UpdatePreviewImage, cancellationToken: cancellationToken);
                    }
                    else UpdatePreviewImage();
                }, cancellationToken);

                yield return ytSearchResult;
                i++;
            }
        }

        public ValueTask<IHandleResult> HandleSearchResult(ISearchResult searchResult)
        {
            if (searchResult is not YTSearchResult yTSearchResult)
            {
                throw new InvalidOperationException(nameof(YTSearchResult));
            }

            switch (searchResult.SelectedOperation)
            {
                case YTOpenInBrowserOperation:
                    ProcessUtils.GetManagerInstance().StartNewProcess(yTSearchResult.Video.Url);
                    break;
                case CopySearchOperation:
                    Clipboard.SetText(yTSearchResult.Video.Url);
                    break;
            }

            return new ValueTask<IHandleResult>(new HandleResult(true, true));
        }
    }
}