namespace ForestGame.Core.GameComponents.Models;

internal class GameCounter : IGameCounter
{
    public int CurrentLevel { get; set; } = 1;
    public int PointsCounter { get; set; } = 0;
    public int VictoryPoints { get; set; } = 0;
    public bool PointsIsEqual { get => PointsCounter == VictoryPoints; }
}