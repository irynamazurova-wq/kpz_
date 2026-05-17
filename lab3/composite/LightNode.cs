using System;

namespace Composite
{
    public abstract class LightNode
    {
        public abstract string InnerHTML { get; }
        public abstract string OuterHTML { get; }
        public abstract void Accept(ILightVisitor visitor);

        protected virtual void OnCreated() { }
        protected virtual void OnTextRendered() { }

        public void Initialize()
        {
            OnCreated(); 
            Console.WriteLine($"[Система]: Вузол {this.GetType().Name} успішно додано в пам'ять.");
        }
    }
}