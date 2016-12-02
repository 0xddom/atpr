using System;
using System.IO;
using ATPRNER;

namespace ATPR
{
	/// <summary>
	/// Strategy class that generates the matches between textentities and dictionary entities.
	/// </summary>
	public class MatchEntitiesStrategy : ExecStrategy
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
		public void Run(Options options)
		{
			if (options.Verbose)
				Console.WriteLine("Option 3.");

			if (options.Dictionary == null)
			{
				Console.WriteLine("Dictionary required. Exiting...");
				return;
			}
			TextWriter output;
			if (string.IsNullOrEmpty(options.Output)) output = new StreamWriter(Console.OpenStandardOutput());
			else output = new StreamWriter(options.Output);

			foreach(string dic in FilesUtils.GetFiles(options.Dictionary)) 
				DictionaryMatcher.MatchEntitiesInFiles(options.InputFile, dic, output, options.Separator);

			output.Close();
		}
	}
}
