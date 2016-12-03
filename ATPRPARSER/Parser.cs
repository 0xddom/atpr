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
		
		/// <summary>
		/// Parse the document searching for sentences where the entity found.
		/// Returns a csv line with the file, the entity the sentence and the sintax analisis of the sentences
		/// </summary>
		/// <param name="text">Document text</param>
		/// <param name="entity">Entity.</param>
		/// <param name="origFile">Original file.</param>
		public static List<string[]> Parse (string text, string entity, string origFile,string language)
		{
			var results = new List<string[]>();
			//Load spanish models.
			var modelsDirectory = StanfordEnv.PARSER_MODELS;
			var lexparserDirectory = modelsDirectory + StanfordEnv.GetParserLanguageFiles(language);
			var lp = LexicalizedParser.loadModel(lexparserDirectory);

			string[] splittedText = SplitText (text);
			List<string> entityLines = GetEntitiesLines (splittedText,entity);

			foreach (var line in entityLines) {
				//Parser sentence.
				var tokenizerFactory = PTBTokenizer.factory (new CoreLabelTokenFactory (), "");
				var sent2Reader = new java.io.StringReader (line);
				var rawWords2 = tokenizerFactory.getTokenizer (sent2Reader).tokenize ();
				sent2Reader.close ();
				var tree2 = lp.apply (rawWords2);

				results.Add(new string[] { origFile, entity, line, tree2.ToString() });
			}

			return results;
		}

		/// <summary>
		/// Gets the matchs of the matchingFile.
		/// </summary>
		/// <returns>The matching.</returns>
		/// <param name="matchingFilePath">Matching file path.</param>
		public static List<String[]> GetMatching (string matchingFilePath, char sep)
		{
			return CSVUtils.TabulateCSV (new StreamReader(matchingFilePath), sep);
		}

		/// <summary>
		/// Splits the text.
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		public static string[] SplitText (string text)
		{
			char[] delimiters = {
				'{', '}', '(', ')', '[', ']', '>', '<', '-', '_', '=', '+',
				'|', '\\', ':', ';', '\'', ',', '.', '/', '?', '~', '!',
				'@', '#', '$', '%', '^', '&', '*', '\r', '\n', '\t'
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

			foreach (string line in text) 
				if (line.Contains (entity))
					entitiesLines.Add (line);
			
			return entitiesLines;
		}
	}
}
