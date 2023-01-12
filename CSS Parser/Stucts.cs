using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoCssParser
{
    /// <summary>
    /// Property Info (Property Name and Property Value_.
    /// </summary>
    public struct Property
    {
        public string PropertyName;
        public string PropertyValue;
    };

    /// <summary>
    /// Saves a CSS Selector
    /// and its CSS Declaration Block
    /// </summary>
    public struct TagWithCSS
    {
        // In fact, this is the CSS Selector of a CSS Rule Set  
        public string TagName;
        // all properties defines the CSS Declaration Block
        public List<Property> Properties;
    };
}
