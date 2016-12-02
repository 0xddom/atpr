using System;
using System.IO;

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

			WriteResult ("TODO");
		}


	}
}

