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

		public Match GetNext()
		{
			List<string[]> entities = new List<string[]>();

			string filePath = matches[count][0];
			while (HasNext() && matches[count][0].Equals(filePath))
				entities.Add(matches[count++]);

			return new Match(filePath, entities);
		}

		public bool HasNext()
		{
			return count < matches.Count;
		}
	}
}
