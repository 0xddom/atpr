using System;
using ATPRNER;

namespace ATPR
{
	/// <summary>
	/// Generate entities strategy class.
	/// Check generate entities command arguments and execute generate entities.
	/// </summary>
	public class GenerateEntitiesStrategy : ExecStrategy
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ATPR.GenerateEntitiesStrategy"/> class.
		/// </summary>
		public GenerateEntitiesStrategy()
		{
		}
		/// <summary>
		/// Run the specified options.
		/// Run all the logic of the command.
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
