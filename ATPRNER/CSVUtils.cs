using System.IO;
using System.Xml;

namespace ATPRNER
{
	public class CSVUtils
	{
		// BIG TODO
		public static string EntitiesToCsv(string entitiesXml)
		{
			using (XmlReader reader = XmlReader.Create(new StringReader(entitiesXml)))
			{
				while (reader.Read())
				{
					switch (reader.NodeType)
					{
						case XmlNodeType.Element:
							System.Console.WriteLine(reader.Name);
							break;
					}
				}
			}

			return null;
		}
	}
}
