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
	public class ParseStrategy : AbstractExecStrategy
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ATPR.ParseStrategy"/> class.
		/// </summary>
		public ParseStrategy ()
		{
		}

		/// <summary>
		/// Run the specified options.
		/// Run all the logic of the command.
		/// </summary>
		/// <param name="options">Options.</param>
		public override void Run (Options options)
		{
			this.options = options;

			if (options.Verbose)
				Console.Error.WriteLine ("Parse text command");

			if (options.Matchfile == null) {
				Console.Error.WriteLine ("Matcfile required. Exiting...");
				return;
			}
				
			if (options.InputFile == null) {
				Console.Error.WriteLine ("Input file/directory required. Exiting...");
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
			
	}
}

