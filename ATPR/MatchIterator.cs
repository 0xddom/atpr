using System;
using System.Collections.Generic;

namespace ATPR
{
	public class MatchIterator : IMatchIterator
	{
		List<string[]> matches;
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
			while (matches[count][0].Equals(filePath))
				entities.Add(matches[count++]);

			return new Match(filePath, entities);
		}

		public bool HasNext()
		{
			return count < matches.Count;
		}
	}
}
