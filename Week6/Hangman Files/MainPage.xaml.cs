namespace Hangman
{
    public partial class MainPage : ContentPage
    {
        private List<string> _words;
        private string _theword;
        private char[] _displayedWord;

        public MainPage()
        {
            InitializeComponent();
            _words = new List<string>();
        }

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
            char guess = char.ToUpper(entryGuess.Text[0]);
            if (_theword.Contains(guess)) {
                for (int i = 0; i < _theword.Length; i++) {
                    if(_theword[i] == guess) {
                        _displayedWord[i] = guess;
                    }
                }
                lblWord.Text = new string(_displayedWord);
            }
            else {
                // Letter is not in the word
            }
        }

        private void StartBtn_Clicked(object sender, EventArgs e) {
            StartBtn.IsEnabled = false;
            lblWord.IsVisible = true;
            GuessStack.IsVisible = true;
            Random random = new Random();
            int i = random.Next(0, _words.Count);
            _theword = _words[i];

            // Strings are immutable, so for the word we "fill in" as they make correct guesses
            // We need a character array instead so it can be changed easily
            // Don't you use character arrays in Procedural Programming?
            _displayedWord = new string('_', _theword.Length).ToCharArray();
            lblWord.Text = new string(_displayedWord);
        }

        // When someone clicks the box, remove the current letter
        private void entryGuess_Focused(object sender, FocusEventArgs e) {
            Entry entry = (Entry)sender;
            entry.Text = "";
        }
    }
}
