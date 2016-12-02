using System;
using ATPR.Utils;
using ATPRNER;

namespace ATPR
{
	/// <summary>
	/// Strategy class that generates the dictionary from the NER result.
	/// </summary>
	public class GenerateDictionaryStrategy : AbstractExecStrategy
	{
		public GenerateDictionaryStrategy()
		{
		}

		/// <summary>
		/// Generates the dictionary of entities found.
		/// </summary>
		/// <param name="options">Options.</param>
		public override void Run(Options options)
		{
			this.options = options;

			if (options.Verbose)
				Console.Error.WriteLine("Dictionary generation command");

			string xml = NER.GenerateEntitiesToString(options.InputFile);
			string csv = CSVUtils.RemoveDuplicates(CSVUtils.EntitiesToCsv(xml, options.Separator));

			WriteResult(csv);
		}
	}
}
