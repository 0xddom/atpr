using System;
using System.IO;
using com.sun.xml.@internal.xsom.impl.parser;
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
		public static void GenerateDict(string inputPath, string outputPath)
		{
			StreamWriter outputFile = new StreamWriter(outputPath);
			GenerateDict(inputPath, outputFile);
			outputFile.Close();
		}

		static string GetStanfordHome()
		{
			return Environment.GetEnvironmentVariable("STANFORD_HOME") ?? Consts.DEFAULT_STANFORD_NLP;
		}

		/// <summary>
		/// Generates the dict from the documents in inputPath
		/// and send output to stdout
		/// </summary>
		/// <param name="inputPath">Input path.</param>
		public static void GenerateDict(string inputPath)
		{
			var stdout = new StreamWriter(Console.OpenStandardOutput());
			GenerateDict(inputPath, stdout);
			stdout.Close();
		}

		/// <summary>
		/// Returns an array of files to be parsed
		/// </summary>
		/// <returns>A file paths array</returns>
		/// <param name="inputPath">The input path.</param>
		static String[] GetFiles(string inputPath)
		{
			if (Directory.Exists(inputPath))
			{
				return Directory.GetFiles(inputPath);
			}
			else {
				if (File.Exists(inputPath))
				{
					return new string[] { inputPath };
				}
				else
				{
					throw new DirectoryNotFoundException(inputPath);
				}
			}
		}

		/// <summary>
		/// Global method for dictionary generation
		/// </summary>
		/// <param name="inputPath">The input path</param>
		/// <param name="output">Output stream</param>
		static void GenerateDict(string inputPath, StreamWriter output)
		{
			output.WriteLine("<wis>");

			var jarRoot = GetStanfordHome();
			var classifiersDirecrory = jarRoot + Consts.CLASIFIERS;
			string[] fileEntries = GetFiles(inputPath);

			foreach (var document in fileEntries)
			{
				Toxy.ParserContext c = new Toxy.ParserContext(document);
				IDocumentParser parser = ParserFactory.CreateDocument(c);
				ToxyDocument result = parser.Parse();

				string text = result.ToString();

				var classifier = CRFClassifier.getClassifierNoExceptions(classifiersDirecrory + Consts.MODELS);

				output.WriteLine(classifier.classifyToString(text, "xml", true));
			}
			output.WriteLine("</wis>");
		}
	}
}
