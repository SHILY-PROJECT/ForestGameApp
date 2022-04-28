namespace ForestGame.Core.GameObjects.Settings;

internal record ObstacleSettings : BaseSettings
{
    public ObstacleSettings(string name, string displayedModel, ConsoleColor colorObject, ConsoleColor colorBackground)
        : base(name, displayedModel, colorObject, colorBackground) { }

}
