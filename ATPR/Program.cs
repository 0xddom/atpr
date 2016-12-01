using System;
using CommandLine;
using CommandLine.Text;

namespace ATPR
{
	class MainClass
	{
		// Define a class to receive parsed values
		class Options {
			[Option('p', "path", Required = true,
				HelpText = "Directory where the tool will find the files.")]
			public string InputFile { get; set; }

			[Option('v', "verbose", DefaultValue = false,
				HelpText = "Prints all messages to standard output.")]
			public bool Verbose { get; set; }

			[Option('o', "output", Required = false,
				HelpText = "Output directory where the tool will save the results.")]
			public bool Output { get; set; }
				
			[Option('c', "choose", Required = true,
				HelpText = "Selected option for running the tool.")]
			public int Choose { get; set; }

			[Option('d', "dictionary", Required = false,
				HelpText = "Path to a dictionary.")]
			public string Dictionary { get; set; }

			[HelpOption]
			public string GetUsage() {
				return HelpText.AutoBuild(this,
					(HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
			}
		}

		// Consume them
		static void Main(string[] args) {
			var options = new Options();
			if (CommandLine.Parser.Default.ParseArguments(args, options)) {
				// Values are available here
				if (options.Verbose) Console.WriteLine("Running with options {0}", options.ToString());
				int choose = options.Choose;
				switch (choose) {
				case 1:	//Option 1, gets only entities
					break;
				case 2:	//Option 2, generates dictionary
					break;
				case 3:	//Option 3, gets entities that match with a dictionary
					break;
				default:
					//ERROR
					break;
				}


			}
		}
	}
}
