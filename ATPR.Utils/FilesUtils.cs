using System;
using System.IO;
using MimeDetective;
using System.Linq;
using Toxy;

namespace ATPR.Utils
{
	/// <summary>
	/// Files utils.
	/// Methods for common File tasks.
	/// </summary>
	public static class FilesUtils
	{
		public static string FileToText(string filePath)
		{
			FileType fType;

			using (Stream s = new FileStream(filePath, FileMode.Open))
			{
				fType = s.GetFileType();
			}
			var availableStrategies = from type in System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
									  where typeof(IFileToTextStrategy).IsAssignableFrom(type) && !type.IsInterface
									  select type;

			foreach (var sType in availableStrategies)
			{
				var strategy = Activator.CreateInstance(sType) as IFileToTextStrategy;
				if (strategy.IsSupportedExtension(fType.Extension))
					return strategy.ExtractText(filePath, fType.Extension);
				
			}

			return null; // Unsupported file
		}

		/// <summary>
		/// Returns an array of files to be parsed
		/// </summary>
		/// <returns>A file paths array</returns>
		/// <param name="inputPath">The input path.</param>
		public static string[] GetFiles(string inputPath)
		{
			if (Directory.Exists(inputPath))
				return Directory.GetFiles(inputPath);
			if (File.Exists(inputPath))
				return new string[] { inputPath };
			throw new DirectoryNotFoundException(inputPath);

		}

		/// <summary>
		/// Check if language model files exists.
		/// </summary>
		/// <returns><c>true</c>, if models was existsed, <c>false</c> otherwise.</returns>
		/// <param name="path">Path.</param>
		public static bool ExistsModels(string path)
		{
			return File.Exists (path);
		}
	}
}
