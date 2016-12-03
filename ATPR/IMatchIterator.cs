using System;
namespace ATPR
{
	/// <summary>
	/// Interface for iterating matches.
	/// </summary>
	public interface IMatchIterator
	{
		bool HasNext();
		Match GetNext();
	}
}
