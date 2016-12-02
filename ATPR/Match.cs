using System;
using System.Collections.Generic;
using System.IO;
using ATPR.Utils;

namespace ATPR
{
	/// <summary>
	/// Stores a match with the text of the source file
	/// </summary>
	public class Match
	{
		public string Text { get; private set; }
		public List<string[]> Items { get; private set; }

		public Match(string filePath, List<string[]> items)
		{
			Text = FilesUtils.FileToText(filePath);
			if (Text == null)
				throw new ArgumentException();
			Items = items;
		}
	}
}
