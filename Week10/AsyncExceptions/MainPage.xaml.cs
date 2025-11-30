namespace AsyncExceptions
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void RunAsyncDemo_Clicked(object sender, EventArgs e) {
            try {
                StatusLabel.Text = "Starting async operation...";
                await Task1Async();
                await Task2Async(); // This will throw, try it without await and see the difference
                StatusLabel.Text = "All tasks completed!";
            }
            catch (HttpRequestException ex) {
                StatusLabel.Text = "HttpRequestException caught: " + ex.Message;
            }
            catch (Exception ex) {
                StatusLabel.Text = "General exception caught: " + ex.Message;
            }
        }

        private async Task Task1Async() {
            await Task.Delay(1000); // simulate work
            StatusLabel.Text = "Task1 completed";
        }

        private async Task Task2Async() {
            await Task.Delay(1000); // simulate work
            StatusLabel.Text = "Task2 about to throw exception";
            throw new HttpRequestException("Simulated network error!");
        }
    }
}
