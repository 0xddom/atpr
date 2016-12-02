using System;
using System.IO;
using ATPRNER;

namespace ATPR
{
	public class GenerateDictionaryStrategy : ExecStrategy
	{
		public GenerateDictionaryStrategy()
		{
		}

		Options options;

		public void Run(Options options)
		{
			this.options = options;

			if (options.Verbose)
				Console.WriteLine("Dictionary generation command");

			string xml = NER.GenerateEntitiesToString(options.InputFile);
			string csv = CSVUtils.EntitiesToCsv(xml, options.Separator);

			WriteResult(csv);
		}

		void WriteResult(string result)
		{
			if (string.IsNullOrEmpty(options.Output)) Console.Write(result);
			else File.WriteAllText(options.Output, result);
		}
	}
}
