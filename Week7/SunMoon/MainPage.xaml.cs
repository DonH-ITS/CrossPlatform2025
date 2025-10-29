namespace SunMoving
{
    public partial class MainPage : ContentPage
    {
        private int timeCounter = 0; // Simulated minutes
        private IDispatcherTimer _timer;
		
        public MainPage() {
            InitializeComponent();
            InitialiseTimer();
            // TODO: Load saved theme preference using Preferences and Apply Theme
            // TODO: Handle window size changes
        }
		
		private void InitialiseTimer(){
			// Initialise your timer here
		}
		
        private void Timer_Tick(object sender, EventArgs e) {
            // Increment timer
            int hours = (timeCounter / 60) % 24;
            int minutes = timeCounter % 60;
			// Update Label, make it look nice!
        }		
		
        private async void AnimateWeatherElement() {
            // TODO: Move the sun/cloud across the screen
            // The sun should:
            // 1. Translate horizontally from left to right
            // 2. Rotate slightly while moving (to simulate rotation of the sun)
            // 3. Scale subtly up and down to mimic a natural rising/setting effect
            // Example animations: TranslateTo, RotateTo, ScaleTo
        }
		
		private void StartButton_Clicked(object sender, EventArgs e) {
			AnimateWeatherElement();
		}

		
        private void ToggleTheme() {
            // TODO: Switch DynamicResource colors between day/night
            // Save preference to Preferences
        }
		
		private void ApplyTheme(){
			// Make the Theme Changes Here
		}
		
        private void Window_SizeChanged(object sender, EventArgs e) {
            // TODO: Adjust WeatherElement size based on available window size
        }
    }
}