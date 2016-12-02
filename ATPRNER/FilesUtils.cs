using System;
using System.IO;
using Toxy;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;

namespace ATPRNER
{
	public class FilesUtils
	{
		/// <summary>
		/// Extrat the text from a file, using Toxy for binary files.
		/// </summary>
		/// <returns>The plain text of the document</returns>
		/// <param name="filePath">File path.</param>
		public static string FileToText(string filePath)
		{
			if (filePath.EndsWith (".doc", StringComparison.CurrentCulture)
			    || filePath.EndsWith (".docx", StringComparison.CurrentCulture)
			    || filePath.EndsWith (".pdf", StringComparison.CurrentCulture)) {
				ParserContext c = new ParserContext (filePath);
				IDocumentParser parser = ParserFactory.CreateDocument (c);
				ToxyDocument result = parser.Parse ();

				return result.ToString ();
			}
			if (filePath.EndsWith(".txt", StringComparison.CurrentCulture))
			{
				return File.ReadAllText(filePath);
			}
			return null; // Unsupported file
		}

		/// <summary>
		/// Returns an array of files to be parsed
		/// </summary>
		/// <returns>A file paths array</returns>
		/// <param name="inputPath">The input path.</param>
		public static string[] GetFiles(string inputPath)
		{
			if (Directory.Exists(inputPath))
			{
				return Directory.GetFiles(inputPath);
			}
			if (File.Exists(inputPath))
			{
				return new string[] { inputPath };
			}
			throw new DirectoryNotFoundException(inputPath);

		}
	}
}
