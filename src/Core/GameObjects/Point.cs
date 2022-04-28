namespace ForestGame.Core.GameObjects;

internal class Point : BaseImmovableGameObject
{
    public Point(PointSettings pointSettings) : base(pointSettings.StartPosition)
    {
        Name = pointSettings.Name;
        Model = pointSettings.DisplayedModel;
        Points = pointSettings.Points;
        ColorObject = pointSettings.ColorObject;
        ColorBackground = pointSettings.ColorBackground;
    }

    public int Points { get; set; }
}