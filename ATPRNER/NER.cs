using System;
using System.IO;
using edu.stanford.nlp.ie.crf;
using Toxy;

namespace ATPRNER
{
	public class NER
	{
		/// <summary>
		/// Generates the dict from the documents in inputPath
		/// and send output to output file in outputPath
		/// </summary>
		/// <param name="inputPath">Input path.</param>
		/// <param name="outputPath">Output path.</param>
		public static void GenerateEntities(string inputPath, string outputPath)
		{
			StreamWriter outputFile = new StreamWriter(outputPath);
			GenerateEntities(inputPath, outputFile);
			outputFile.Close();
		}

		/// <summary>
		/// Gets the home for the stanford jars.
		/// </summary>
		/// <returns>The stanford home path</returns>
		static string GetStanfordHome()
		{
			return Environment.GetEnvironmentVariable("STANFORD_HOME") ?? Consts.DEFAULT_STANFORD_NLP;
		}

		/// <summary>
		/// Generates the dict from the documents in inputPath
		/// and send output to stdout
		/// </summary>
		/// <param name="inputPath">Input path.</param>
		public static void GenerateEntities(string inputPath)
		{
			var stdout = new StreamWriter(Console.OpenStandardOutput());
			GenerateEntities(inputPath, stdout);
			stdout.Close();
		}

		/// <summary>
		/// Generates the dict from the documents in inputPath
		/// and send output to a string.
		/// </summary>
		/// <returns>The entities xml.</returns>
		/// <param name="inputPath">Input path.</param>
		public static string GenerateEntitiesToString(string inputPath)
		{
			var strWriter = new StringWriter();
			GenerateEntities(inputPath, strWriter);
			return strWriter.ToString();
		}

		/// <summary>
		/// Returns an array of files to be parsed
		/// </summary>
		/// <returns>A file paths array</returns>
		/// <param name="inputPath">The input path.</param>
		static string[] GetFiles(string inputPath)
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

		/// <summary>
		/// Extrat the text from a file, using Toxy for binary files.
		/// </summary>
		/// <returns>The plain text of the document</returns>
		/// <param name="filePath">File path.</param>
		static string FileToText(string filePath)
		{
			if (filePath.EndsWith(".doc", StringComparison.CurrentCulture)
				|| filePath.EndsWith(".docx", StringComparison.CurrentCulture)
				|| filePath.EndsWith(".pdf", StringComparison.CurrentCulture))
			{
				Toxy.ParserContext c = new Toxy.ParserContext(filePath);
				IDocumentParser parser = ParserFactory.CreateDocument(c);
				ToxyDocument result = parser.Parse();

				return result.ToString();
			}
			if (filePath.EndsWith(".txt", StringComparison.CurrentCulture))
			{
				return File.ReadAllText(filePath);
			}
			return null; // Unsupported file
		}

		/// <summary>
		/// Global method for entities generation
		/// </summary>
		/// <param name="inputPath">The input path</param>
		/// <param name="output">Output stream</param>
		static void GenerateEntities(string inputPath, TextWriter output)
		{
			output.WriteLine("<wis>");

			var jarRoot = GetStanfordHome();
			var classifiersDirectory = jarRoot + Consts.CLASIFIERS;
			string[] fileEntries = GetFiles(inputPath);

			foreach (var document in fileEntries)
			{
				string text = FileToText(document);
				// XXX: Better a NullObject, but string can't be inherited I think.
				if (text == null)
				{
					var stderr = new StreamWriter(Console.OpenStandardError());
					stderr.WriteLine($"The file '{document}' is not supported");
					stderr.Close();
					continue;
				}

				var classifier = CRFClassifier.getClassifierNoExceptions(classifiersDirectory + Consts.MODELS);

				output.WriteLine(classifier.classifyToString(text, "xml", true));
			}
			output.WriteLine("</wis>");
		}
	}
}
