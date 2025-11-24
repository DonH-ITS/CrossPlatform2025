using System.Text.Json;
using Microsoft.Maui.Storage;


namespace JSONExample
{
    public partial class MainPage : ContentPage
    {

        public MainPage() {
            InitializeComponent();
        }

        private async void ButtonMake_Clicked(object sender, EventArgs e) {
            Person person = new Person();
            person.Name = NameEntry.Text;
            int.TryParse(AgeEntry.Text, out int value);
            person.Age = value;

            string json = JsonSerializer.Serialize(person);
            await DisplayAlert("JSON Format", json, "OK");
            await Clipboard.SetTextAsync(json);
        }

        private async void ButtonConvert_Clicked(object sender, EventArgs e) {
            string json = CopyJSON.Text;
            try {
                Person? person = JsonSerializer.Deserialize<Person>(json);
                await DisplayAlert("Output", $"Name : {person?.Name}\nAge : {person?.Age}", "OK");
            }
            catch (Exception ex) {
                await DisplayAlert("Error", "Error parsing JSON", "OK");
            }
        }

        private async void ButtonWrite_Clicked(object sender, EventArgs e) {
            string filename = Path.Combine(FileSystem.Current.AppDataDirectory, "person.json");
            await Clipboard.SetTextAsync(filename);
            Person person = new Person();
            person.Name = NameEntry.Text;
            int.TryParse(AgeEntry.Text, out int value);
            person.Age = value;

            string json = JsonSerializer.Serialize(person);
            try {
                using (FileStream outputStream = File.Create(filename)) {
                    using (StreamWriter writer = new StreamWriter(outputStream)) {
                        await writer.WriteAsync(json);
                        await DisplayAlert("Write", "File Written Successfully\n" + filename, "OK");
                    }
                }
            }
            catch (Exception ex) {
                await DisplayAlert("Error", "Error Writing JSON", "OK");
            }
        }

        private async void ButtonRead_Clicked(object sender, EventArgs e) {
            string filename = Path.Combine(FileSystem.Current.AppDataDirectory, "person.json");
            string jsonstring = "";
            try {
                using (FileStream inputStream = File.OpenRead(filename)) {
                    using (StreamReader reader = new StreamReader(inputStream)) {
                        jsonstring = await reader.ReadToEndAsync();
                    }
                }
            }
            catch {
                await DisplayAlert("Error", "Could not read file", "OK");
                return;
            }

            try {
                Person? person = JsonSerializer.Deserialize<Person>(jsonstring);
                NameEntry.Text = person.Name;
                AgeEntry.Text = person.Age.ToString();
            }
            catch {
                await DisplayAlert("Error", "Could not parse json", "OK");
            }

        }
        private async void ButtonWriteEntry_Clicked(object sender, EventArgs e) {
            string filename = Path.Combine(FileSystem.Current.AppDataDirectory, "entry.json");
            Entry entry = new Entry("John Doe", 30, "Engineer");
            await Clipboard.SetTextAsync(filename);
            await entry.WritetoJson(filename);
            await DisplayAlert("Status", "Done", "OK");
        }
        private async void ButtonReadEntry_Clicked(object sender, EventArgs e) {
            string filename = Path.Combine(FileSystem.Current.AppDataDirectory, "entry.json");
            string jsonstring = "";
            using (FileStream inputStream = File.OpenRead(filename)) {
                using (StreamReader reader = new StreamReader(inputStream)) {
                    jsonstring = await reader.ReadToEndAsync();
                }
            }
            Entry entry = JsonSerializer.Deserialize<Entry>(jsonstring);
            await DisplayAlert("Entry Stuff", $"Name: {entry.Name}\nAge: {entry.Age}\nOccupation: {entry.Occupation}", "OK");
        }

        private async void ButtonWriteEntryList_Clicked(object sender, EventArgs e) {
            string filename = Path.Combine(FileSystem.Current.AppDataDirectory, "listentry.json");
            List<Entry> people = new List<Entry>();

            people.Add(new Entry("John Doe", 30, "Engineer"));
            people.Add(new Entry("Jane Smith", 25, "Analyst"));

            string json = JsonSerializer.Serialize(people);
            using (FileStream outputStream = File.Create(filename)) {
                using (StreamWriter writer = new StreamWriter(outputStream)) {
                    await writer.WriteAsync(json);
                    await DisplayAlert("Write", "File Written Successfully\n" + filename, "OK");
                }
            }
        }

        private async void ButtonReadEntryList_Clicked(object sender, EventArgs e) {
            string filename = Path.Combine(FileSystem.Current.AppDataDirectory, "listentry.json");
            string jsonstring;
            using (FileStream inputStream = File.OpenRead(filename)) {
                using (StreamReader reader = new StreamReader(inputStream)) {
                    jsonstring = await reader.ReadToEndAsync();
                }
            }
            List<Entry> entries = JsonSerializer.Deserialize<List<Entry>>(jsonstring);
            await DisplayAlert("Read", $"{entries.Count} objects read", "OK");
        }
    }
}
