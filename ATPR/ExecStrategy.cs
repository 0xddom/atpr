using System;
namespace ATPR
{
	/// <summary>
	/// Base interface for strategy classes
	/// </summary>
	public interface ExecStrategy
	{
		void Run();
		bool UsesNER();
		bool UsesParser();
	}
}
