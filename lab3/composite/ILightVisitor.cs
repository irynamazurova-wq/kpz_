namespace Composite
{
    public interface ILightVisitor
    {
        void Visit(LightElementNode element);
        void Visit(LightTextNode textNode);
    }
}