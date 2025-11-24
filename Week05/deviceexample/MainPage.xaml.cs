namespace Week5Device
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            
            DeviceDisplay.Current.MainDisplayInfoChanged += Current_MainDisplayInfoChanged;

            this.SizeChanged += OnWindowChange;

            
            
        }

        private void OnWindowChange(object sender, EventArgs e) {
            widthDisplay.Text = "Width : " + this.Width;
            heightDisplay.Text = "Height : " + this.Height;
        }

        private void Current_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e) {
            DisplayAlert("Orientation", $"Current Orientation: {DeviceDisplay.Current.MainDisplayInfo.Orientation}", "Ok");
        }

        private void Button_Clicked(object sender, EventArgs e) {
            Shell.Current.DisplayAlert("Dimensions", "Height: " + DeviceDisplay.Current.MainDisplayInfo.Height + " Width: " + DeviceDisplay.Current.MainDisplayInfo.Width, "Ok");
        }

        protected override void OnSizeAllocated(double width, double height) {
            base.OnSizeAllocated(width, height);
            outputLbl.Text = "hello " + this.Width;
        }

        private void ShowButton_Clicked(object sender, EventArgs e) {
            DisplayAlert("Orientation", DeviceDisplay.Current.MainDisplayInfo.Orientation.ToString(), "Ok");
            DisplayAlert("a", "width: " + this.Width + "height " + this.Height, "ok");
        }

        private void Button_Clicked_1(object sender, EventArgs e) {
#if WINDOWS
            this.Window.Width = 600;
            this.Window.Height = 600;
#elif ANDROID
            Platform.CurrentActivity.RequestedOrientation = Android.Content.PM.ScreenOrientation.Landscape;
#endif
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
        }

        protected override void OnAppearing() {
            base.OnAppearing();

        }
    }
}
