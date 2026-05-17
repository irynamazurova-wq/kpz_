#nullable disable
using System;

namespace Bridge
{
    public interface IRenderer
    {
        void RenderCircle();
        void RenderSquare();
        void RenderTriangle();
    }

    public class VectorRenderer : IRenderer
    {
        public void RenderCircle() => Console.WriteLine("Drawing Circle as vector lines");
        public void RenderSquare() => Console.WriteLine("Drawing Square as vector lines");
        public void RenderTriangle() => Console.WriteLine("Drawing Triangle as vector lines");
    }

    public class RasterRenderer : IRenderer
    {
        public void RenderCircle() => Console.WriteLine("Drawing Circle as pixels");
        public void RenderSquare() => Console.WriteLine("Drawing Square as pixels");
        public void RenderTriangle() => Console.WriteLine("Drawing Triangle as pixels");
    }

    public abstract class Shape
    {
        protected IRenderer _renderer;

        protected Shape(IRenderer renderer)
        {
            _renderer = renderer;
        }

        public abstract void Draw();
    }

    public class Circle : Shape
    {
        public Circle(IRenderer renderer) : base(renderer) {}
        public override void Draw() => _renderer.RenderCircle();
    }

    public class Square : Shape
    {
        public Square(IRenderer renderer) : base(renderer) {}
        public override void Draw() => _renderer.RenderSquare();
    }

    public class Triangle : Shape
    {
        public Triangle(IRenderer renderer) : base(renderer) {}
        public override void Draw() => _renderer.RenderTriangle();
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            IRenderer vector = new VectorRenderer();
            IRenderer raster = new RasterRenderer();

            Shape vectorCircle = new Circle(vector);
            Shape rasterSquare = new Square(raster);
            Shape rasterTriangle = new Triangle(raster);

            vectorCircle.Draw();
            rasterSquare.Draw();
            rasterTriangle.Draw();

            Console.ReadKey();
        }
    }
}