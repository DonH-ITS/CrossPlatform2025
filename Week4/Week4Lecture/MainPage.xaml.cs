using Microsoft.Maui.Dispatching;

namespace AwaitAsyncApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage() {
            InitializeComponent();
        }

        private void SyncBtn_Clicked(object sender, EventArgs e) {
            lblupd.Text = "Beginning task delay";
            var t = Task.Delay(6000);
            t.Wait();
            lblupd.Text = "I was updated by the Sync Method \u2620";
        }

        private async void ASyncBtn_Clicked(object sender, EventArgs e) {
            Button btn = (Button)sender;
            btn.IsEnabled = false;
            lblupd.Text = "asynchronous running \u26F9";
            await Task.Delay(3000);
            await btn.FadeTo(0, 3000);
            lblupd.Text = "Updated by Asynchronous Method \u263A";
// btn.Opacity = 1;
            btn.IsEnabled = true;
        }

        private async void Button_Clicked(object sender, EventArgs e) {
            await DisplayAlert("Alert", "You have been alerted", "OK");

            bool yesno = await DisplayAlert("Alert", "Yes or No", "Yes", "No");
            if (yesno) {
                lblupd.Text = "clicked Yes";
                return;
            }
            else {
                lblupd.Text = "Clicked no";
            }
            string result = await DisplayPromptAsync("Question 1", "What's your name?");

            if(result != null) 
                await DisplayAlert("Hello", "Hello " + result, "Good Bye");
            else 
                await DisplayAlert("Alert", "Why you no give name", "\u2639");
        }

    }

}
