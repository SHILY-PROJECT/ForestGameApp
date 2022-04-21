namespace MyForestGame.Core.Interfaces.Player;

internal interface IPlayerSettings
{
    string Name { get; }
    string DisplayedModel { get; }
    ConsoleColor ColorObject { get; }
    ConsoleColor ColorBackground { get; }
    PositionModel StartPosition { get; }
}