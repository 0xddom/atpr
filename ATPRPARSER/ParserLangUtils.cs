using System;
using ATPR.Utils;

namespace ATPRPARSER
{
	public static class ParserLangUtils
	{
		public static bool CheckLangFiles(String language)
		{
			return FilesUtils.ExistsModels (StanfordEnv.PARSER_MODELS + @"/lexparser/"
				+language+ @"PCFG.ser.gz");
		}
	}
}

