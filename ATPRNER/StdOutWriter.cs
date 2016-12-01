using System;
using System.IO;
using System.Text;

namespace ATPRNER
{
	public class StdOutWriter : TextWriter
	{
		public StdOutWriter()
		{
		}

		public override Encoding Encoding
		{
			get
			{
				return Encoding.UTF8;
			}
		}
	}
}
