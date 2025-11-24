
namespace Hangman
{
    internal class DrawingGallows : IDrawable
    {
        public int WrongGuesses { get; set; }
        public bool Dancing { get; set; }
        public uint Count { get; set; } = 0;
        public void Draw(ICanvas canvas, RectF dirtyRect) {
            if (Dancing) {
                DrawDance(canvas);
                return;
            }

            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 3;

            // Base and pole
            canvas.DrawLine(20, 180, 120, 180);
            canvas.DrawLine(70, 180, 70, 20);
            canvas.DrawLine(70, 20, 130, 20);
            canvas.DrawLine(130, 20, 130, 40);

            if (WrongGuesses > 0)
                canvas.DrawCircle(130, 55, 15); // Head
            if (WrongGuesses > 1)
                canvas.DrawLine(130, 70, 130, 120); // Body
            if (WrongGuesses > 2)
                canvas.DrawLine(130, 80, 110, 100); // Left arm
            if (WrongGuesses > 3)
                canvas.DrawLine(130, 80, 150, 100); // Right arm
            if (WrongGuesses > 4)
                canvas.DrawLine(130, 120, 110, 150); // Left leg
            if (WrongGuesses > 5)
                canvas.DrawLine(130, 120, 150, 150); // Right leg
        }

        private void DrawDance(ICanvas canvas) {
            canvas.StrokeColor = Colors.Green;
            canvas.StrokeSize = 3;

            canvas.DrawCircle(100, 55, 15); // Head
            canvas.DrawLine(100, 70, 100, 120); // Body
            if (Count % 3 == 0) {
                canvas.DrawLine(100, 80, 80, 100); // Left arm
                canvas.DrawLine(100, 80, 120, 75); // Right arm
                canvas.DrawLine(100, 120, 80, 125); // Left le
                canvas.DrawLine(100, 120, 120, 150); // Right leg
            }
            else if(Count % 3 == 1) {
                canvas.DrawLine(100, 80, 80, 75); // Left arm
                canvas.DrawLine(100, 80, 120, 100); // Right arm
                canvas.DrawLine(100, 120, 80, 150); // Left le
                canvas.DrawLine(100, 120, 120, 125); // Right leg
            }
            else {
                canvas.DrawLine(100, 80, 80, 100); // Left arm
                canvas.DrawLine(100, 80, 120, 100); // Right arm
                canvas.DrawLine(100, 120, 80, 150); // Left le
                canvas.DrawLine(100, 120, 120, 150); // Right leg
            }
            ++Count;
            if (Count > 200000) Count = 0;
        }
    }
}
