using System;
using System.Collections.Generic;

namespace ATPRNER
{
	public class DictionaryMatcher
	{
		/// <summary>
		/// Matchs the text entities with the dictionary entities.
		/// </summary>
		/// <returns>The entities.</returns>
		/// <param name="textEntities">Text entities.</param>
		/// <param name="dictEntities">Dict entities.</param>
		public static Dictionary<String,MatchedEntity> 
			MatchEntities (List<String[]> textEntities,List<String[]> dictEntities)
		{
			Dictionary<String,MatchedEntity> matches = new Dictionary<String,MatchedEntity> ();

			foreach (String[] entity in textEntities) {
				if (dictEntities.Contains(entity)) {
					if (matches.ContainsKey (entity[1])) {
						matches[entity[1]].IncrementMatch();
					} else {
						MatchedEntity matchedEntity = new MatchedEntity (entity[1]);
						matches.Add (entity[1], matchedEntity);
					}
				}
			}
			return matches;
		}
			
	}
}

