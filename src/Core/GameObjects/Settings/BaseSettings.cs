namespace ForestGame.Core.GameObjects.Settings;

internal record BaseSettings
{
    public BaseSettings() { }

    public BaseSettings(string name, string displayedModel, ConsoleColor colorObject, ConsoleColor colorBackground) =>
        (Name, DisplayedModel, ColorObject, ColorBackground) = (name, displayedModel, colorObject, colorBackground);

    public string Name { get; set; }
    public string DisplayedModel { get; set; }
    public ConsoleColor ColorObject { get; set; }
    public ConsoleColor ColorBackground { get; set; }
    public PositionModel StartPosition { get; set; }
}