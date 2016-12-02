using System;
using System.IO;
using ATPRNER;

namespace ATPR
{
	public class MatchEntitiesStrategy : ExecStrategy
	{
		public MatchEntitiesStrategy()
		{
		}

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
