namespace MyForestGame.Core.GameComponents.Objs;

internal class PlayerObject : BaseMovableGameObject
{
    public PlayerObject(IPlayerSettings settings) : base(settings.StartPosition)
    {
        Initialize(settings);
    }

    public PlayerObject(IMovementModule movementModule, IPlayerSettings settings) : base(movementModule, settings.StartPosition)
    {
        Initialize(settings);
    }

    public int Points { get; set; }

    public bool IsCoordinatesInPlayerPositionOrAbout(int width, int height)
        => (width < CurrentPosition.Width + 2 || width < CurrentPosition.Width - 2) &&
           (height < CurrentPosition.Height + 2 || height < CurrentPosition.Height - 2);

    private void Initialize(IPlayerSettings settings)
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