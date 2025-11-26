namespace MusicPlayer
{
    public partial class MainPage : ContentPage
    {
        private MusicVM _musicViewModel;

        public MainPage()
        {
            InitializeComponent();
            _musicViewModel = new MusicVM();
            BindingContext = _musicViewModel;
        }

        // If you want to make this fully follow the MVVM idea, these event handlers should be replaced with Commands in the VM
        private async void PickButton_Clicked(object sender, EventArgs e) {
            await _musicViewModel.PickFile();
        }

        private void StartButton_Clicked(object sender, EventArgs e) {
            _musicViewModel.Start();
        }
        private void PauseButton_Clicked(object sender, EventArgs e) {
            _musicViewModel?.Pause();
        }
        private void StopButton_Clicked(object sender, EventArgs e) {
            _musicViewModel.Stop();
        }
        private void Start_Drag(object sender, EventArgs e) {
            _musicViewModel.Dragging = true;
        }
        private void Slider_DragCompleted(object sender, EventArgs e) {
            var slider = (Slider)sender;
            _musicViewModel.SeekTo(slider.Value);
            _musicViewModel.Dragging = false;
        }
    }
}
