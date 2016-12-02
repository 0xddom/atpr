using System;
using System.Collections.Generic;
using System.IO;
using ATPR.Utils;

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
			MatchEntities(List<string[]> textEntities, List<string> dictEntities)
		{
			Dictionary<string, MatchedEntity> matches = new Dictionary<string, MatchedEntity>();

			foreach (string[] entity in textEntities) {
				if (dictEntities.Contains(entity[1])) { 
					if (matches.ContainsKey (entity[1])) {
						matches[entity [1]].IncrementMatch();
					} else {
						var matchedEntity = new MatchedEntity (entity[1],entity[0]);
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
		public static void MatchEntitiesInFiles(string inputPath, string dicPath, TextWriter output, char sep)
		{
			string[] files = FilesUtils.GetFiles(inputPath);
			foreach (string file in files)
			{
				string xml = NER.GenerateEntitiesToString(file);
				string csv = CSVUtils.EntitiesToCsv(xml, sep);

				List<String[]> dicTable = CSVUtils.TabulateCSV(new StreamReader(dicPath), sep);
				List<String[]> fileTable = CSVUtils.TabulateCSV(new StringReader(csv), sep);
				List<String> entitiesTable = GetEntitiesFromDic (dicTable);

				var matchs = MatchEntities(fileTable, entitiesTable);

				GenerateMatchedEntriesCSV(file, dicPath, matchs, output, sep);
			}
		}

		/// <summary>
		/// Gets the entities from dictionary 
		/// (entities are the second col of the csv).
		/// </summary>
		/// <returns>The entities from dic.</returns>
		private static List<String> GetEntitiesFromDic(List<String[]> dicTable)
		{		
			List<String> entitiesTable = new List<String> ();
			foreach (string[] item in dicTable) {
				entitiesTable.Add (item [1]);
			}

			return entitiesTable;
				
		}

		/// <summary>
		/// Generates a CSV with the provided dictionary and writes it to the stream
		/// </summary>
		/// <param name="origFile">Path of the file where the match was found.</param>
		/// <param name="dicFile">The dictionary used to match.</param>
		/// <param name="entries">The found matchs.</param>
		/// <param name="output">Output stream.</param>
		public static void GenerateMatchedEntriesCSV(string origFile, string dicFile, Dictionary<string, MatchedEntity> entries, TextWriter output, char sep)
		{
			foreach (var entry in entries)
			{
				var entryObj = entry.Value;
				output.WriteLine("{0}{4}{1}{4}{2}{4}{5}{4}{3}", origFile, entryObj.EntityName, entryObj.MatchNumber, dicFile, sep, entryObj.Type);
			}
		}
	}
}

