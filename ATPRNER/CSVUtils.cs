using System.IO;
using System.Text;
using System.Xml;

namespace ATPRNER
{
	public class CSVUtils
	{
		static bool foundWi;

		// BIG TODO
		public static string EntitiesToCsv(string entitiesXml)
		{
			foundWi = false;

			var sb = new StringBuilder();

			XmlReader reader = XmlReader.Create(new StringReader(entitiesXml));

			while (reader.Read())
				CreateEntry(ref reader, ref sb);
			reader.Close();

			return sb.ToString();
		}

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
