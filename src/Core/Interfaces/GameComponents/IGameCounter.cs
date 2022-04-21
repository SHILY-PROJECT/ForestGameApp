namespace MyForestGame.Core.Interfaces.GameComponents;

internal interface IGameCounter
{
    int CurrentLevel { get; set; }
    int PointsCounter { get; set; }
    int VictoryPoints { get; set; }
    bool PointsIsEqual { get; }
}