namespace Week8DataBindingMain
{
    public partial class MainPage : ContentPage
    {
        private int _seconds;
        public int Seconds
        {
            get => _seconds;
            set
            {
                _seconds = value;
                OnPropertyChanged();
            }
        }

        private string _myText;
        public string MyText
        {
            get
            {
                return _myText;
            }
            set
            {
                _myText = value;
                OnPropertyChanged();
            }
        }

        private int _myAge;
        public int MyAge
        {
            get
            {
                return _myAge;
            }
            set
            {
                _myAge = value;
                OnPropertyChanged(nameof(MyAge));
            }
        }
        

        public MainPage() {
            InitializeComponent();
            InitialiseTimers();
            MyText = "blah blah";
            BindingContext = this; 
        }

        private System.Timers.Timer _systemtimer;
        private IDispatcherTimer _dispatchertimer;
        public void InitialiseTimers() {
            Seconds = 0;
            _systemtimer = new System.Timers.Timer
            {
                Interval = 1000
            };
            _systemtimer.Elapsed += (s, e) =>
            {
                Seconds++;
                //SecondsOutput.Text = $"{Seconds} seconds gone";
            };
            _dispatchertimer = Dispatcher.CreateTimer();
            _dispatchertimer.Interval = TimeSpan.FromMilliseconds(1000);
            _dispatchertimer.Tick += (s, e) =>
            {
                Seconds++;
                SecondsOutput.Text = $"{Seconds} seconds gone";
            };
        }



        private void CheckBtn_Clicked(object sender, EventArgs e) {
            DisplayAlert("Hello", MyText, "ok");
        }

        private void CheckBtn2_Clicked(object sender, EventArgs e) {
            DisplayAlert("Hello", MyAge.ToString(), "ok");
        }

        private void ChangeText(object sender, EventArgs e) {
            MyText = "Hello, you are " + MyAge + "years old";
        }

        private void Button_Clicked(object sender, EventArgs e) {
            _systemtimer.Start();
        }

        private void Button_Clicked_1(object sender, EventArgs e) {
            _dispatchertimer.Start();
        }
    }
}