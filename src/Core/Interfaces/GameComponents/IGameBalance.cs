namespace MyForestGame.Core.Interfaces.GameComponents;

internal interface IGameBalance
{
    int PercentageOfPoints { get; }
    int PercentageOfObstacle { get; }
    int PercentageOfEnemies { get; }
}