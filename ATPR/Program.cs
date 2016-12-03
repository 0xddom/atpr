using System;
using CommandLine;
using System.Globalization;
using System.Threading;
using ATPRNER;
using ATPRPARSER;

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

				if (!CheckLanguage(options.Language))
				{
					Console.Error.WriteLine(
						@"The language identifier contains dots and/or slashes. Those characters are not permited in the language identifier.
If your language identifier contains those symbols in its name, please rename it.
Aborting...");
					Environment.Exit(1);
				}

				ExecStrategy strategy = null;

				switch (options.Choose)
				{
					case "entities": //Option 1, gets only entities
						strategy = new GenerateEntitiesStrategy(options);
						break;
					case "dictionary": //Option 2, generates dictionary
						strategy = new GenerateDictionaryStrategy(options);
						break;
					case "match": //Option 3, gets entities that match with a dictionary
						strategy = new MatchEntitiesStrategy(options);
						break;
					case "parser": //Option 4, parse text and show the syntax analis
						strategy = new ParseStrategy(options);
						break;
					default:
						Console.Error.WriteLine("Option not recognized. Exiting...");
						break;
				}
				if (strategy != null)
				{
					if ((strategy.UsesNER() && !NERLangUtils.CheckLangFiles(options.Language))
					    || (strategy.UsesParser() && !ParserLangUtils.CheckLangFiles(options.Language)))
					{
						Console.Error.WriteLine("Language files not found\nExiting...");
						Environment.Exit(1);
					}
					strategy.Run();
				}
			}
		}

		static bool CheckLanguage(string lang)
		{
			bool acc = true;
			foreach (var c in new string[]{
				".", "/", "\\"})
			{
				acc = acc && !lang.Contains(c);
			}

			return acc;
		}

		/// <summary>
		/// Prints the arguments.
		/// </summary>
		/// <param name="args">Arguments.</param>
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
