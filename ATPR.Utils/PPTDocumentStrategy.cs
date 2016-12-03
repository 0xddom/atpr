using System;
using Toxy;
using System.Text;
using System.Collections.Generic;

namespace ATPR.Utils
{
	public class PPTDocumentStrategy : IFileToTextStrategy
	{
		
		public PPTDocumentStrategy ()
		{
	
		}

		public string ExtractText(string filePath, string extension)
		{
			StringBuilder textResult = new StringBuilder("");

			ParserContext context = new ParserContext(filePath);
			ISlideshowParser parser = ParserFactory.CreateSlideshow(context);
			ToxySlideshow slideshow = parser.Parse();

			foreach (ToxySlide slide in slideshow.Slides) {
				foreach (String text in slide.Texts) {
					textResult.Append ('.' + text);
				}
			}

			return textResult.ToString();
		}

		public bool IsSupportedExtension(string extension)
		{
			return extension.Equals("ppt");
		}

	}
}

