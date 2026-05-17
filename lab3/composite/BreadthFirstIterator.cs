using System.Collections.Generic;

namespace Composite
{
    public class BreadthFirstIterator : ILightIterator
    {
        private readonly Queue<LightNode> _queue = new Queue<LightNode>();

        public BreadthFirstIterator(LightNode root)
        {
            if (root != null) _queue.Enqueue(root);
        }

        public bool HasNext() => _queue.Count > 0;

        public LightNode Next()
        {
            if (!HasNext()) return null;
            LightNode current = _queue.Dequeue();

            if (current is LightElementNode element)
            {
                foreach (var child in element.Children)
                {
                    _queue.Enqueue(child);
                }
            }
            return current;
        }
    }
}