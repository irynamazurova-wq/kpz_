using System.Collections.Generic;

namespace Composite
{
    public class DepthFirstIterator : ILightIterator
    {
        private readonly Stack<LightNode> _stack = new Stack<LightNode>();

        public DepthFirstIterator(LightNode root)
        {
            if (root != null) _stack.Push(root);
        }

        public bool HasNext() => _stack.Count > 0;

        public LightNode Next()
        {
            if (!HasNext()) return null;
            LightNode current = _stack.Pop();

            // Якщо поточний вузол є елементом (тегом) з дітьми, штовхаємо дітей у стек у зворотному порядку
            if (current is LightElementNode element)
            {
                for (int i = element.Children.Count - 1; i >= 0; i--)
                {
                    _stack.Push(element.Children[i]);
                }
            }
            return current;
        }
    }
}