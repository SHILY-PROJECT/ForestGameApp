namespace MyForestGame.Core.Interfaces.GameComponents;

public interface IGameBalance
{
    int PercentageOfPoints { get; }
    int PercentageOfObstacle { get; }
    int PercentageOfEnemies { get; }
}