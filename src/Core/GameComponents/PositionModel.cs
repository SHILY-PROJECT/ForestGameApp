namespace ForestGame.Core.GameComponents;

internal class PositionModel
{
    public PositionModel() { }

    public PositionModel(PositionModel position) : this(position.Width, position.Height) { }

    public PositionModel(int widthPosition, int heightPosition)
    {
        Width = widthPosition;
        Height = heightPosition;
    }

    public int Width { get; set; }
    public int Height { get; set; }
}