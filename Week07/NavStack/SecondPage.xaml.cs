using System.Xml.Linq;

namespace Week7NavStack;

public partial class SecondPage : ContentPage
{
    public event EventHandler<string> DataSentBack;

    public SecondPage()
	{
		InitializeComponent();
	}

    public SecondPage(string data) {
        InitializeComponent();
        namelbl.Text = "Hello " + data;
    }

    private async void OnBackButtonClicked(object sender, EventArgs e) {
        // Navigate back to the first page
        await Navigation.PopAsync();
    }

    private async void OnBackButtonSendClicked(object sender, EventArgs e) {
        // Navigate back to the first page
        DataSentBack?.Invoke(this, "Hello From Page 2: " + SendEntry.Text);
        await Navigation.PopAsync();
    }
}