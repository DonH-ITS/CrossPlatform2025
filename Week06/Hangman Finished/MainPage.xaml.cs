
namespace Hangman
{
    public partial class MainPage : ContentPage
    {
        private List<string> _words;
        private string? _theword;
        private char[]? _displayedWord;
        private DrawingGallows _drawinggallows;
        private List<char> _wrongGuesses;
        private List<char> _correctGuesses;
        private System.Timers.Timer _timer;
        private int _winCounter;
        private int _lossCounter;

        public MainPage()
        {
            InitializeComponent();
            InstantiateObjects();
            ReadPreferences();
        }

        private void InstantiateObjects() {
            _words = new List<string>();
            _drawinggallows = new DrawingGallows();
            _wrongGuesses = new List<char>();
            hangmanView.Drawable = _drawinggallows;
            _correctGuesses = new List<char>();
            _timer = new System.Timers.Timer();
            _timer.Interval = 200;
            _timer.Elapsed += (s, e) =>
            {
                hangmanView.Invalidate();
            };
        }

        private void ReadPreferences() {
            _winCounter = Preferences.Default.Get("wins", 0);
            _lossCounter = Preferences.Default.Get("loss", 0);
            WriteWinLoss();
        }

        private void WriteWinLoss() {
            if (_winCounter != 0 || _lossCounter != 0) {
                Recordlbl.Text = $"You have won {_winCounter} games out of {_winCounter + _lossCounter}, {(double)_winCounter / (_winCounter + _lossCounter):P0} of games";
            }
        }

        // I did not add any of the easy, medium, hard as I think it would clutter the code up too much
        private async Task ReadTheWords()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("words.txt");
            using var reader = new StreamReader(stream);
            while (!reader.EndOfStream) {
                string? line = await reader.ReadLineAsync();
                if (line != null)
                    _words.Add(line);
            }
        }

        protected override async void OnAppearing() {
            base.OnAppearing();
            await ReadTheWords();
        }

        private void OnGuessClicked(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(entryGuess.Text))
                return;
            MakeGuess();
        }

        private void MakeGuess() {
            char guess = char.ToUpper(entryGuess.Text[0]);
            lblStatus.Text = "";
            if (_theword != null && _displayedWord != null) {
                if (_theword.Contains(guess)) {
                    if (_correctGuesses.Contains(guess)) {
                        lblStatus.Text = $"You already have {guess}";
                    }
                    else {
                        for (int i = 0; i < _theword.Length; i++) {
                            if (_theword[i] == guess) {
                                _displayedWord[i] = guess;
                            }
                        }
                        _correctGuesses.Add(guess);
                        UpdateWordLabel();
                        if (!_displayedWord.Contains('_'))
                            FinishGame(true);
                    }
                }
                else {
                    if (!_wrongGuesses.Contains(guess)) {
                        _wrongGuesses.Add(guess);
                        UpdateWrongLabel();
                        _drawinggallows.WrongGuesses = _wrongGuesses.Count;
                        hangmanView.Invalidate();
                        if(_wrongGuesses.Count >= 6) {
                            FinishGame(false);
                        }
                    }
                    else {
                        lblStatus.Text = $"You already tried {guess}";
                    }
                }
            }
            entryGuess.Focus();
            // Clear the Entry box
            entryGuess.Text = "";
        }

        private void FinishGame(bool won) {
            if (won) {
                lblStatus.TextColor = Colors.Green;
                lblStatus.Text = $"You correctly guessed the word {_theword}!";
                _drawinggallows.Dancing = true;
                _drawinggallows.Count = 0;
                _timer.Start();
                ++_winCounter;
                Preferences.Default.Set("wins", _winCounter);
            }
            else {
                lblStatus.Text = $"Oh no, too many wrong guesses, you lose, {_theword} was the correct word";
                ++_lossCounter;
                Preferences.Default.Set("loss", _lossCounter);
            }

            // After the game finishes, we want to hide/show certain controls
            StartBtn.IsEnabled = true;
            StartBtn.IsVisible = true;
            GuessBtn.IsEnabled = false;
            GuessStack.IsVisible = false;
            lblWord.IsVisible = false;
            lblMisses.IsVisible = false;
            WriteWinLoss();

        }

        private void StartBtn_Clicked(object sender, EventArgs e) {
            ResetButtons();
            PickWord();
            entryGuess.Focus();
        }

        private void ResetButtons() {
            // Show/Hide the buttons we want, reset the status text and reset the gallows
            StartBtn.IsEnabled = false;
            StartBtn.IsVisible = false;
            lblWord.IsVisible = true;
            GuessStack.IsVisible = true;
            hangmanView.IsVisible = true;
            GuessBtn.IsEnabled = true;
            lblMisses.IsVisible = true;
            lblStatus.IsVisible = true;
            lblStatus.TextColor = Colors.DarkRed;
            lblStatus.Text = "";
            _timer.Stop();
            _drawinggallows.Dancing = false;
            _drawinggallows.WrongGuesses = 0;
            hangmanView.Invalidate();
        }

        // When someone clicks the box, remove the current letter
        private void entryGuess_Focused(object sender, FocusEventArgs e) {
            Entry entry = (Entry)sender;
            entry.Text = "";
        }

        private void PickWord() {
            // Clear the lists of guesses first
            _wrongGuesses.Clear();
            _correctGuesses.Clear();
            
            UpdateWrongLabel();

            // Pick a random word from the list
            Random random = new Random();
            int i = random.Next(0, _words.Count);
            _theword = _words[i].ToUpper();

            // Strings are immutable, so for the word we "fill in" as they make correct guesses
            // We need a character array instead so it can be changed easily
            // Don't you use character arrays in Procedural Programming?
            _displayedWord = new string('_', _theword.Length).ToCharArray();
            UpdateWordLabel();

        }

        private void UpdateWrongLabel() {
            if (_wrongGuesses != null)
                lblMisses.Text = "Wrong Guesses: " + string.Join(" ", _wrongGuesses.ToArray());
        }

        private void UpdateWordLabel() {
            // Make the label a little clearer
            if(_displayedWord != null)
                lblWord.Text = string.Join(" ", _displayedWord);
        }

    }
}
