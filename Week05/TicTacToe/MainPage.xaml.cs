using Microsoft.Maui.Controls.Shapes;
using Path = Microsoft.Maui.Controls.Shapes.Path;

namespace TicTacToe2025
{
    // This has everything on the Lab PDF and maybe a little more
    // I split off the static methods into their own class file UsefulMethods.cs also on github
    public partial class MainPage : ContentPage
    {
        private bool gridcreated = false;
        private int numbrows = 3;
        private int player = 1;
        private int[,] positions;
        private bool winner = false;

        // This bool controls whether shapes are drawn or the borders are coloured in
        private bool useShapes = true;

        // Setting these as variables instead of fixing them below
        // These could be constant variables with const (instead of final)
        // But I'm not making them constant as in the future a feature could be
        // letting the user choose.
        private Color bgColour = Colors.Red;
        private Color pl1Colour = Colors.Green;
        private Color pl2Colour = Colors.Blue;
        public MainPage()
        {
            InitializeComponent();

            // Get the saved numbrows key from preferences so it persists between runs of the app
            GridSize.Text = Preferences.Default.Get("numbrows", 3).ToString();
        }
        private void StartBtn_Clicked(object sender, EventArgs e) {
            if (!gridcreated) {
                //First get the size of the grid from the box
                //Try Parse will try to Parse the entry box, if it fails numbrows will be assigned 0
                int.TryParse(GridSize.Text, out numbrows);
                //We don't want less than 3 rows in a grid and we don't want more than let's say 9
                if (numbrows <= 2) {
                    numbrows = 3;
                }
                else if (numbrows > 9)
                    numbrows = 9;
                GridSize.Text = numbrows + "";
                Preferences.Default.Set("numbrows", numbrows);
                positions = new int[numbrows, numbrows];
                //Disable the box for entering the grid size
                GridSize.IsEnabled = false;
                CreateTheGrid();
                StartBtn.IsEnabled = false;
            }
            //If the grid has already been created, call the RestartGame method instead to reset the board
            else {
                RestartGame();
                //New Game might have different number of rows, will require calling CreateTheGrid again if this is the case
                if (int.TryParse(GridSize.Text, out int newrows)) {
                    if (newrows != numbrows) {
                        numbrows = newrows;
                        if (numbrows <= 2) {
                            numbrows = 3;
                        }
                        else if (numbrows > 9)
                            numbrows = 9;
                        GridSize.Text = numbrows + "";
                        Preferences.Default.Set("numbrows", numbrows);
                        positions = new int[numbrows, numbrows];
                        ResetTheGrid();
                        CreateTheGrid();
                    }
                }
                GridSize.IsEnabled = false;
                StartBtn.IsEnabled = false;
            }
        }

        // This method is so that on a new game, if the number of rows has changed, we need to clear out the borders and rows/columns already there
        // Then we can call CreateTheGrid again with the new numbrows
        private void ResetTheGrid() {
            GridPageContent.Clear();
            GridPageContent.ColumnDefinitions.Clear();
            GridPageContent.RowDefinitions.Clear();
        }

        private void RestartGame() {
            //We need to reset a bunch of variables, there is no winner so set it to false
            //Reset so Player 1 goes first
            //Reset the text to give feedback
            winner = false;
            player = 1;
            whichplayerlabel.Text = $"Player {player}'s Turn";
            //All entries in positions have to be reset to 0
            for (int i = 0; i < numbrows; i++) {
                for (int j = 0; j < numbrows; j++) {
                    positions[i, j] = 0;
                }
            }

            //When starting a new game, we need to remove all the X's and O's from the board
            //We prepare a list to store the children ready for deletion
            //Search all the children of GridPageContent, if the child is of type Path or Ellipse we enter it into the list
            List<View> childrenToRemove = new();
            foreach (var item in GridPageContent.Children) {
                if (item.GetType() == typeof(Path)) {
                    childrenToRemove.Add((Path)item);
                }
                else if (item.GetType() == typeof(Ellipse)) {
                    childrenToRemove.Add((Ellipse)item);
                }
            }

            //Actually remove them from the Grid
            foreach (var item in childrenToRemove) {
                GridPageContent.Remove(item);
            }

            //This section of code is how to reset the colours of the grid if you didn't want to draw X's and O's
            foreach (var item in GridPageContent.Children) {
                if (item.GetType() == typeof(Border)) {
                    Border border = (Border)item;
                    border.BackgroundColor = bgColour;
                }
            }
        }

        private void FinishGame(int which) {
            if (which != 3) {
                whichplayerlabel.Text = $"Player {player} wins";
            }
            else {
                whichplayerlabel.Text = "It's a Draw";
            }
            //Set winner to be true to prevent any more moves
            winner = true;
            //Enable the start game button so we can reset the board
            StartBtn.IsEnabled = true;

            //Reenable the box for changing the GridSize
            GridSize.IsEnabled = true;
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
            //if winner is blocking DoMove from running if winner is set to true
            if (winner)
                return;

            int column = Convert.ToInt32(border.GetValue(Grid.ColumnProperty).ToString());
            int row = Convert.ToInt32(border.GetValue(Grid.RowProperty).ToString());
            if (positions[row, column] == 0) {
                positions[row, column] = player;
                double height = border.Height;
                int result = CheckWinner(player);
                bool update = true;
                if (result == player || result == 3) {
                    FinishGame(result);
                    update = false;
                }

                //Draw Cross's (X's) for player 1, remembering to change player after the cross is drawn
                if (player == 1) {

                    if (useShapes) {
                        Path cross = UsefulMethods.MakeCrossUsingPath(height, 6, Color.FromRgb(0, 0, 0));
                        GridPageContent.Add(cross, column, row);
                    }
                    else {
                        border.BackgroundColor = pl1Colour;
                    }
                    player = 2;
                }
                //Draw an ellipse for player 2
                else {
                    if (useShapes) {
                        Ellipse ell = UsefulMethods.DrawEllipse(height);
                        GridPageContent.Add(ell, column, row);
                    }
                    else {
                        border.BackgroundColor = pl2Colour;
                    }
                    player = 1;
                }
                //Only update the player label text if there has not been a winner or a draw
                if (update) whichplayerlabel.Text = "Player " + player + "'s Turn";
            }

        }

        private int CheckWinner(int player) {
            //If a row, column or diagonal is complete, we return the player number to indicate they have won
            if (UsefulMethods.SearchRowsComplete(positions, numbrows, player))
                return player;
            if (UsefulMethods.SearchColsComplete(positions, numbrows, player))
                return player;
            if (UsefulMethods.SearchDiagonalComplete(positions, numbrows, player))
                return player;
            //Check if Draw and if it is a draw return 3
            if (!UsefulMethods.FindinArray(positions, numbrows, 0))
                return 3;
            //If game can continue return 0
            return 0;
        }

    }
}
