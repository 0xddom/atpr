using System;
using System.Collections.Generic;
using ATPR.Utils;
using edu.stanford.nlp.ie.crf;

namespace ATPRNER
{
	/// <summary>
	/// A multiton that stores Classifiers instances based on language
	/// </summary>
	public class CRFClassifiers
	{
		static Dictionary<string, CRFClassifier> classifiers;

		static string classifiersDirectory = StanfordEnv.GetStanfordHome() + StanfordEnv.CLASIFIERS;

		static CRFClassifiers()
		{
			classifiers = new Dictionary<string, CRFClassifier>();
		}

		public static CRFClassifier GetClassifierByLang(string lang)
		{
			if (!classifiers.ContainsKey(lang))
			{
				classifiers.Add(lang,
					CRFClassifier.getClassifierNoExceptions(classifiersDirectory + StanfordEnv.GetNerLanguageFiles(lang)));
			}
			return classifiers[lang];
		}
	}
}
