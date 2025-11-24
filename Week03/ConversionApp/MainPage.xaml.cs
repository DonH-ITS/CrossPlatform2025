namespace Conversion
{
    public partial class MainPage : ContentPage
    {
        public MainPage(){
            InitializeComponent();

            ConversionTypePicker.SelectedIndex = 0;
        }

        private void btnCalculate_Clicked(object sender, EventArgs e) {
            // This method is probably too long and could be split up
            // Particularly as it is an event handler, it should not be this long
            double quantity = 0;
            try {
                 if (string.IsNullOrWhiteSpace(QuantityEntry.Text) || !double.TryParse(QuantityEntry.Text, out quantity)) {
                     lblResult.Text = "Ensure a number is entered ";
                     return;
                 }
            }
            catch {
                lblResult.Text = "Error in reading entry";
                return;
            }
            double ans;
            int conversion = ConversionTypePicker.SelectedIndex;
            string output = "", input = "";
            switch (conversion) {
                case 0:
                    ans = 2.20462 * quantity;
                    input = " kg";
                    output = " lbs";
                    break;
                case 1:
                    ans = quantity / 2.20462;
                    input = " lbs";
                    output = " kg";
                    break;
                case 2:
                    ans = quantity * 9 / 5 + 32;
                    input = "\u00B0C";
                    output = "\u00B0F";
                    break;
                case 3:
                    ans = (quantity - 32) * 5 / 9;
                    input = "\u00B0F";
                    output = "\u00B0C";
                    break;
                case 4:
                    ans = quantity * 0.621372;
                    output = " mi";
                    input = " km";
                    break;
                case 5:
                    ans = quantity / 0.621372;
                    input = " miles";
                    output = " km";
                    break;
                default:
                    ans = 0;
                    break;
            }
            
            lblResult.Text = $"The conversion: {quantity:N2}{input} is " + ans.ToString("N2") + output;
        }
    }
}
