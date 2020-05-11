

// Author: FreeDOOM#4231 on Discord


using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            const int numberOfFigures = 16;

            List<Shape> shapes = new List<Shape>();

            Console.WriteLine("All shapes:\n");

            //Punkt 1
            Random rng = new Random();
            Vector2 _center;
            float _side;
            for (int i = 0, a = 0; i < numberOfFigures; i++)
            {
                a = rng.Next(3);
                _center = new Vector2(rng.Next(-127, 128), rng.Next(-127, 128));
                _side = (float)(rng.NextDouble() * 32.0);

                switch(rng.Next(3))
                {
                    case 0:
                        shapes.Add(new Square(_center, _side));
                        break;
                    case 1:
                        shapes.Add(new Hexagon(_center, _side));
                        break;
                    case 2:
                        shapes.Add(new Circle(_center, _side));
                        break;
                    default:
                        shapes.Add(new Shape(_center));
                        break;
                }

                Console.WriteLine("  " + shapes[i]);
            }

            
            // Punkt 2
            float summaryArea = 0f;
            float summaryCircumference = 0f;
            foreach (Shape shape in shapes)
            {
                summaryArea += shape.Area();
                summaryCircumference += shape.Circumference();
            }

            Console.WriteLine("\nShapes statistics:");
            Console.WriteLine($"  Summary area:{summaryArea}");
            Console.WriteLine($"  Summary circumference:{summaryCircumference}");


            // Punkt 3
            foreach (Shape shape in shapes)
                shape.Move((float)rng.NextDouble(), (float)rng.NextDouble());

            summaryArea = 0f;
            summaryCircumference = 0f;
            foreach (Shape shape in shapes)
            {
                summaryArea += shape.Area();
                summaryCircumference += shape.Circumference();
            }

            Console.WriteLine("\nShapes statistics after Moving by random vectors:");
            Console.WriteLine($"  Summary area:{summaryArea}");
            Console.WriteLine($"  Summary circumference:{summaryCircumference}");


            // Punkt 4
            foreach (Shape shape in shapes)
                shape.Scale(2);

            summaryArea = 0f;
            summaryCircumference = 0f;
            foreach (Shape shape in shapes)
            {
                summaryArea += shape.Area();
                summaryCircumference += shape.Circumference();
            }

            Console.WriteLine("\nShapes statistics after Scaling by factor of 2:");
            Console.WriteLine($"  Summary area:{summaryArea}");
            Console.WriteLine($"  Summary circumference:{summaryCircumference}");
        }
    }


    class Circle : Shape
    {
        float radius;

        public Circle(Vector2 center, float radius)
            : base(center)
        {
            this.radius = radius;
        }

        public override void Move(float x, float y)
        { center += new Vector2(x, y); }
        public override void Scale(float factor)
        { radius *= factor; }

        public override float Area()
        { return (float)Math.PI * radius * radius; } // PI * r^2
        public override float Circumference()
        { return 2 * (float)Math.PI * radius; } // 2 * PI * r

        public override string ToString()
        {
            return $"{base.ToString()}  Radius:{radius}";
        }
    }

    class Hexagon : Shape
    {
        float side;

        public Hexagon(Vector2 center, float side)
            : base(center)
        {
            this.side = side;
        }

        public override void Move(float x, float y)
        { center += new Vector2(x, y); }
        public override void Scale(float factor)
        { side *= factor; }

        public override float Area()
        { return 1.5f * side * side * (float)Math.Sqrt(3); } // Wzór 6 * a^2 * sqrt(3) / 4
        public override float Circumference()
        { return 6 * side; }

        public override string ToString()
        {
            return $"{base.ToString()}  Side lenght:{side}";
        }
    }

    class Square : Shape
    {
        float side;

        public Square(Vector2 center, float side)
            : base(center)
        {
            this.side = side;
        }

        public override void Move(float x, float y)
        { center += new Vector2(x, y); }
        public override void Scale(float factor)
        { side *= factor; }

        public override float Area()
        { return side * side; }
        public override float Circumference()
        { return 4 * side; }

        public override string ToString()
        {
            return $"{base.ToString()}  Side lenght:{side}";
        }
    }

    class Shape
    {
        public Vector2 center;

        public Shape(Vector2 center)
        {
            this.center = center;
        }

        virtual public void Move(float x, float y)
        { }
        virtual public void Scale(float s)
        { }

        virtual public float Area()
        { return 0; }
        virtual public float Circumference()
        { return 0; }

        public override string ToString()
        {
            return $"Shape:  Type:{this.GetType().Name}  Center:{center}";
        }
    }


    struct Vector2
    {
        public float x;
        public float y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2 Zero { get { return new Vector2(0, 0); } }

        public override string ToString()
        {
            return $"<{x},{y}>";
        }
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }
    }
}
