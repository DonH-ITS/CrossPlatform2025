using Plugin.Maui.Audio;

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

        private async void PickButton_Clicked(object sender, EventArgs e) {
            await _musicViewModel.PickFile();
        }

        private async void StartButton_Clicked(object sender, EventArgs e) {
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
