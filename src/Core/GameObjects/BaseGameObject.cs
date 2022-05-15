namespace ForestGame.Core.GameObjects;

internal abstract class BaseGameObject : IGameObject
{
    public string Name { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public ConsoleColor ColorBackground { get; set; }
    public ConsoleColor ColorObject { get; set; }
    public PositionModel? CurrentPosition { get; set; }
    public bool IsVisible { get; set; } = true;

    public void SetCurrentPosition(int width, int hight)
        => CurrentPosition = new(width, hight);

    public void SetCurrentPosition(PositionModel position)
        => SetCurrentPosition(position.Width, position.Height);

    public bool IsCurrentPosition(int width, int hight)
        => width == CurrentPosition?.Width && hight == CurrentPosition.Height && IsVisible;

    public bool IsCurrentPosition(PositionModel position)
        => IsCurrentPosition(position.Width, position.Height);
}