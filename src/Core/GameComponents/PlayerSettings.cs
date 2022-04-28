namespace ForestGame.Core.GameComponents;

internal class PlayerSettings
{
    public string Name { get; set; }
    public string DisplayedModel { get; set; }
    public ConsoleColor ColorObject { get; set; }
    public ConsoleColor ColorBackground { get; set; }
    public PositionModel StartPosition { get; set; }
}