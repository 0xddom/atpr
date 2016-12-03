using System;
namespace ATPR.Utils
{
	/// <summary>
	/// Stanford NLP library configuration constants.
	/// </summary>
	public class StanfordEnv
	{
		/// <summary>
		/// The default stanford nlp directory.
		/// </summary>
		public static string DEFAULT_STANFORD_NLP = Environment.GetEnvironmentVariable("HOME") + "/Hackaton/Stanford/";

		public static string PARSER_HOME = GetStanfordHome() + "models/stanford-spanish";

		public static string PARSER_MODELS = PARSER_HOME + "/edu/stanford/nlp/models";

		/// <summary>
		/// The clasifiers directory.
		/// </summary>
		public static string CLASIFIERS = @"/stanford-ner-2015-12-09/classifiers/";


		/// <summary>
		/// Gets the home for the stanford jars.
		/// </summary>
		/// <returns>The stanford home path</returns>
		public static string GetStanfordHome()
		{
			return Environment.GetEnvironmentVariable("STANFORD_HOME") ?? DEFAULT_STANFORD_NLP;
		}


		/// <summary>
		/// Gets NER language file name;
		/// </summary>
		/// <returns>The ner language files.</returns>
		/// <param name="language">Language.</param>
		public static string GetNerLanguageFiles(string language)
		{
			return @"/" + language + ".ancora.distsim.s512.crf.ser.gz";
		}

		/// <summary>
		/// Gets the PARSER language files.
		/// </summary>
		/// <returns>The parser language files.</returns>
		/// <param name="language">Language.</param>
		public static string GetParserLanguageFiles(string language)
		{
			return @"/lexparser/"+language+@"PCFG.ser.gz";
		}


	}
}
