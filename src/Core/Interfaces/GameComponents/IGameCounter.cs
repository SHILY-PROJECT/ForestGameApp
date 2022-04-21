namespace MyForestGame.Core.Interfaces.GameComponents;

public interface IGameCounter
{
    int CurrentLevel { get; set; }
    int PointsCounter { get; set; }
    int VictoryPoints { get; set; }
    bool PointsIsEqual { get; }
}