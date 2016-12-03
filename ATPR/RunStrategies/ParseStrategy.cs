using System;
using System.Collections.Generic;
using ATPR.Utils;
using ATPRParser;
using ATPRPARSER;

namespace ATPR
{
	/// <summary>
	/// Strategy class that generate the syntax analisis of the documents
	/// using the entities of the matching process result.
	/// </summary>
	public class ParseStrategy : AbstractExecStrategy
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ATPR.ParseStrategy"/> class.
		/// </summary>
		public ParseStrategy(Options options) : base(options)
		{
		}

		/// <summary>
		/// Run the specified options.
		/// Run all the logic of the command.
		/// </summary>
		/// <param name="options">Options.</param>
		public override void Run()
		{
			if (options.Verbose)
				Console.Error.WriteLine("Parse text command");

			if (options.InputFile == null)
			{
				Console.Error.WriteLine("Input file/directory required. Exiting...");
				return;
			}
				
			List<string[]> matchs = Parser.GetMatching(options.InputFile, options.Separator);

			IMatchIterator iter = new MatchIterator(matchs);

			var csvEntries = new List<string[]>();

			while (iter.HasNext())
			{
				Match match = iter.GetNext();
				foreach (string[] item in match.Items)
					csvEntries.AddRange(Parser.Parse(match.Text, item[1], match.FilePath,options.Language));
			}

			var CSV_FMT = "{0}{4}{1}{4}{2}{4}{3}";

			WriteResult(CSVUtils.RemoveDuplicates(CSVUtils.BuildCSV(csvEntries, 
			                              (builder, e) => builder.AppendFormat(
				                                                        CSV_FMT, 
				                                                        e[0], 
				                                                        e[1], 
				                                                        e[2], 
				                                                        e[3], 
				                                                        options.Separator))));
		}

		public override bool UsesNER()
		{
			return false;
		}

		public override bool UsesParser()
		{
			return true;
		}
	}
}

