namespace Week3Lecture
{
    public partial class MainPage : ContentPage
    {
        private IDispatcherTimer? timer;
        int timeElapsed = 0;
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            SetUpTimer();
        }


        private void SetUpTimer() {
            timer = Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += OnTimerTick;
        }

        private void OnTimerTick(object? sender, EventArgs e) {
            label.RotationX = 360 * timeElapsed / 1000.0;
            timeElapsed += 100;
        }

        private void OnButtonPressed(object sender, EventArgs e)
        {
            Button pressed = (Button)sender;
            if (pressed != null) {
                pressed.Text = "Pressed";
                pressed.BackgroundColor = Color.FromRgb(100, 100, 100);
            }
            timer?.Start();
        }

        private void OnButtonReleased(object sender, EventArgs e) {
            Button theButn = (Button)sender;
            if (theButn != null) {
                theButn.BackgroundColor = Colors.BlueViolet;
                theButn.Text = "Released";
            }
            lblTimer.Text = "Button has been held down for " + timeElapsed / 1000.0 + "seconds";
            timer?.Stop();
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e) {
            Entry ent = (Entry)sender;
            label1.Text = "TextChanged: " + ent.Text;
        }
        
        private void Entry_Completed(object sender, EventArgs e) {
            Entry ent = (Entry)sender;
            label2.Text = "Completed: " + ent.Text;
        }

        private void Button_Clicked(object sender, EventArgs e) {
            // If there are no Gestures Add one
            if(imgBot.GestureRecognizers.Count == 0){
                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += OnImageTapped;
                imgBot.GestureRecognizers.Add(tapGestureRecognizer);
            }
            // If there is a gesture remove them (code below only works with one)
            else {
                foreach (var gesture in imgBot.GestureRecognizers) {
                    if (gesture.GetType() == typeof(TapGestureRecognizer)) {
                        imgBot.GestureRecognizers.Remove(gesture);
                        /* Once we remove the gesture, without the break it crashes
                         * This is because the loop definition will now cause an out of bounds error
                         * As after deleting the Count is now lower
                         * This break will work as we know there is only one Gesture
                         */
                        break;

                    }
                }
            }
        }

        private void OnImageTapped(object? sender, EventArgs e) {
            ++count;
            lblCount.Text = "Counter " + count;
            DisplayAlert("You have tapped the image", "img tap", "OK");    
        }

        private void OnSwipeGesture(object sender, SwipedEventArgs e) {
            BoxView swipedLabel = (BoxView)sender;
            lblWork.Text = $"Swiped {e.Direction} on box";
        }

        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) {
            lblWork.Text = "Tapped on Image";
        }

        private void TapGestureRecognizer_Tapped2(object sender, TappedEventArgs e) {
            lblWork.Text = "Double Tapped on Image";
        }
    }
}