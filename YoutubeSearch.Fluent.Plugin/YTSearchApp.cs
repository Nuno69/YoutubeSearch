using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blast.API.Processes;
using Blast.Core.Interfaces;
using Blast.Core.Objects;
using Blast.Core.Results;
using TextCopy;
using YoutubeExplode;

namespace YoutubeSearch.Fluent.Plugin
{
	public class YTSearchApp : ISearchApplication
	{
		private readonly SearchApplicationInfo searchApplicationInfo;
		private YoutubeClient client;
		public YTSearchApp()
		{
			client = new();
			searchApplicationInfo = new("YoutubeSearch", "Allows to search Youtube without leaving Fluent Search", YTSearchResult.SupportedOperations)
			{
				SearchTagName = "Youtube",
				MinimumSearchLength = 3,
				SearchTagOnly = true,
				IsProcessSearchEnabled = false,
				IsProcessSearchOffline = false,
				ApplicationIconGlyph = null,
				SearchAllTime = ApplicationSearchTime.Moderate,
				DefaultSearchTags = new List<SearchTag>
				{
					new()
					{
						Name = "Youtube",
						IconGlyph = null
					}
				}
			};
		} public SearchApplicationInfo GetApplicationInfo()
		{
			return searchApplicationInfo;
		}
		public async IAsyncEnumerable<ISearchResult> SearchAsync(SearchRequest searchRequest, CancellationToken cancellationToken)
		{
			string text = searchRequest.SearchedText;
			string tag = searchRequest.SearchedTag;
			if (String.IsNullOrWhiteSpace(text) || String.IsNullOrWhiteSpace(tag) || !tag.Equals("Youtube", StringComparison.OrdinalIgnoreCase))
			{
				yield break;
			}
			else
			{
				await foreach (var video in client.Search.GetVideosAsync(text))
				{
					yield return new YTSearchResult(video, text);
				}
			}
		}
		public async ValueTask<IHandleResult> HandleSearchResult(ISearchResult searchResult)
		{
			if (searchResult is not YTSearchResult yTSearchResult)
			{
				return new HandleResult(true, true);
			}
			switch (searchResult.SelectedOperation)
			{
				case YTOpenInBrowserOperation openInBrowserOperation:
					ProcessUtils.GetManagerInstance().StartNewProcess(yTSearchResult.Video.Url);
					break;
				case YTCopyURLOperation copyURLOperation:
					Clipboard.SetText(yTSearchResult.Video.Url);
					break;
				default:
					break;
			}
		return 	new HandleResult(true, true);
		}
	}
}
