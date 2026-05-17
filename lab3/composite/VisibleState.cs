namespace Composite
{
    public class VisibleState : ILightState
    {
        public string Render(LightElementNode element)
        {
            string classes = element.CssClasses.Count > 0 ? $" class=\"{string.Join(" ", element.CssClasses)}\"" : "";
            if (element.ClosureType == "single")
            {
                return $"<{element.TagName}{classes}/>";
            }
            return $"<{element.TagName}{classes}>{element.InnerHTML}</{element.TagName}>";
        }
    }
}