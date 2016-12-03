using System;
namespace ATPR.Utils
{
	public class R2ZString
	{
		public int vaddr { get; set; }
		public int paddr { get; set; }
		public int ordinal { get; set; }
		public int size { get; set; }
		public int length { get; set; }
		public string section { get; set; }
		public string type { get; set; }
		public string String { get;set; }

		public R2ZString()
		{
		}
	}
}
