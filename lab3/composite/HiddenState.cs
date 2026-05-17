namespace Composite
{
    public class HiddenState : ILightState
    {
        public string Render(LightElementNode element)
        {
            return $"";
        }
    }
}