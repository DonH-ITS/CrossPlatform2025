namespace ViewModelExample
{
    public partial class MainPage : ContentPage
    {
        private MyViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();
            viewModel = new MyViewModel();
            BindingContext = viewModel;
        }

        private void ChangeText_Clicked(object sender, EventArgs e) {
            viewModel.ModelText = "New Text";
        }

        private void Button_Clicked(object sender, EventArgs e) {
            viewModel.StartStopTimer();
        }

        private void AddBookButton_Clicked(object sender, EventArgs e) {
            Int32.TryParse(publishedyear.Text, out int year);
            viewModel.AddBooks(title.Text, author.Text, year);
        }
    }
}
