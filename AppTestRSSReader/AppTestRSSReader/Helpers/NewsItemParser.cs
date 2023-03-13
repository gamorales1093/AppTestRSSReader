using AppTestRSSReader.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace AppTestRSSReader.Helpers
{
   public class NewsItemParser
    {
        public List<NewsData> ParseNews(string response)
        {
            if (response == null)
            {
                return null;
            }

            XDocument doc = XDocument.Parse(response);

            foreach (var item in doc.Descendants("item"))
            {
                NewsData news = new NewsData();
                news.link = item.Element("link").Value.ToString();
                news.pubdate = item.Element("pubDate").Value.ToString();
                news.guid = item.Element("guid").Value.ToString();
                news.imageURL = "";        // by default, there is no image
                news.title = HtmlToPlainText(item.Element("title").Value.ToString()); 
                string str = item.Element("description").Value.ToString();

                if (str.Contains("<img"))
                {

                    var desc = str;
                    var i = desc.IndexOf("src=");
                    if (i != -1)
                    {
                        var index2 = desc.IndexOf(".jpg", i);
                        if (index2 != -1 && index2 > i)
                        {
                            // TODO : this is dangerous, because the length of desc has to be chacked before doing that.
                            // 
                            var img = desc.Substring(i + 10, index2 - i - 6);
                            news.imageURL = "https" + img;
                            //feed.ImageUrl = img;

                        }
                    }
                }

                if (str.Contains("Rendre compatible"))
                {

                    Debug.WriteLine("ok");

                    var text0 = HtmlToPlainText(str);
                }


                // convert html to plain text
                var text = HtmlToPlainText(str);
                // keep a short part of the description string
                var max = 200;
                if (text.Length < max)
                {
                    max = text.Length;
                }
                news.description = text.Substring(0, max) + " ...";


                Singleton.news.Add(news);
            }

            return Singleton.news;
        }

        private static string HtmlToPlainText(string html)
        {
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;
            //Decode html specific characters
            text = System.Net.WebUtility.HtmlDecode(text);

            // olivier added : convert \n<p>... into <p>...
            text = tagWhiteSpaceRegex.Replace(text, "<");


            //Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");
            //Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            //Strip formatting
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;
        }

    }
}
