using System.Text.Json;
using System.Web;

namespace TriviaAPI
{
    public partial class MainPage : ContentPage
    {

        private HttpClient httpClient;
        private TriviaResponse? _triviaResponse;
        public MainPage()
        {
            InitializeComponent();
            httpClient = new HttpClient();
        }

        private async void DownloadBtn_Clicked(object sender, EventArgs e) {
            DownloadBtn.IsEnabled = false;
            try {
                var response = await httpClient.GetAsync("https://opentdb.com/api.php?amount=5&type=multiple");
                if (response != null && response.IsSuccessStatusCode) {
                    string text = await response.Content.ReadAsStringAsync();
                    ViewContents.Text = text;
                    _triviaResponse = JsonSerializer.Deserialize<TriviaResponse>(text);
                    if (_triviaResponse != null) {
                        string output = "" + _triviaResponse.response_code + "\n" + _triviaResponse.results.Count;
                        foreach (var question in _triviaResponse.results) {
                            output += question.question + "\n";
                        }
                        output = HttpUtility.HtmlDecode(output);
                        SomeOutput.Text = output;
                    }
                }
            }
            catch {
                await DisplayAlert("Error", "Error in downloading Webpage", "OK");
            }
            DownloadBtn.IsEnabled = true;
        }

        private async void DownloadOpt_Clicked(object sender, EventArgs e) {
            int amount;
            if(!int.TryParse(AmountEntry.Text, out amount)) {
                return;
            }
            int conversion = DifficultyPicker.SelectedIndex;
            string difficulty = "";
            switch (conversion) {
                case 1:
                    difficulty = "easy";
                    break;
                case 2:
                    difficulty = "medium";
                    break;
                case 3:
                    difficulty = "hard";
                    break;
                default:
                    difficulty = "easy";
                    break;
            }
            string fullurl = MakeURL(amount, difficulty, "multiple");
            try {
                var response = await httpClient.GetAsync(fullurl);
                if (response != null && response.IsSuccessStatusCode) {
                    string text = await response.Content.ReadAsStringAsync();
                    ViewContents.Text = text;
                    _triviaResponse = JsonSerializer.Deserialize<TriviaResponse>(text);
                    if (_triviaResponse != null) {
                        string output = "" + _triviaResponse.response_code + "\n" + _triviaResponse.results.Count;
                        foreach (var question in _triviaResponse.results) {
                            output += question.question + "\n";
                        }
                        output = HttpUtility.HtmlDecode(output);
                        SomeOutput.Text = output;
                    }
                }
            }
            catch {
                await DisplayAlert("Error", "Error in downloading Webpage", "OK");
            }
        }

        private string MakeURL(int amount, string difficulty, string type) {
            return "https://opentdb.com/api.php?amount=" + amount + "&difficulty=" + difficulty + "&type=" + type;
        }
    }
}
