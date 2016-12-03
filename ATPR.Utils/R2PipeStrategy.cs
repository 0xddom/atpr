using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using r2pipe;

namespace ATPR.Utils
{
	public class R2PipeStrategy : IFileToTextStrategy
	{
		public R2PipeStrategy()
		{
		}

		public string ExtractText(string filePath, string extension)
		{

			string r2Path = null;

			if (FilesUtils.ExistsOnPath("radare2.exe"))
				r2Path = FilesUtils.GetFullPath("radare2.exe");
			else if (FilesUtils.ExistsOnPath("radare2"))
				r2Path = FilesUtils.GetFullPath("radare2");
			else {
				PrintR2NotFound();
				return null; // No radare
			}

			PrintR2Warning();

			Console.Error.WriteLine($"Using radare from {r2Path}");

			var sb = new StringBuilder();

			using (IR2Pipe pipe = new R2Pipe(filePath, r2Path))
			{
				var queue = new QueuedR2Pipe(pipe);
				queue.Enqueue(new R2Command("?V", (string result) =>
											Console.Error.WriteLine($"Using radare version: {result}")));
				queue.Enqueue(new R2Command("e bin.rawstr=1", (no_use) => { /* Do nothing */ }));
				queue.Enqueue(new R2Command("izj", (string jsonResult) =>
				{
					List<R2ZString> strings = JsonConvert.DeserializeObject<List<R2ZString>>(jsonResult);

					foreach (var s in strings)
					{
						sb.AppendLine(Base64Decode(s.String));
					}
				}));
				queue.ExecuteCommands();
			}

			return sb.ToString();
		}

		void PrintR2Warning()
		{
			Console.Error.WriteLine(@"-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
			
The r2pipe can write undesired output to stdout. 
Is advisable to use the -o option when using the r2pipe backend.

-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
");
		}

		public bool IsSupportedExtension(string extension)
		{
			return false;
		}

		void PrintR2NotFound()
		{
			Console.Error.WriteLine(@"radare2 was not found in your computer.
You can install it and use it as a fallback for not detected files.");
		}

		string Base64Decode(string base64EncodedData)
		{
			var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
			return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
		}
	}
}
