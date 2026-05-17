using System;
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

        private ILightState _currentState;

        public int ChildrenCount => Children.Count;

        public LightElementNode(string tagName, string displayType, string closureType)
        {
            TagName = tagName;
            DisplayType = displayType;
            ClosureType = closureType;
            
            _currentState = new VisibleState();
            Initialize(); 
        }

        public void SetState(ILightState state)
        {
            _currentState = state;
            Console.WriteLine($"[Система]: Стан тегу <{TagName}> змінено на {state.GetType().Name}.");
        }

        public void ApplyClass(string className)
        {
            CssClasses.Add(className);
            OnClassListApplied(className);
        }

        protected virtual void OnClassListApplied(string className)
        {
            Console.WriteLine($"[Хук OnClassListApplied]: До тегу <{TagName}> успішно застосовано клас \"{className}\".");
        }

        protected override void OnCreated()
        {
            Console.WriteLine($"[Хук OnCreated]: Створено новий HTML-тег: <{TagName}>");
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

        public override string OuterHTML => _currentState.Render(this);

        public override void Accept(ILightVisitor visitor)
        {
            visitor.Visit(this); 
            foreach (var child in Children)
            {
                child.Accept(visitor); 
            }
        }
    }
}