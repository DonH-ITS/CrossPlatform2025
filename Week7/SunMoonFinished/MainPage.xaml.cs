namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        private int timeCounter = 0; 
        private IDispatcherTimer _timer;
        private bool _dayTheme;
        private bool _keepRaining = false;

        private Random random;
        public MainPage()
        {
            InitializeComponent();
            InitialiseTimer();
            _dayTheme = Preferences.Default.Get("DayTheme", true);
            ApplyTheme();
            this.SizeChanged += Window_SizeChanged;
            random = new Random();
        }

        private void InitialiseTimer()
        {
            _timer = Dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeCounter++;
            int hours = (timeCounter / 60) % 24;
            int minutes = timeCounter % 60;
            // Update Label, make it look nice!
            TimeLabel.Text = hours + ":" + minutes.ToString("D2");
        }

        private async void AnimateWeatherElement()
        {
            // Always move the sun/moon to the left of the screen when starting
            // We could resume from where they stopped but that would require more code 
            // So I've gone with this
            bool pauseanimation = false;
            WeatherElement.TranslationX = 0;
            WeatherElement.Rotation = 0;
            WeatherElement.Scale = 1;
            // This animation will continue indefinitely, until someone clicks to stop the button
            while (!pauseanimation)
            {
                // The first 5 seconds will translate the element across the screen
                // Also in those first 5 seconds it will rotate around at the same time as translation
                // And it will scale up to twice its size in the middle and then back to its regular size at the end
                // The pauseanimation bool is in case someone clicks the stop animation button
                WeatherElement.TranslateTo(MainGrid.Width - WeatherElement.Width, 0, 5000);
                WeatherElement.RotateTo(360, 5000);
                pauseanimation = await WeatherElement.ScaleTo(2, 2500);
                if (pauseanimation) break;
                pauseanimation = await WeatherElement.ScaleTo(1, 2500);
                if (pauseanimation) break;

                // The second 5 seconds does the reverse of the first section
                WeatherElement.TranslateTo(0, 0, 5000);
                WeatherElement.RotateTo(0, 5000);
                pauseanimation = await WeatherElement.ScaleTo(2, 2500);
                if (pauseanimation) break;
                pauseanimation = await WeatherElement.ScaleTo(1, 2500);

            }
        }

        private void StartButton_Clicked(object sender, EventArgs e)
        {
            if (_timer.IsRunning) {
                WeatherElement.CancelAnimations();
                _timer.Stop();
                StartButton.Text = "Start Animation";
            }
            else {
                AnimateWeatherElement();
                _timer.Start();
                StartButton.Text = "Stop Animation";
            }
        }

        private void ToggleThemeButton_Clicked(object sender, EventArgs e)
        {
            _dayTheme = !_dayTheme;
            ApplyTheme();
            Preferences.Default.Set("DayTheme", _dayTheme);
        }

        protected override void OnDisappearing()
        {
            // Make sure the timer stops before the page is closed
            _timer.Stop();
            base.OnDisappearing();
        }

        private void ApplyTheme()
        {
            // Make the Theme Changes Here
            // I have switch to using images now, so uncomment/comment as appropriate if you preferred the emojis
            if (_dayTheme)
            {
                Resources["PageBackgroundColour"] = Resources["DayBackgroundColour"];
                //Resources["WeatherEmoji"] = Resources["SunEmoji"];
                WeatherElement.Source = "sun.png";
                TimeLabel.TextColor = Colors.Black;
            }
            else
            {
                Resources["PageBackgroundColour"] = Resources["NightBackgroundColour"];
                //Resources["WeatherEmoji"] = Resources["MoonEmoji"];
                WeatherElement.Source = "moon.png";
                TimeLabel.TextColor = Colors.Yellow;
            }
        }

        private void Window_SizeChanged(object? sender, EventArgs e)
        {
            double size = Math.Min(this.Width, this.Height) / 4;
            WeatherElement.HeightRequest = size;
            WeatherElement.WidthRequest = size;
        }

        private async Task CreateRainDrop() {
            // This creates one raindrop and has it falling to the ground
            random = new Random();
            double size = Math.Min(MainGrid.Width, MainGrid.Height) / 15;

            // I pick a random horizontal starting position at the top of the screen
            // i.e. TranslationX is determined randomly
            Label drop = new Label
            {
                WidthRequest = size,
                HeightRequest = size,
                TranslationX = random.Next((int)MainGrid.Width - (int)size),
                FontSize = size * .6,
                Text = (string)Resources["RainEmoji"],
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Start
            };
            // Add the Raindrop to the grid
            MainGrid.Add(drop);
            // Animate the raindrop to the bottom of the window, have it go at a random speed
            await drop.TranslateTo(drop.TranslationX, MainGrid.Height, (uint)random.Next(2000, 5000));
            // After the animation is complete (i.e. raindrop is at the bottom of the screen
            // Remove the raindrop from the grid, this will clear up resources
            // Without this we would waste a lot of memory for unseen raindrops
            MainGrid.Remove(drop);
        }

        private async void CreateRainDrop_Clicked(object sender, EventArgs e) {
            _keepRaining = !_keepRaining;
            if (_keepRaining) {
                CreateMultipleRainDrops();
                CreateRainDropsBtn.Text = "Stop Rain";
            }
            else {
                CreateRainDropsBtn.Text = "Start Rain";

            }

        }

        private async void CreateMultipleRainDrops() {
            // This will keep creating raindrops until the stop button is clicked
            while (_keepRaining) {
                // CreateRainDrop is async so it returns immediately to here
                // But multiple instances of the method will be run at the same time
                CreateRainDrop();
                // Add a little delay between raindrops
                await Task.Delay(random.Next(100, 500));
            }
        }
    }
}
