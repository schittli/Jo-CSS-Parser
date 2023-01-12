using JoCssParser.Parser;


namespace Jo_CSS_Parser_UnitTests.Parser {

	[TestClass]
	public class UnitTest_Parse_CSS {

		[TestMethod]
		public void Test_Parse_CSS() {
      
			// Der Test-String
			string testStr = @"Hallo
/* 1 - This is multi-line
comment */
p1 {
  color: red; /* Hallo */
}
/**** 
* 2- multi-line comment 
*/
p2 {
  color: red; /* Hallo */
}";

			// Die erwartetn CSS Rule-Sets
			List<string> expectedCssRuleSets = new List<string>() {
				{
					@"Hallo
p1 {
  color: red; 
}"
				}
				, {
					@"p2 {
  color: red; 
}"
				}
			};

			// Parsen
			var cssRuleSets = CSSRuleSetParser.ParseCSSRuleSets(testStr);

			for (var index = 0; index < cssRuleSets.Count; index++) {
				var cssRuleSet = cssRuleSets[index];
				// ClipboardService.SetText(cssRuleSet);
				var expectedCssRuleSet = expectedCssRuleSets[index];
				Assert.IsTrue(expectedCssRuleSet.Equals(cssRuleSet));
			}
		}

	}

}

