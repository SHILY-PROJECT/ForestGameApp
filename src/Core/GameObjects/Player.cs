namespace ForestGame.Core.GameObjects;

internal class Player : BaseMovableGameObject
{
    public Player(PlayerSettings settings) : base(settings.StartPosition)
    {
        Initialize(settings);
    }

    public Player(IMovementModule movementModule, PlayerSettings settings) : base(movementModule, settings.StartPosition)
    {
        Initialize(settings);
    }

    public int Points { get; set; }

    public bool IsCoordinatesInPlayerPositionOrAbout(int width, int height)
        => (width < CurrentPosition.Width + 2 || width < CurrentPosition.Width - 2) &&
           (height < CurrentPosition.Height + 2 || height < CurrentPosition.Height - 2);

    private void Initialize(PlayerSettings settings)
    {
        if (settings.Name.All(x => char.IsLetter(x)))
        {
            Name = settings.Name;
            Model = settings.DisplayedModel;
            ColorObject = settings.ColorObject;
            ColorBackground = settings.ColorBackground;
        }
        else throw new ArgumentException($"'{nameof(settings.Name)}:{settings.Name}' invalid argument.");
    }
}