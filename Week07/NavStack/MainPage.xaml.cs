namespace Week7NavStack
{
    public partial class MainPage : ContentPage
    {
        private Animal animal;

        public MainPage() {
            InitializeComponent();
            animal = new Animal();
            animal.name = "Wolfie";
            animal.type = "Dog";
        }

        private async void ButtonPage2_Clicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new SecondPage());

        }

        private async void ButtonPage3_Clicked(object sender, EventArgs e) {
            string name = nameEntry.Text;
            await Navigation.PushAsync(new SecondPage(name));
        }

        private async void ButtonPageReceive_Clicked(object sender, EventArgs e) {
            string name = nameEntry.Text;

            // Create the second page object
            var secondPage = new SecondPage(name);

            // Subscribe to the event with an eventhandler
            secondPage.DataSentBack += (s, data) =>
            {
                string received = (string)data;
                TextLabel.Text = received;
            };
            await Navigation.PushAsync(secondPage);
        }

        private async void ButtonGoto3_Clicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new Page3(animal));
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            animalName.Text = animal.name;
            animalType.Text = animal.type;
        }
    }
}
