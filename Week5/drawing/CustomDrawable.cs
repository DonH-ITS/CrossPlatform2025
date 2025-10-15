using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week5Lecture
{
    public class CustomDrawable : IDrawable
    {
        public int CircleSize { get; set; }
        public int Height { get; set; } = 50;
        public int Width { get; set; } = 50;
        public bool DoDraw { get; set; } = true;
        public void Draw(ICanvas canvas, RectF dirtyRect) {
            canvas.StrokeColor = Colors.Black;
            canvas.StrokeSize = 2;
            if (DoDraw) {
                // Draw a rectangle
                canvas.DrawRectangle(50, 50, Width, Height);

                // Draw a circle
                canvas.FillColor = Colors.Blue;
                canvas.FillCircle(150, 150, CircleSize);
            }
        }
    }
}
