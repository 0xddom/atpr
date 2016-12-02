using System;
using ATPRNER;

namespace ATPR
{
	/// <summary>
	/// sStrategy class  that check generate entities command arguments and execute generate entities.
	/// </summary>
	public class GenerateEntitiesStrategy : AbstractExecStrategy
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ATPR.GenerateEntitiesStrategy"/> class.
		/// </summary>
		public GenerateEntitiesStrategy()
		{
		}

		/// <summary>
		/// Generates an entities XML from NER output.
		/// </summary>
		/// <param name="options">Options.</param>
		public override void Run(Options options)
		{
			if (options.Verbose)
				Console.Error.WriteLine("Option 1.");

			WriteResult(NER.GenerateEntitiesToString(options.InputFile));
		}
	}
}
