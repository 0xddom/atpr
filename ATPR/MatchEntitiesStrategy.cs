using System;
using System.IO;
using ATPR.Utils;
using ATPRNER;

namespace ATPR
{
	/// <summary>
	/// Strategy class that generates the matches between textentities and dictionary entities.
	/// </summary>
	public class MatchEntitiesStrategy : AbstractExecStrategy
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ATPR.MatchEntitiesStrategy"/> class.
		/// </summary>
		public MatchEntitiesStrategy()
		{
		}

		/// <summary>
		/// Generates the match between textentities and dictionary entities.
		/// </summary>
		/// <param name="options">Options.</param>
		public override void Run(Options options)
		{
			if (options.Verbose)
				Console.Error.WriteLine("Option 3.");

			if (options.Dictionary == null)
			{
				Console.Error.WriteLine("Dictionary required. Exiting...");
				return;
			}
			TextWriter output;
			if (string.IsNullOrEmpty(options.Output)) output = new StreamWriter(Console.OpenStandardOutput());
			else output = new StreamWriter(options.Output);

			foreach (string dic in FilesUtils.GetFiles(options.Dictionary))
				DictionaryMatcher.MatchEntitiesInFiles(options.InputFile, dic, output, options.Separator);
			

			output.Close();
		}
	}
}
