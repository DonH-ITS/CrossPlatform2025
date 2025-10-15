namespace Week5Lecture
{
    public partial class MainPage : ContentPage
    {
        private CustomDrawable _customdrawable;
        public MainPage()
        {
            InitializeComponent();
            _customdrawable = new CustomDrawable();
            _customdrawable.CircleSize = 100;
            DrawingCanvas.Drawable = _customdrawable;
        }

        private void Button_Clicked(object sender, EventArgs e) {
            // Clear Canvas
            _customdrawable.DoDraw = false;
            DrawingCanvas.Invalidate();
        }

        private void Circle_Clicked(object sender, EventArgs e) {
            // Draw Rectangle
            int width;
            if (!int.TryParse(WidthRect.Text, out width))
                return;
            _customdrawable.CircleSize = width;
            _customdrawable.DoDraw = true;
            DrawingCanvas.Invalidate();

        }

        private void DrawRectangle_Clicked(object sender, EventArgs e) {
            // Draw Rectangle
            int width, height;
            if (!int.TryParse(WidthRect.Text, out width) || !int.TryParse(HeightRect.Text, out height))
                return;
            _customdrawable.Height = height;
            _customdrawable.Width = width;
            _customdrawable.DoDraw = true;
            DrawingCanvas.Invalidate();
        }
    }
}
