using Blast.Core.Interfaces;
using Blast.Core.Objects;
using Blast.Core.Results;

namespace YoutubeSearch.Fluent.Plugin
{
	/// <summary>
	/// provides  the Youtube search result to Fluent Search, allowing to then operate on it further.
	/// </summary>
	public sealed class YTSearchResult : SearchResultBase
	{
		/// <summary>
		/// The name of the Youtube video. Shown in Fluent Search as a result.
		/// </summary>
		public string? VideoName { get; set; }
		/// <summary>
		/// The video description, hopefully shown as preview.
		/// </summary>
		public string? videoDescription { get; set; }
		/// <summary>
		/// Used for operating the result. Basically all operations require it more or less.
		/// </summary>
		public string? VideoURL { get; set; }
		public YTSearchResult(string resultName, string searchedText, string resultType, double score, IList<ISearchOperation> supportedOperations, ICollection<SearchTag> tags, ProcessInfo processInfo = null) : base(resultName, searchedText, resultType, score, supportedOperations, tags, processInfo)
		{
		}

		protected override void OnSelectedSearchResultChanged()
		{

		}
	}
}