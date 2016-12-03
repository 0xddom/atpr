using System;
namespace ATPR.Utils
{
	public interface IFileToTextStrategy
	{
		bool IsSupportedExtension(string extension);
		string ExtractText(string filePath, string extension);
	}
}
