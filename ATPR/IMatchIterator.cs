using System;
namespace ATPR
{
	public interface IMatchIterator
	{
		bool HasNext();
		Match GetNext();
	}
}
