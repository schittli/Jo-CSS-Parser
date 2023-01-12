

# Jo-CSS-Parser

Complete Css Parser Writen in C#


# Updates

## 230112, by Tom

### Renamed: GetPropertie to GetProperty:
	
		Old
			public Property GetPropertie(string Tag, CssProperty Property)
		New
			public Property GetProperty(string Tag, CssProperty Property)
			
		Old
			public Property GetPropertie(Tag tag, CssProperty Property)
		New
			public Property GetProperty(Tag tag, CssProperty Property)
		

### CssParser: Parser for CSS RuleSet


Jo-CSS-Parser has a big issue:
When calling the following code, it may happen that Regex.Matches() takes forever (or *very*) long to find a result:


```
string pattern = @"(?<selector>(?:(?:[^,{]+),?)*?)\{(?:(?<name>[^}:]+):?(?<value>[^};]+);?)*?\}";

List<string> b = new List<string>();

foreach (Match m in Regex.Matches(input, pattern)) {
		b.Add(m.Value);
}
```


That's why I split this function:

1. Find all CSS RuleSets
2. Parse all found CSS RuleSets


Just to be sure we use the same naming: This is a CSS RuleSet:

```
h1 {
		color: green;
}
/* Selector */
p:first-child{ 
		 
		/* Declaration-block */
		background-color: green;
		color: white;
		font-size: 15px;
		border-radius: 50px        
		;
		text-transform: uppercase                
		;
		font-weight: bold;
}

body {
		text-align: center;
}
```				


