using System;
using Toxy;
using edu.stanford.nlp.parser.lexparser;
using edu.stanford.nlp.process;
using edu.stanford.nlp.trees;
using com.sun.tools.corba.se.logutil;
using System.Collections.Generic;
using System.IO;
using ATPR.Utils;
using TagLib.Riff;

namespace ATPRParser
{
	public class Parser
	{

		public static void Parse (string text, string entity, int number)
		{

			string[] splittedText = SplitText (text);
			List<string> entityLines = GetEntitiesLines (splittedText,entity);

			foreach (var line in entityLines) {
				
				//Load spanish models.
				var jarRoot = GetStanfordHome ();
				var modelsDirectory = jarRoot + Consts.MODELS;
				var lexparserDirectory = modelsDirectory + Consts.LEXPARSER;
				var lp = LexicalizedParser.loadModel (lexparserDirectory);

				//Parser sentence.
				var tokenizerFactory = PTBTokenizer.factory (new CoreLabelTokenFactory (), "");
				var sent2Reader = new java.io.StringReader (line);
				var rawWords2 = tokenizerFactory.getTokenizer (sent2Reader).tokenize ();
				sent2Reader.close ();
				var tree2 = lp.apply (rawWords2);

				//Print LISP like format tree.
				var tp = new TreePrint ("penn");
				tp.printTree (tree2);
			}
		}

		/// <summary>
		/// Gets the home for the stanford jars.
		/// </summary>
		/// <returns>The stanford home path</returns>
		static string GetStanfordHome ()
		{
			return Environment.GetEnvironmentVariable ("STANFORD_HOME") ?? Consts.DEFAULT_STANFORD_NLP;
		}

		/// <summary>
		/// Gets the matchs of the matchingFile.
		/// </summary>
		/// <returns>The matching.</returns>
		/// <param name="matchingFilePath">Matching file path.</param>
		public static List<String[]> GetMatching (string matchingFilePath)
		{
			String matchs = FilesUtils.FileToText (matchingFilePath);
			StringReader reader = new StringReader (matchs);
			return CSVUtils.TabulateCSV (reader, ';');
		}

		/// <summary>
		/// Splits the text.
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		public static string[] SplitText (string text)
		{
			char[] delimiters = new char[] {
				'{', '}', '(', ')', '[', ']', '>', '<', '-', '_', '=', '+',
				'|', '\\', ':', ';', ' ', '\'', ',', '.', '/', '?', '~', '!',
				'@', '#', '$', '%', '^', '&', '*', ' ', '\r', '\n', '\t'
			};

			return text.Split (delimiters, StringSplitOptions.RemoveEmptyEntries);
		}

		/// <summary>
		/// Gets the entities lines.
		/// </summary>
		/// <returns>The entities lines.</returns>
		/// <param name="text">Text.</param>
		public static List<string> GetEntitiesLines (string[] text, string entity)
		{
			List<string> entitiesLines = new List<string>();

			foreach (string line in text) {
				if (line.Contains (entity))
					entitiesLines.Add (line);
			}
			return entitiesLines;
		}
	}
}
