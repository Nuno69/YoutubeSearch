using Blast.API.Search;
using YoutubeExplode;
using YoutubeExplode.Common;
using System.Collections.Generic;

namespace YoutubeSearch.Fluent.Plugin
{
	/// <summary>
	/// Provides some common logic for querying Youtube data.
	/// </summary>
	internal class Utility
	{
		private YoutubeClient client;
		internal async IAsyncEnumerable<YTSearchResult> VideoQuery(string q)
		{
			await foreach (var video in client.Search.GetVideosAsync(q))
			{
				yield return new(video, q);
			}
		}
		public Utility()
		{
			client = new();
		}
	}
	}
