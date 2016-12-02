using System;
using System.IO;

namespace ATPR
{
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

			if (options.Matchfile == null) {
				Console.WriteLine ("Matcfile required. Exiting...");
				return;
			}
				
			if (options.InputFile == null) {
				Console.WriteLine ("Input file/directory required. Exiting...");
				return;
			}
				
			//TODO: Parse text

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
				File.WriteAllText (options.Output, result);
		}
	}
}

