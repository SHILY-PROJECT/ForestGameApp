namespace ForestGame.Core.GameObjects.Settings;

internal record PointSettings : BaseSettings
{
    public PointSettings(string name, string displayedModel, ConsoleColor colorObject, ConsoleColor colorBackground, int points)
        : base(name, displayedModel, colorObject, colorBackground) => Points = points;

    public int Points { get; set; }
}