using System;
using System.IO;
using ATPRNER;

namespace ATPR
{
	/// <summary>
	/// Strategy class that generates the dictionary from the NER result.
	/// </summary>
	public class GenerateDictionaryStrategy : ExecStrategy
	{
		public GenerateDictionaryStrategy()
		{
		}

		Options options;

		/// <summary>
		/// Generates the dictionary of entities found.
		/// </summary>
		/// <param name="options">Options.</param>
		public void Run(Options options)
		{
			this.options = options;

			if (options.Verbose)
				Console.WriteLine("Dictionary generation command");

			string xml = NER.GenerateEntitiesToString(options.InputFile);
			string csv = CSVUtils.RemoveDuplicates(CSVUtils.EntitiesToCsv(xml, options.Separator));

			WriteResult(csv);
		}

		/// <summary>
		/// Writes the result to the output stream.
		/// </summary>
		/// <param name="result">Result.</param>
		void WriteResult(string result)
		{
			if (string.IsNullOrEmpty(options.Output)) Console.Write(result);
			else File.WriteAllText(options.Output, result);
		}
	}
}
