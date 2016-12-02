using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

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
		/// <returns>The duplicates.</returns>
		/// <param name="csv">Csv.</param>
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
	}
}
