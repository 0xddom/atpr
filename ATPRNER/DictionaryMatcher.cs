using System;
using System.Collections.Generic;
using System.IO;

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
		public static Dictionary<string, MatchedEntity> 
			MatchEntities (List<string[]> textEntities,List<string[]> dictEntities)
		{
			Dictionary<string, MatchedEntity> matches = new Dictionary<string, MatchedEntity> ();

			foreach (string[] entity in textEntities) {
				if (dictEntities.Contains(entity)) {
					if (matches.ContainsKey (entity[1])) {
						matches[entity[1]].IncrementMatch();
					} else {
						var matchedEntity = new MatchedEntity (entity[1]);
						matches.Add (entity[1], matchedEntity);
					}
				}
			}
			return matches;
		}

		/// <summary>
		/// Writes to the output stream a csv with the match results against the dictionary
		/// </summary>
		/// <param name="inputPath">Files path.</param>
		/// <param name="dicPath">Dictionary path.</param>
		/// <param name="output">Output stream.</param>
		public static void MatchEntitiesInFiles(string inputPath, string dicPath, TextWriter output)
		{
			string[] files = FilesUtils.GetFiles(inputPath);
			foreach (string file in files)
			{
				string xml = NER.GenerateEntitiesToString(file);
				string csv = CSVUtils.EntitiesToCsv(xml);

				var dicTable = CSVUtils.TabulateCSV(new StreamReader(dicPath), ';');
				var fileTable = CSVUtils.TabulateCSV(new StringReader(csv), ';');

				var matchs = MatchEntities(fileTable, dicTable);

				CSVUtils.GenerateMatchedEntriesCSV(file, dicPath, matchs, output);
			}
		}
	}
}

