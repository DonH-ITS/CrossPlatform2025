public static Ellipse DrawEllipse(double height) {
	return new Ellipse()
	{
		Stroke = Color.FromRgb(0, 0, 0),
		StrokeThickness = 6,
		Fill = Color.FromRgb(0, 255, 0),
		VerticalOptions = LayoutOptions.Center,
		HorizontalOptions = LayoutOptions.Center,
		HeightRequest = height - 5,
		WidthRequest = height - 5
	};
}

public static Path MakeCrossUsingPath(double dim, int stroke, Color color) {
	Path pth = new Path()
	{
		Stroke = color,
		StrokeThickness = stroke,
		VerticalOptions = LayoutOptions.Center,
		HorizontalOptions = LayoutOptions.Center

	};
	pth.Data = new PathGeometry
	{
		Figures = new PathFigureCollection
				{
					new PathFigure
					{
						StartPoint = new Point(0,0),
						Segments = new PathSegmentCollection
						{
							new LineSegment(new Point(dim-10, dim-10))
						}
					},
				   new PathFigure
					{
						StartPoint = new Point(0 , dim - 10),
						Segments = new PathSegmentCollection
						{
							new LineSegment(new Point(dim-10, 0))
						}
					}
				}
	};
	return pth;
}