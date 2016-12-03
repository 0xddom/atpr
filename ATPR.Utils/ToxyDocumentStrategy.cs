using System;
using System.Collections.Generic;
using System.IO;
using Toxy;

namespace ATPR.Utils
{
	public class ToxyDocumentStrategy : IFileToTextStrategy
	{
		public ToxyDocumentStrategy()
		{
		}

		public string ExtractText(string filePath, string extension)
		{
			ParserContext c = new ParserContext(filePath);

			try
			{
				IDocumentParser parser = ParserFactory.CreateDocument(c);
				ToxyDocument result = parser.Parse();
				return result.ToString();
			}
			catch (InvalidDataException)
			{
				Console.Error.WriteLine($"'{filePath}' is supported but don't have the required extension.");
				var newFilePath = $"{filePath}.{extension}";
				Console.Error.WriteLine($"Creating a copy in '{newFilePath}' and using that to read.");
				File.Copy(filePath, newFilePath);
				return ExtractText(newFilePath, extension);
			}
			catch (Exception e)
			{
				Console.Error.WriteLine("{0} Exception caught error with {1}.", e, filePath);
				return null;
			}
		}

		public bool IsSupportedExtension(string extension)
		{
			return new List<string>(new string[] { "doc", "docx", "pdf", "rtf", "html" }).Contains(extension);
		}
	}
}
