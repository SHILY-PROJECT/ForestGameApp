namespace MyForestGame.Core.Interfaces.GameComponents;

internal interface IGameGridSize
{
    int Width { get; }
    int Height { get; }

    void SetGrid(int width, int height);
}
