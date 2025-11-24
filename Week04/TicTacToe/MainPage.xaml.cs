namespace TicTacToe2025
{
    // This has done to the end of DoMove section, just before Checking the filled squares
    public partial class MainPage : ContentPage
    {
        private bool gridcreated = false;
        private int numbrows = 3;
        private int player = 1;
        public MainPage()
        {
            InitializeComponent();
        }
        private void StartBtn_Clicked(object sender, EventArgs e) {
            if (!gridcreated) {
                CreateTheGrid();
                Button mybtn = (Button)sender;
                mybtn.IsEnabled = false;
            }
        }

        private void CreateTheGrid() {
            //Create numbrows rows and numbrows columns 3x3, 4x4 etc.
            for (int i = 0; i < numbrows; ++i) {
                GridPageContent.AddRowDefinition(new RowDefinition());
                GridPageContent.AddColumnDefinition(new ColumnDefinition());
            }

            //Populate the grid with Borders
            for (int i = 0; i < numbrows; ++i) {
                for (int j = 0; j < numbrows; ++j) {
                    Border styledBorder = new Border
                    {
                        BackgroundColor = Colors.Red,
                        Stroke = Colors.Black,
                        StrokeThickness = 3

                    };
                    TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += OnBorderTapped;
                    styledBorder.GestureRecognizers.Add(tapGestureRecognizer);
                    GridPageContent.Add(styledBorder, j, i);
                }
            }
            gridcreated = true;
            whichplayerlabel.Text = "Player 1's Turn";
        }

        private void OnBorderTapped(object? sender, TappedEventArgs e) {
            Border border = (Border)sender;
            if (border != null) {
                DoMove(border);
            }
        }

        private void DoMove(Border border) {
            if(player == 1) {
                border.BackgroundColor = Color.FromRgb(0, 255, 0);
                player = 2;
            }
            else {
                border.BackgroundColor = Color.FromRgb(0, 0, 255);
                player = 1;
            }
            whichplayerlabel.Text = "Player " + player + "'s Turn";
        }

    }
}
