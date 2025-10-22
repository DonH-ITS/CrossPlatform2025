namespace AnimationDemo2025
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnMoveBoxClicked(object sender, EventArgs e) {
            await MyBox.TranslateTo(100, 0, 1000); // Move right
            await MyBox.TranslateTo(0, 100, 1000); // Move down
        }

        private async void OnMoveBoxClicked2(object sender, EventArgs e) {
            double currentRotation = MyBox.RotationX;
            double currentTranslation = MyBox.TranslationX;
            double currentTransY = MyBox.TranslationY;
            await MyBox.RotateXTo(currentRotation + 180, 1000);
            await MyBox.TranslateTo(currentTranslation + 100, currentTransY, 1000); // Move right
            await MyBox.TranslateTo(currentTranslation + 100, currentTransY + 100, 1000);    // Move down
        }

        private async void OnFadeBoxClicked(object sender, EventArgs e) {
            await MyBox.FadeTo(0, 1000); // Fade out
            await Task.Delay(500);
            await MyBox.FadeTo(1, 1000); // Fade in
        }

        private async void OnScaleAndRotateClicked(object sender, EventArgs e) {
            await MyBox.ScaleTo(2, 1000);   // Double the size
            await MyBox.RotateTo(360, 1000); // Rotate 360 degrees
            await MyBox.ScaleTo(1, 1000);   // Return to original size
        }

        private async void MultipleAnimationsClicked(object sender, EventArgs e) {
            MyBox.RotateTo(180, 1000);
            await MyBox.TranslateTo(50, 50, 1000);
            await Task.Delay(1000);
            MyBox.RotateTo(0, 1000);
            await MyBox.TranslateTo(0, 0, 1000);
        }

        private async void MoveBox(object sender, EventArgs e) {
            bool wascancelled = await MyBox.TranslateTo(150, 150, 3000);
            if (wascancelled) {
                await DisplayAlert("WHY?", "Why did you stop the box moving?!", "OK");
                MyBox.TranslationX = 0; MyBox.TranslationY = 0;
            }
           /* wascancelled = await MyBox.RotateXTo(150, 3000);
            if (wascancelled) {
                await DisplayAlert("WHY?", "Why did you stop the box rotating?!", "OK");
                MyBox.RotationX = 0;
            }*/
        }

        private void CancelAnimationsClicked(object sender, EventArgs e) {
            MyBox.CancelAnimations();
        }
    }
}
