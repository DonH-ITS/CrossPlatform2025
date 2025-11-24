using Plugin.Maui.Audio;

namespace LectureAudioFile
{
    public partial class MainPage : ContentPage
    {
        private IAudioPlayer audioplayer;
        private bool _isPlaying = false;
        private bool _isPaused = false;
        private string _pickedfile = "";

        public double Volume
        {
            get
            {
                return audioplayer.Volume;
            }
            set
            {
                audioplayer.Volume = value;
            }
        }
        public bool IsPlaying
        {
            get
            {
                return _isPlaying;
            }
            set
            {
                _isPlaying = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AudioStateText));
            }
        }
        public string PickedFileName
        {
            get => _pickedfile;
            set
            {
                _pickedfile = value;
                OnPropertyChanged();
            }
        }
        

        public string AudioStateText
        {
            get
            {
                if (IsPlaying)
                    return "Playing";
                else if (_isPaused)
                    return "Paused";
                else
                    return "Stopped";
            }
        }
        public MainPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing() {
            base.OnAppearing();
            audioplayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("dicerolling.mp3"));
            audioplayer.PlaybackEnded += EndPlayback;
            audioplayer.Loop = true;
            BindingContext = this;
        }

        private async void OnStartLoopClicked(object sender, EventArgs e) {
            audioplayer.Loop = true;
            IsPlaying = true;
            _isPaused = false;
            audioplayer.Play();
        }

        private void Button_Clicked(object sender, EventArgs e) {
            audioplayer.Loop = false;
        }

        private void ButtonStop_Clicked(object sender, EventArgs e) {
            audioplayer.Stop();
            IsPlaying = false;
        }

        private void ButtonPause_Clicked(object sender, EventArgs e) {
            audioplayer.Pause();
            _isPaused = true;
            IsPlaying = false;
        }

        private void EndPlayback(object? sender, EventArgs e) {
            IsPlaying = false;
        }


        private async void PickImgButton_Clicked(object sender, EventArgs e) {
            try {
                PickOptions options = new()
                {
                    PickerTitle = "Please select an image file",
                    FileTypes = FilePickerFileType.Images,
                };
                var result = await FilePicker.Default.PickAsync(options);
                if (result != null) {
                    if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase)) {
                        Image image1 = new()
                        {
                            Source = result.FullPath
                        };
                        MainLayout.Add(image1);
                    }
                }
            }
            catch (Exception ex) {
                // The user canceled or something went wrong
            }

        }

        private async void PickButton_Clicked(object sender, EventArgs e) {
            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                     { DevicePlatform.Android, new[] { "audio/mpeg", "audio/ogg", "audio/flac" } }, // MIME type
                     { DevicePlatform.WinUI, new[] { ".mp3", ".ogg", "flac" } }, // file extension

                });
            PickOptions options = new()
            {
                PickerTitle = "Please select a music file",
                FileTypes = customFileType,
            };
            FileResult fileResult = await FilePicker.Default.PickAsync(options);
            if (fileResult != null) {
                if (fileResult.FileName.EndsWith("mp3")) {

                    using var stream = await fileResult.OpenReadAsync();
                    audioplayer = AudioManager.Current.CreatePlayer(stream);
                    audioplayer.Play();
                    PickedFileName = fileResult.FileName;
                }
            }
            else
                PickedFileName = "";
        }

    }
}
