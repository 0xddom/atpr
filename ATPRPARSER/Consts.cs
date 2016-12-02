using System;
namespace ATPRParser
{
	public class Consts
	{
		/// <summary>
		/// The default stanford nlp directory.
		/// </summary>
		public static string DEFAULT_STANFORD_NLP = Environment.GetEnvironmentVariable("HOME") + "/Hackaton/Stanford/";

		/// <summary>
		/// The LEXPARSER directory.
		/// </summary>
		public static string LEXPARSER = @"/lexparser/spanishPCFG.ser.gz";

		/// <summary>
		/// The spanish models.
		/// </summary>
		public static string MODELS = @"/edu/stanford/nlp/models";

	}
}
