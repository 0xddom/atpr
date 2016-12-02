using System;
using CommandLine;
using System.Globalization;
using System.Threading;

namespace ATPR
{
	class MainClass
	{
		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name="args">The command-line arguments.</param>
		static void Main(string[] args)
		{

			CultureInfo ci = new CultureInfo("en-US");
			Thread.CurrentThread.CurrentCulture = ci;
			Thread.CurrentThread.CurrentUICulture = ci;

			var options = new Options();
			if (Parser.Default.ParseArguments(args, options))
			{
				if (options.Verbose) PrintArgs(args);

				ExecStrategy strategy = null;

				switch (options.Choose)
				{
					case "entities": //Option 1, gets only entities
						strategy = new GenerateEntitiesStrategy();
						break;
					case "dictionary": //Option 2, generates dictionary
						strategy = new GenerateDictionaryStrategy();
						break;
					case "match": //Option 3, gets entities that match with a dictionary
						strategy = new MatchEntitiesStrategy();
						break;
					case "parser": //Option 4, parse text and show the syntax analis
						strategy = new ParseStrategy();
						break;
					default:
						Console.Error.WriteLine("Option not recognized. Exiting...");
						break;
				}
				if (strategy != null)
					strategy.Run(options);
			}
		}

		static void PrintArgs(string[] args)
		{
			Console.Error.Write("Running with options: ");
			for (int i = 0; i < args.Length; i++)
			{
				Console.Error.Write(" {0} ", args[i]);
			}
			Console.Error.WriteLine("");
		}
	}
}
