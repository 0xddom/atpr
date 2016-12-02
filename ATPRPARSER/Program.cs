using System;
using Toxy;
using edu.stanford.nlp.parser.lexparser;
using java.io;
using edu.stanford.nlp.process;
using edu.stanford.nlp.trees;
using com.sun.tools.corba.se.logutil;

namespace ATPRParser
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
		/// Parse the specified sentence.
		/// </summary>
		/// <param name="sentence">Sentence to parse</param>
		public static void Parse(string sentence)
		{
			//Load spanish models.
			var jarRoot = GetStanfordHome();
			var modelsDirectory = jarRoot + Consts.MODELS;
			var lexparserDirectory = modelsDirectory + Consts.LEXPARSER;
			var lp = LexicalizedParser.loadModel(lexparserDirectory);

			//Parser sentence.
			var tokenizerFactory = PTBTokenizer.factory(new CoreLabelTokenFactory(), "");
			var sent2Reader = new StringReader(sentence);
			var rawWords2 = tokenizerFactory.getTokenizer(sent2Reader).tokenize();
			sent2Reader.close();
			var tree2 = lp.apply(rawWords2);

			//Print LISP like format tree.
			var tp = new TreePrint("penn");
			tp.printTree(tree2);

		}

		/// <summary>
		/// Gets the home for the stanford jars.
		/// </summary>
		/// <returns>The stanford home path</returns>
		static string GetStanfordHome()
		{
			return Environment.GetEnvironmentVariable("STANFORD_HOME") ?? Consts.DEFAULT_STANFORD_NLP;
		}
	}
}
