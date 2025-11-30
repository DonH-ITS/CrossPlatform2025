using Plugin.Maui.Audio;

namespace ErrorExamples
{
    public partial class MainPage : ContentPage
    {
        List<Image> images;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnDivideClicked(object sender, EventArgs e) {
            try {
                int number = Convert.ToInt32(DivisionEntry.Text);

                // *** BREAKPOINT HERE ***
                int result = 100 / number;

                DivisionResultLabel.Text = $"100 / {number} = {result}";
            }
            catch (DivideByZeroException) {
                await DisplayAlert("Error", "Cannot divide by zero!", "OK");
            }
            catch (Exception ex) {
                await DisplayAlert("Error", $"Invalid input: {ex.Message}", "OK");
            }
            
                await DisplayAlert("Finally", "Division example finished.", "OK");
            
        }


        private async void OnFileReadClicked(object sender, EventArgs e) {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, "missing.txt");

            try {
                // *** BREAKPOINT HERE ***
                string text = File.ReadAllText(filePath);

                FileResultLabel.Text = text;
            }
            catch (FileNotFoundException fnf) {
                await DisplayAlert("File Error", $"File not found:\n{fnf.FileName}", "OK");
            }
            catch (UnauthorizedAccessException) {
                await DisplayAlert("Permission Error",
                    "The app does not have permission to read this file.",
                    "OK");
            }
            catch (IOException ioex) {
                await DisplayAlert("I/O Error",
                    $"The file may be in use by another process or inaccessible.\n\nDetails:\n{ioex.Message}",
                    "OK");
            }
            catch (Exception ex) {
                await DisplayAlert("Unknown Error",
                    $"Something unexpected happened:\n{ex.Message}",
                    "OK");
            }
            finally {
                await DisplayAlert("Finally", "File example finished.", "OK");
            }
        }

        private async void OnAgeClicked(object sender, EventArgs e) {
            try {
                int age = Convert.ToInt32(AgeEntry.Text);

                if (age < 0)
                    throw new ArgumentException("Age cannot be negative.");

                AgeResultLabel.Text = $"Your age is {age}";
            }
            catch (Exception ex) {
                await DisplayAlert("Error", ex.Message, "OK");
            }
            finally {
                await DisplayAlert("Finally", "Age example completed.", "OK");
            }
        }

        private async void Button_Clicked(object sender, EventArgs e) {
            await Task.Delay(100);
            images = new List<Image>();
            for(int i=0; i<1000000; i++) {
                Image image = new Image()
                {
                    Source = "ufo.png",
                    WidthRequest = 256,
                    HeightRequest = 256,
                };
                images.Add(image);
            }
        }
    }
}
