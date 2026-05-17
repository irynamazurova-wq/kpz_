using System.Collections.Generic;
using System.Text;

namespace Composite
{
    public class LightElementNode : LightNode
    {
        public string TagName { get; set; }
        public string DisplayType { get; set; }
        public string ClosureType { get; set; }
        public List<string> CssClasses { get; set; } = new List<string>();
        public List<LightNode> Children { get; set; } = new List<LightNode>();

        public int ChildrenCount => Children.Count;

        public LightElementNode(string tagName, string displayType, string closureType)
        {
            TagName = tagName;
            DisplayType = displayType;
            ClosureType = closureType;
        }

        public override string InnerHTML
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var child in Children)
                {
                    sb.Append(child.OuterHTML);
                }
                return sb.ToString();
            }
        }

        public override string OuterHTML
        {
            get
            {
                string classes = CssClasses.Count > 0 ? $" class=\"{string.Join(" ", CssClasses)}\"" : "";
                if (ClosureType == "single")
                {
                    return $"<{TagName}{classes}/>";
                }
                return $"<{TagName}{classes}>{InnerHTML}</{TagName}>";
            }
        }
    }
}