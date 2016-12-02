using System;
using CommandLine;
using CommandLine.Text;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using Org.BouncyCastle.Asn1.Misc;
using ATPRNER;
using System.Globalization;
using System.Threading;

namespace ATPR
{
	class MainClass
	{
		/// <summary>
		/// Argument parse options.
		/// </summary>
		class Options
		{
			[Option('p', "path", Required = true,
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

			[HelpOption]
			public string GetUsage()
			{
				return HelpText.AutoBuild(this,
					(HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
			}
		}

		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name="args">The command-line arguments.</param>
<<<<<<< HEAD
		static void Main(string[] args)
		{
=======
		static void Main(string[] args) {

			CultureInfo ci = new CultureInfo("en-US");
			Thread.CurrentThread.CurrentCulture = ci;
			Thread.CurrentThread.CurrentUICulture = ci;
		
>>>>>>> 755bb784c66dc833bc435db3892dc8e45e34f71d
			var options = new Options();
			if (Parser.Default.ParseArguments(args, options))
			{
				// Values are available here
				if (options.Verbose)
				{
					Console.Write("Running with options: ");
					for (int i = 0; i < args.Length; i++)
					{
						Console.Write(" {0} ", args[i]);
					}
					Console.WriteLine("");
				}

				int choose = options.Choose;

				switch (choose)
				{
					case 1: //Option 1, gets only entities
						if (options.Verbose)
							Console.WriteLine("Option 1.");

						if (String.IsNullOrEmpty(options.Output))
						{
							NER.GenerateEntities(options.InputFile);
						}
						else {
							NER.GenerateEntities(options.InputFile, options.Output);
						}

						break;
					case 2: //Option 2, generates dictionary
						if (options.Verbose) Console.WriteLine("Option 2.");

						string xml = NER.GenerateEntitiesToString(options.InputFile);
						string csv = CSVUtils.EntitiesToCsv(xml);

						System.Console.WriteLine("[DEBUG]");
						System.Console.WriteLine(csv);
						if (string.IsNullOrEmpty(options.Output))
						{
							
						}
						break;
					case 3: //Option 3, gets entities that match with a dictionary
						if (options.Verbose)
							Console.WriteLine("Option 3.");
						if (options.Dictionary == null)
						{
							Console.WriteLine("Dictionary required. Exiting...");
							return;
						}

						break;
					default:
						Console.WriteLine("Option not recognized. Exiting...");
						break;
				}
			}
		}
	}
}
