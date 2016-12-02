using System;
using ATPRNER;

namespace ATPR
{
	public class GenerateEntitiesStrategy : ExecStrategy
	{
		public GenerateEntitiesStrategy()
		{
		}

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
