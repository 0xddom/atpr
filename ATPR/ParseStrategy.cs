using System;
using System.IO;
using System.Collections.Generic;
using ATPRParser;
using ATPR.Utils;
using DocumentFormat.OpenXml.Drawing;
using TagLib.Riff;
using System.Collections.ObjectModel;

namespace ATPR
{
	/// <summary>
	/// Strategy class that generate the syntax analisis of the documents
	/// using the entities of the matching process result.
	/// </summary>
	public class ParseStrategy : ExecStrategy
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ATPR.ParseStrategy"/> class.
		/// </summary>
		public ParseStrategy ()
		{
		}

		/// <summary>
		/// The command line argument options.
		/// </summary>
		Options options;

		/// <summary>
		/// Run the specified options.
		/// Run all the logic of the command.
		/// </summary>
		/// <param name="options">Options.</param>
		public void Run (Options options)
		{
			this.options = options;

			if (options.Verbose)
				Console.WriteLine ("Parse text command");
			
			if (options.InputFile == null) {
				Console.WriteLine ("Input file/directory required. Exiting...");
				return;
			}
				
			//TODO: Parse text

			List<String[]> matchs = ATPRParser.Parser.GetMatching (options.InputFile);

			Dictionary<string,List<String[]>> orgMatches = new Dictionary<string,List<String[]>> ();

			foreach (String[] item in matchs) {
				if (orgMatches.ContainsKey (item [0])) {
					orgMatches [item [0]].Add (item);
				} else {
					List<String[]> itemList = new List<String []>();
					itemList.Add (item);
					orgMatches.Add (item [0], itemList);
				}
			}
				
			foreach (var entry in orgMatches) {
				foreach (String[] subItem in entry.Value) {
					String text = FilesUtils.FileToText (subItem [1]);
					Parser.Parse (text, subItem [2], int.Parse(subItem [3]));
				}
			}

			WriteResult ("TODO");
		}

		/// <summary>
		/// Writes the run method result.
		/// </summary>
		/// <param name="result">Result.</param>
		void WriteResult (string result)
		{
			if (string.IsNullOrEmpty (options.Output))
				Console.Write (result);
			else
				System.IO.File.WriteAllText (options.Output, result);
		}
	}
}

