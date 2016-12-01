using System;
using Toxy;
using edu.stanford.nlp.ie.crf;
using System.IO;

namespace ATPRNER
{
	
	class MainClass
	{
		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name="args">The command-line arguments.</param>
		public static void Main (string[] args)
		{
			
		}

		/// <summary>
		/// Generates the dict from the documents in inputPath
		/// and send output to output file in outputPath
		/// </summary>
		/// <param name="inputPath">Input path.</param>
		/// <param name="outputPath">Output path.</param>
		public void GenerateDict(string inputPath,string outputPath)
		{

			System.IO.StreamWriter outputFile = new System.IO.StreamWriter(outputPath);

			var jarRoot = @"~/Hackaton/Standford/";
			var classifiersDirecrory = jarRoot + @"/stanford-ner-2015-12-09/classifiers/";
			string [] fileEntries = Directory.GetFiles(inputPath);
		
			foreach(String document in fileEntries)
			{
				ParserContext c=new ParserContext(@"~/Hackaton/Dics/DicHack.pdf");
				IDocumentParser parser=ParserFactory.CreateDocument(c);
				ToxyDocument result = parser.Parse();

				string text = result.ToString();

				var classifier = CRFClassifier.getClassifierNoExceptions(classifiersDirecrory + 
					@"/spanish.ancora.distsim.s512.crf.ser.gz");

				outputFile.WriteLine (classifier.classifyToString (text, "xml", true));
			}

			outputFile.Close();

		}

		/// <summary>
		/// Generates the dict from the documents in inputPath
		/// and send output to stdout
		/// </summary>
		/// <param name="inputPath">Input path.</param>
		public void GenerateDict(string inputPath)
		{

			var jarRoot = @"~/Hackaton/Standford/";
			var classifiersDirecrory = jarRoot + @"/stanford-ner-2015-12-09/classifiers/";

			string [] fileEntries = Directory.GetFiles(inputPath);


			foreach(string file in fileEntries)
			{
				ParserContext c=new ParserContext(@"~/Hackaton/Dics/DicHack.pdf");
				IDocumentParser parser=ParserFactory.CreateDocument(c);
				ToxyDocument result = parser.Parse();

				string text = result.ToString();

				var classifier = CRFClassifier.getClassifierNoExceptions(classifiersDirecrory + 
					@"/spanish.ancora.distsim.s512.crf.ser.gz");

				Console.WriteLine("{0}\n", classifier.classifyToString(text ,"xml", true));
			}
	
		}
	}
}
