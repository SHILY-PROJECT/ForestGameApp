namespace ForestGame.Core.GameObjects.Settings;

internal record PlayerSettings : BaseSettings
{
    public PlayerSettings() { }

    public PlayerSettings(string name, string displayedModel, ConsoleColor colorObject, ConsoleColor colorBackground)
        : base(name, displayedModel, colorObject, colorBackground) { }

}