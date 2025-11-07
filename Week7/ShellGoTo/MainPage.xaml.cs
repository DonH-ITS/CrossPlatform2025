namespace Week7Nav2
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e) {
            await Shell.Current.GoToAsync("details?name=John&age=20");
        }
    }
}
