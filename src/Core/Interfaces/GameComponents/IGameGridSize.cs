namespace MyForestGame.Core.Interfaces.GameComponents;

public interface IGameGridSize
{
    int Width { get; }
    int Height { get; }

    void SetGrid(int width, int height);
}
