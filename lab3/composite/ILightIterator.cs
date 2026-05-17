namespace Composite
{
    public interface ILightIterator
    {
        bool HasNext();
        LightNode Next();
    }
}