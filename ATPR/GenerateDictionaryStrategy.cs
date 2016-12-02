using System;
using ATPRNER;

namespace ATPR
{
	public class GenerateDictionaryStrategy : ExecStrategy
	{
		public GenerateDictionaryStrategy()
		{
		}

		public void Run(Options options)
		{
			if (options.Verbose)
				Console.WriteLine("Option 2.");

			string xml = NER.GenerateEntitiesToString(options.InputFile);
			string csv = CSVUtils.EntitiesToCsv(xml, options.Separator);

			if (string.IsNullOrEmpty(options.Output))
			{
				Console.WriteLine(csv);
			}
			else {
				System.IO.File.WriteAllText(options.Output, csv);
			}
		}
	}
}
