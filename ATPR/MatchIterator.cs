using System;
using System.Collections.Generic;

namespace ATPR
{
	/// <summary>
	/// Iterates over the matched entries
	/// </summary>
	public class MatchIterator : IMatchIterator
	{
		readonly List<string[]> matches;
		int count;

		public MatchIterator(List<string[]> matches)
		{
			this.matches = matches;
			count = 0;
		}

		/// <summary>
		/// Returns the next matches grouped by source file
		/// </summary>
		/// <returns>The next.</returns>
		public Match GetNext()
		{
			List<string[]> entities = new List<string[]>();

			string filePath = matches[count][0];
			while (HasNext() && matches[count][0].Equals(filePath))
				entities.Add(matches[count++]);

			return new Match(filePath, entities);
		}

		/// <summary>
		/// Returns true if there are more matches
		/// </summary>
		/// <returns><c>true</c>, if there are more, <c>false</c> otherwise.</returns>
		public bool HasNext()
		{
			return count < matches.Count;
		}
	}
}
