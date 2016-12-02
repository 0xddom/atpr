using System;
namespace ATPR.Utils
{
	public class Consts
	{
		/// <summary>
		/// The default stanford nlp directory.
		/// </summary>
		public static string DEFAULT_STANFORD_NLP = Environment.GetEnvironmentVariable("HOME") + "/Hackaton/Stanford/";

		/// <summary>
		/// The clasifiers directory.
		/// </summary>
		public static string CLASIFIERS = @"/stanford-ner-2015-12-09/classifiers/";

		/// <summary>
		/// The spanish models.
		/// </summary>
		public static string MODELS = @"/spanish.ancora.distsim.s512.crf.ser.gz";

		/// <summary>
		/// Gets the home for the stanford jars.
		/// </summary>
		/// <returns>The stanford home path</returns>
		public static string GetStanfordHome()
		{
			return Environment.GetEnvironmentVariable("STANFORD_HOME") ?? Consts.DEFAULT_STANFORD_NLP;
		}
	}
}
