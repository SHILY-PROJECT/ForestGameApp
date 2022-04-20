using System;

namespace MyForestGame.Core.BaseObjects;

public class GameGridSizeModel
{
    public int Width { get; set; }
    public int Height { get; set; }

    public GameGridSizeModel(int width, int height)
    {
        if (width < 8 || height < 8)
            throw new ArgumentException($"'{nameof(width)}:{width}' x '{nameof(height)}:{height}' - Minimum size 8x8");

        Width = width;
        Height = height;
    }
}