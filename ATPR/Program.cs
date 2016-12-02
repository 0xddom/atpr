using System;
using CommandLine;
using CommandLine.Text;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using Org.BouncyCastle.Asn1.Misc;
using ATPRNER;
using System.Globalization;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Wordprocessing;
using TagLib.Riff;

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

				string choose = options.Choose;
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
					default:
						Console.WriteLine("Option not recognized. Exiting...");
						break;
				}
				if (strategy != null)
					strategy.Run(options);
			}
		}
	}
}
