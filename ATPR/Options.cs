using System;
using CommandLine;

namespace ATPR
{
	/// <summary>
	/// Argument parse options.
	/// </summary>
	public class Options
	{
		[Option('i', "input", Required = true,
			HelpText = "File or directory where the tool will get the files.")]
		public string InputFile { get; set; }

		[Option('v', "verbose", DefaultValue = false,
			HelpText = "Prints all messages to standard output.")]
		public bool Verbose { get; set; }

		[Option('o', "output", Required = false,
			HelpText = "Output path where the tool will save the results.")]
		public string Output { get; set; }

		[Option('c', "choose", Required = true,
			HelpText = "Selected option for running the tool.")]
		public int Choose { get; set; }

		[Option('d', "dictionary", Required = false,
			HelpText = "Path to a dictionary.")]
		public string Dictionary { get; set; }

		[Option('S', "separator", DefaultValue = ';',
				HelpText = "The CSV separator for the input and output CSVs. Default ';'")]
		public char Separator { get; set; }

		[HelpOption]
		public string GetUsage()
		{
			return CommandLine.Text.HelpText.AutoBuild(this,
				(CommandLine.Text.HelpText current) => CommandLine.Text.HelpText.DefaultParsingErrorsHandler(this, current));
		}
	}
}
