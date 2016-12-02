using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Collections.Generic;
using System;
using TagLib.Riff;

namespace ATPRNER
{
	public class CSVUtils
	{
		static bool foundWi;

		/// <summary>
		/// From a NER xml entities returns a CSV with the interesting entities
		/// </summary>
		/// <returns>The entities in CSV.</returns>
		/// <param name="entitiesXml">Entities xml.</param>
		public static string EntitiesToCsv(string entitiesXml)
		{
			foundWi = false;

			var sb = new StringBuilder();

			XmlReader reader = XmlReader.Create(new StringReader(entitiesXml));

			while (reader.Read())
				CreateEntry(ref reader, ref sb);
			reader.Close();

			return RemoveDuplicates(sb.ToString());
		}

		/// <summary>
		/// Removes the duplicates from a CSV.
		/// </summary>
		/// <returns>The CSV without duplicated entries.</returns>
		/// <param name="csv">A CSV file.</param>
		public static string RemoveDuplicates(string csv)
		{
			string[] csvEntries = csv.Split('\n');
			HashSet<string> withoutDuplicates = new HashSet<string>();
			foreach (string entry in csvEntries)
			{
				if (!withoutDuplicates.Contains(entry))
					withoutDuplicates.Add(entry);
			}

			var e = withoutDuplicates.GetEnumerator();
			var sb = new StringBuilder();
			do
			{
				sb.AppendFormat("{0}\n", e.Current);
			} while (e.MoveNext());

			return sb.ToString();
		}

		/// <summary>
		/// Returns the CSV as a tabular structure
		/// </summary>
		/// <returns>The table</returns>
		/// <param name="reader">A reader with CSV entries.</param>
		/// <param name="sep">The CSV separator</param>
		public static List<string[]> TabulateCSV(TextReader reader, char sep)
		{
			List<string[]> table = new List<string[]>();
			string line;
			while ((line = reader.ReadLine()) != null)
			{
				table.Add(line.Split(sep));	
			}

			return table;
		}

		/// <summary>
		/// From the XML reader write in the string builder the CSV entry for tha found entity.
		/// </summary>
		/// <param name="reader">The xml.</param>
		/// <param name="sb">Output string builder.</param>
		static void CreateEntry(ref XmlReader reader, ref StringBuilder sb)
		{
			bool shouldBreak = false;
			string entity = null;

			while (!shouldBreak && reader.Read())
				switch (reader.NodeType)
				{
					case XmlNodeType.Element:
						foundWi = reader.Name.Equals("wi");
						if (foundWi && !reader.GetAttribute("entity").Equals("O"))
						{
							entity = reader.GetAttribute("entity");
						}
						else {
							shouldBreak = true;
						}
						break;
					case XmlNodeType.Text:
						if (foundWi && entity != null && reader.Value.Length > 3)
						{
							sb.AppendFormat("{0};{1}\n", entity, reader.Value);
							foundWi = false;
							shouldBreak = true;
							entity = null;
						}
						break;
					case XmlNodeType.EndEntity:
						shouldBreak = true;
						break;
				}
		}

		/// <summary>
		/// Gets the entities from dictionary (CSV File).
		/// </summary>
		/// <returns>The entities from dic.</returns>
		/// <param name="dictionaryPath">Dictionary path.</param>
		public static List<String> GetEntitiesFromDic(string dictionaryPath)
		{
			
			List<String> entities = new List<String> ();
			StreamReader reader = new StreamReader(System.IO.File.OpenRead(@"C:\test.csv"));
			while (!reader.EndOfStream)
			{
				var line = reader.ReadLine();
				var values = line.Split(';');
				entities.Add (values [1]);		
			}

			return entities;
		}
	}
}
