namespace MyForestGame.Core.GameComponents;

public class GameGridSize : IGameGridSize
{
    public GameGridSize() { }

    public GameGridSize(int width, int height)
    {
        SetGrid(width, height);
    }

    public int Width { get; set; }
    public int Height { get; set; }

    public void SetGrid(int width, int height)
    {
        if (width >= 8 && height >= 8)
        {
            Width = width;
            Height = height;
        }
        else throw new ArgumentException($"'{nameof(width)}:{width}' x '{nameof(height)}:{height}' minimum size 8x8");
    }
}