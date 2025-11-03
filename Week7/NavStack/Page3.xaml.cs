namespace Week7NavStack;

public partial class Page3 : ContentPage
{
	private readonly Animal animal;
	public Page3(Animal passed)
	{
		InitializeComponent();
		animal = passed;
		NameEntry.Text = animal.name;
		TypeEntry.Text = animal.type;
	}

    private async void Button_Clicked(object sender, EventArgs e) {
		animal.name = NameEntry.Text;
		animal.type = TypeEntry.Text;
		await Navigation.PopAsync();
    }
}