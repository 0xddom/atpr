using System;
using System.IO;
using ATPR.Utils;
using edu.stanford.nlp.ie.crf;
using Toxy;

namespace ATPRNER
{
	/// <summary>
	/// NER:
	/// Extract entities from the text.
	/// </summary>
	public static class NER
	{
		/// <summary>
		/// Generates the dict from the documents in inputPath
		/// and send output to output file in outputPath
		/// </summary>
		/// <param name="inputPath">Input path.</param>
		/// <param name="outputPath">Output path.</param>
		public static void GenerateEntities(string inputPath, string outputPath,string language)
		{
			StreamWriter outputFile = new StreamWriter(outputPath);
			GenerateEntities(inputPath, outputFile,language);
			outputFile.Close();
		}

		/// <summary>
		/// Generates the dict from the documents in inputPath
		/// and send output to stdout
		/// </summary>
		/// <param name="inputPath">Input path.</param>
		public static void GenerateEntities(string inputPath,string language)
		{
			var stdout = new StreamWriter(Console.OpenStandardOutput());
			GenerateEntities(inputPath, stdout,language);
			stdout.Close();
		}

		/// <summary>
		/// Generates the dict from the documents in inputPath
		/// and send output to a string.
		/// </summary>
		/// <returns>The entities xml.</returns>
		/// <param name="inputPath">Input path.</param>
		public static string GenerateEntitiesToString(string inputPath,string language)
		{
			var strWriter = new StringWriter();
			GenerateEntities(inputPath, strWriter,language);
			return strWriter.ToString();
		}

		/// <summary>
		/// Global method for entities generation
		/// </summary>
		/// <param name="inputPath">The input path</param>
		/// <param name="output">Output stream</param>
		static void GenerateEntities(string inputPath, TextWriter output , string language)
		{
			output.WriteLine("<wis>");

			var jarRoot = StanfordEnv.GetStanfordHome();
			var classifiersDirectory = jarRoot + StanfordEnv.CLASIFIERS;
			string[] fileEntries = FilesUtils.GetFiles(inputPath);

			foreach (var document in fileEntries)
			{
				string text = FilesUtils.FileToText(document);
				// XXX: Better a NullObject, but string can't be inherited I think.
				if (text == null)
				{
					var stderr = new StreamWriter(Console.OpenStandardError());
					stderr.WriteLine($"The file '{document}' is not supported");
					stderr.Close();
					continue;
				}

				var classifier = CRFClassifiers.GetClassifierByLang(language); //CRFClassifier.getClassifierNoExceptions(classifiersDirectory + StanfordEnv.GetNerLanguageFiles(language));

				output.WriteLine(classifier.classifyToString(text, "xml", true));
			}
			output.WriteLine("</wis>");
		}
	}
}
