using System;
using Toxy;
using edu.stanford.nlp.parser.lexparser;
using java.io;
using edu.stanford.nlp.process;
using edu.stanford.nlp.trees;

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
			Parse("Por mi hija pequeña que te pego un tiro en toda la pierna.");
		}

		/// <summary>
		/// Parse the specified sentence.
		/// </summary>
		/// <param name="Sentence">Sentence to parse</param>
		public static void Parse(string Sentence)
		{

			//Load spanish models.
			var jarRoot = @"~/Hackaton/Standford/models/stanford-spanish";
			var modelsDirectory = jarRoot+@"/edu/stanford/nlp/models";
			var lp = LexicalizedParser.loadModel(modelsDirectory + @"/lexparser/spanishPCFG.ser.gz");

			//Parser sentence.
			var sent2 = Sentence;
			var tokenizerFactory = PTBTokenizer.factory(new CoreLabelTokenFactory(), "");
			var sent2Reader = new StringReader(sent2);
			var rawWords2 = tokenizerFactory.getTokenizer(sent2Reader).tokenize();
			sent2Reader.close();
			var tree2 = lp.apply(rawWords2);

			//Print LISP like format tree.
			var tp = new TreePrint("penn");
			tp.printTree(tree2);

		}
	}
}
