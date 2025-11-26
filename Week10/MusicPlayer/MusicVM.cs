using System.ComponentModel;
using Plugin.Maui.Audio;

namespace MusicPlayer
{
    public class MusicVM : INotifyPropertyChanged
    {
        private IAudioPlayer? openedMusic;
        private string _pickedFilename = "";
        private double _musicPosition = 0;
        private bool _showControls = false, _musicPlaying = false;
        private IDispatcherTimer _timer;

        public bool Dragging { get; set; } = false;
        public bool ShowControls
        {
            get
            {
                return _showControls;
            }
            set
            {
                _showControls = value;
                OnPropertyChanged();
            }
        }
        public string PickedFileName
        {
            get => _pickedFilename;
            set
            {
                if (value != _pickedFilename) {
                    _pickedFilename = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool MusicPlaying
        {
            get => _musicPlaying;
            set
            {
                if (value != _musicPlaying) {
                    _musicPlaying = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(MusicNotPlaying));
                }
            }
        }
        public bool MusicNotPlaying => !MusicPlaying;
        public double MusicVolume
        {
            get
            {
                return openedMusic != null ? openedMusic.Volume : 1;
            }
            set
            {
                if (openedMusic != null) {
                    openedMusic.Volume = value;
                }
            }
        }
        public double MusicLength
        {
            get
            {
                return openedMusic != null ? openedMusic.Duration : 0;
            }
        }
        public double MusicPosition
        {
            get
            {
                return _musicPosition;
            }
            set
            {
                if (_musicPosition != value) {
                    _musicPosition = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PositionText));
                }
            }
        }
        public string PositionText
        {
            get
            {
                if (openedMusic != null) {
                    double position = openedMusic.CurrentPosition;
                    int minutes = (int)(position / 60);
                    int seconds = (int)(position % 60);
                    return minutes + ":" + seconds.ToString("D2");
                }
                else {
                    return "";
                }
            }
        }

        public async Task PickFile() {
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
            ShowControls = false;
            FileResult? fileResult = await FilePicker.Default.PickAsync(options);
            if (fileResult != null) {
                try {
                    if (fileResult.FileName.EndsWith("mp3") || fileResult.FileName.EndsWith("ogg") || fileResult.FileName.EndsWith("flac")) {
                        using var stream = await fileResult.OpenReadAsync();
                        // Make sure to stop any currently playing song before creating a new object
                        Stop();
                        // This creates a new object with the new file, if there was an old object it will be dereferenced
                        openedMusic = AudioManager.Current.CreatePlayer(stream);
                        openedMusic.PlaybackEnded += OpenedMusic_PlaybackEnded;
                        // Make sure all the properties bound to UI are updated
                        PickedFileName = fileResult.FileName;
                        OnPropertyChanged(nameof(MusicLength));
                        OnPropertyChanged(nameof(MusicPosition));
                        OnPropertyChanged(nameof(MusicVolume));
                        OnPropertyChanged(nameof(PositionText));
                        ShowControls = true;
                    }
                }
                catch {
                    ShowControls = false;
                }
            }
        }

        // When someone clicks the stop button or it gets to the end of the song, this event handler is called
        private void OpenedMusic_PlaybackEnded(object? sender, EventArgs e) {
            
            _timer?.Stop();
            MusicPlaying = false;
        }

        public MusicVM() {
            _timer = Application.Current.Dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            // The timer tick will update the position of the slider and also the text saying how long has been played
            _timer.Tick += ((s, e) =>
            {
                // Only update the music position slider if someone is not dragging it
                if (openedMusic != null && !Dragging && MusicPlaying) {
                    MusicPosition = openedMusic.CurrentPosition;
                }
            });
        }

        public void Start() {
            openedMusic?.Play();
            _timer.Start();
            MusicPlaying = true;
        }

        public void Stop() {
            openedMusic?.Stop();
            MusicPosition = 0;
        }

        public void Pause() {
            openedMusic?.Pause();
            _timer.Stop();
            MusicPlaying = false;
        }

        public void SeekTo(double to) {
            openedMusic?.Seek(to);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string? propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
