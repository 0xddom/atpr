using System;
using ATPRNER;

namespace ATPR
{
	/// <summary>
	/// This strategy class generates the NER xml and saves to a stream
	/// </summary>
	public class GenerateEntitiesStrategy : ExecStrategy
	{
		public GenerateEntitiesStrategy()
		{
		}

		/// <summary>
		/// Generates an entities XML from NER output.
		/// </summary>
		/// <param name="options">Options.</param>
		public void Run(Options options)
		{
			if (options.Verbose)
				Console.WriteLine("Option 1.");

			if (string.IsNullOrEmpty(options.Output))
			{
				NER.GenerateEntities(options.InputFile);
			}
			else {
				NER.GenerateEntities(options.InputFile, options.Output);
			}
		}
	}
}
