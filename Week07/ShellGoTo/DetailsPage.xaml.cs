namespace Week7Nav2;

[QueryProperty(nameof(StoredProperty), "name")]
[QueryProperty(nameof(AgeProperty), "age")]

public partial class DetailsPage : ContentPage
{
	public string StoredProperty {  get; set; }
    public int AgeProperty { get; set; }
	public DetailsPage()
	{
		InitializeComponent();
		
	}

    protected override void OnAppearing() {
        base.OnAppearing();
        Blah.Text = StoredProperty + AgeProperty;
    }

    private async void Button_Clicked(object sender, EventArgs e) {
        await Shell.Current.GoToAsync("..");
    }
}