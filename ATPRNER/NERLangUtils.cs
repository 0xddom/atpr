using System;
using ATPR.Utils;


namespace ATPRNER
{
	/// <summary>
	/// Lang utils.
	/// Methods for common Lang Files tasks.
	/// </summary>
	public static class NERLangUtils
	{
		public static bool CheckLangFiles(String language)
		{
			return FilesUtils.ExistsModels (StanfordEnv.GetStanfordHome () +
			StanfordEnv.CLASIFIERS + @"/" + language
			+ ".ancora.distsim.s512.crf.ser.gz");
		}
	}
}

