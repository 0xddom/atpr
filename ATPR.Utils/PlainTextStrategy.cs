using System;
using Toxy;

namespace ATPR.Utils
{
	public class PlainTextStrategy : IFileToTextStrategy
	{
		public PlainTextStrategy()
		{
		}

		public string ExtractText(string filePath, string extension)
		{
			ParserContext c = new ParserContext(filePath);
			ITextParser parser = ParserFactory.CreateText(c);
			string text = parser.Parse();

			foreach (var t in text)
			{
				if (char.IsControl(t) && t != '\n' && t != '\t' && t != '\r')
				{
					Console.Error.WriteLine("Found control character: {0} {1}", (int)t, t);
					return null;
				}
			}
			return text;
		}

		public bool IsSupportedExtension(string extension)
		{
			return extension.Equals("txt");
		}
	}
}
