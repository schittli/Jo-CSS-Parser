using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


namespace JoCssParser.Parser {

	/// <summary>
	/// Extracts each CSS Ruleset
	/// </summary>
	public class CSSRuleSetParser {

		/// <summary>
		///
		/// Removes all C-Style Comments in cssStr
		/// and then extract each CSS Ruleset
		/// 
		/// These are 2 CSS Ruleset:
		/// 
		///   h1 {
		///      color: green;
		///   }
		///
		///   body {
		///      text-align: center;
		///   }
		/// 
		/// </summary>
		/// <param name=""></param>
		/// <returns></returns>
		public static List<string> ParseCSSRuleSets(string cssStr) {
			// Remove all C-Style Comments
			var noCommentsStr = Remove_CStyleComments(cssStr);

			// Extract all CSS RuleSets into a List<String>
			return _ParseCSSRuleSets(noCommentsStr);
		}


      /// <summary>
      /// Liest den cssStr und extrahiert jedes CSS RuleSet  
      /// </summary>
      /// <param name="cssStr"></param>
      /// <returns></returns>
      public static List<string> _ParseCSSRuleSets(string cssStr) {
         var _myRegexOptions = RegexOptions.IgnoreCase
                               | RegexOptions.Multiline
                               | RegexOptions.ExplicitCapture
                               | RegexOptions.CultureInvariant
                               | RegexOptions.IgnorePatternWhitespace
                               | RegexOptions.Compiled;

         // Source: RegEx for parsing CSS
         // https://gist.github.com/hekt/1145069
         // Die " sind mit "" Escaped
         var sRgxCssRuleSet = @"
            (?<CSSRuleset>
              (?<Selectors>
                (?:[^{}""']|'[^']*'|""[^""]*"")+
              )
              (?<DeclarationBlock>
                (?:[\s\n]|/\*(?:(?!\*/)[\s\S])*\*/)*
                {
                  (?:
                    [^{}""']|/\*(?:(?!\*/)[\s\S])*\*/|'[^']*'|""[^""]*""
                  )*
                }
              )
            )
            ";

         var             oRgxCssRuleSet    = new Regex(sRgxCssRuleSet, _myRegexOptions);
         MatchCollection matchCollection   = null;
         int             LastEndofMatchPos = 0;

         List<string> CssRulesets = new List<string>();

         do {
            // Alle Comments suchen
            matchCollection = oRgxCssRuleSet.Matches(cssStr, LastEndofMatchPos);

            // Jeden gefundenen Comment aus dem ursprünglichen Str entfernen 
            foreach (Match match in matchCollection) {
               var cssRuleSet          = match.Groups["CSSRuleset"].Value;
               var cssSelectors        = match.Groups["Selectors"].Value;
               var cssDeclarationBlock = match.Groups["DeclarationBlock"].Value;

               // Remove html Tags if there are any
               cssSelectors = Remove_HtmlTags(cssSelectors);
               // Build the new CSS Rule Set
               var newRuleSet = cssSelectors + cssDeclarationBlock;

               CssRulesets.Add(newRuleSet.ØRemoveEmptyLines());
               LastEndofMatchPos = match.Index + match.Length;
            }
         }
         while (matchCollection.Count > 0);

         return CssRulesets;
      }


		/// <summary>
		/// Remove all Html Tags <Tag>
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string Remove_HtmlTags(string str) {
			var clean = Regex.Replace
			(str
			 , @"<[^>]*>"
			 , "");
			return clean.ØRemoveEmptyLines();
		}


		/// <summary>
		/// Deletes all C Style Comments in str:
		/// 
		///   /* … */
		/// 
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string Remove_CStyleComments(string str) {
			var _myRegexOptions = RegexOptions.IgnoreCase
			                      | RegexOptions.Multiline
			                      | RegexOptions.ExplicitCapture
			                      | RegexOptions.CultureInvariant
			                      | RegexOptions.Compiled;

			// Diese Kommentare: /* .. */
			// !KH9 https://blog.ostermiller.org/finding-comments-in-source-code-using-regular-expressions/
			var sRgxComment       = @"(?<Comment>/\*(?:[^*]|[\r\n]|(?:\*+(?:[^*/]|[\r\n])))*\*+/)";
			var oRgxCStyleComment = new Regex(sRgxComment, _myRegexOptions);

			int             LastEndofMatchPos = 0;
			StringBuilder   res               = new StringBuilder();
			MatchCollection matchCollection   = null;

			do {
				// Alle Comments suchen
				matchCollection = oRgxCStyleComment.Matches(str, LastEndofMatchPos);

				// Jeden gefundenen Comment aus dem ursprünglichen Str entfernen 
				foreach (Match match in matchCollection) {
					var    grpVal = match.Groups["Comment"].Value;
					string subStr = str.ØSubstring(LastEndofMatchPos, match.Index - LastEndofMatchPos);
					LastEndofMatchPos = match.Index + match.Length;
					res.Append(subStr);
				}
			} while (matchCollection.Count > 0);

			// Vom letzten Kommentar bis zum Ende von str die Zeichen übernehmen
			string lastSubStr = str.ØSubstring(LastEndofMatchPos, str.Length - LastEndofMatchPos);
			res.Append(lastSubStr);
			// Debug.WriteLine(res.ToString());

			return res.ToString();
		}

	}

}
