﻿using System;
using System.IO;
using Toxy;

namespace ATPR.Utils
{
	public static class FilesUtils
	{
		/// <summary>
		/// Extrat the text from a file, using Toxy for binary files.
		/// </summary>
		/// <returns>The plain text of the document</returns>
		/// <param name="filePath">File path.</param>
		public static string FileToText(string filePath)
		{
			if (filePath.EndsWith(".doc", StringComparison.CurrentCulture)
				|| filePath.EndsWith(".docx", StringComparison.CurrentCulture)
				|| filePath.EndsWith(".pdf", StringComparison.CurrentCulture)
				|| filePath.EndsWith(".rtf", StringComparison.CurrentCulture)
				|| filePath.EndsWith(".html", StringComparison.CurrentCulture)
			)
				//Test rtf
			{
				ParserContext c = new ParserContext(filePath);
				//TODO Add suport to ppt and pptx CreateSlideshow
				//Examples https://github.com/tonyqus/toxy/tree/master/Toxy.Test
				try
				{	
					IDocumentParser parser = ParserFactory.CreateDocument(c);
					ToxyDocument result = parser.Parse();
					return result.ToString();
				}
				catch (Exception e)
				{
					Console.Error.WriteLine("{0} Exception caught error with {1}.", e, filePath);
					return null;
				}
			}

			if (filePath.EndsWith(".txt", StringComparison.CurrentCulture)
				|| filePath.EndsWith(".csv", StringComparison.CurrentCulture)
				|| filePath.EndsWith(".html", StringComparison.CurrentCulture))
				try
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
			catch (Exception e) 
			{
				Console.Error.WriteLine("{0} Exception caught error with {1}.", e, filePath);
				return null;
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
				return Directory.GetFiles(inputPath);
			if (File.Exists(inputPath))
				return new string[] { inputPath };
			throw new DirectoryNotFoundException(inputPath);

		}
	}
}
