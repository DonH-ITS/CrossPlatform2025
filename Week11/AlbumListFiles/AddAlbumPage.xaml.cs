public ObservableCollection<string> Tracks { get; } = new ObservableCollection<string>();

private readonly AlbumViewModel _albumViewModel;

public AddAlbumPage(AlbumViewModel albumViewModel) {
	InitializeComponent();
	_albumViewModel = albumViewModel;
	TracksCollectionView.BindingContext = this;
}

private void AddTrackClicked(object sender, EventArgs e) {
	if (!string.IsNullOrWhiteSpace(NewTrackEntry.Text)) {
		Tracks.Add(NewTrackEntry.Text.Trim());
		NewTrackEntry.Text = string.Empty;
	}
}

private void RemoveTrackClicked(object sender, EventArgs e) {
	if (sender is Button btn && btn.BindingContext is string track) {
		Tracks.Remove(track);
	}
}

private async void AddAlbumClicked(object sender, EventArgs e) {
	if (string.IsNullOrWhiteSpace(TitleEntry.Text)) {
		await DisplayAlert("Error", "Album title is required.", "OK");
		return;
	}
	int year;
	if(!int.TryParse(YearEntry.Text.Trim(), out year)) {
		await DisplayAlert("Error", "Invalid Year Given.", "OK");
		return;
	}
	
	var newAlbum = new Album
	{
		Title = TitleEntry.Text.Trim(),
		Artist = ArtistEntry.Text.Trim(),
		Year = year,
		Genre = GenreEntry.Text.Trim(),
		CoverUrl = CoverUrlEntry.Text.Trim(),
		Tracks = Tracks.ToList()
	};

	_albumViewModel.Albums.Add(newAlbum);
	await Navigation.PopAsync();
}