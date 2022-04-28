namespace ForestGame.Core.GameComponents.Models;

internal class GameBalance : IGameBalance
{
    public int PercentageOfPoints { get; private set; }
    public int PercentageOfObstacle { get; private set; }
    public int PercentageOfEnemies { get; private set; }

    public void SetValues(int percentageOfPoints, int percentageOfObstacle, int percentageOfEnemies)
    {
        PercentageOfPoints = percentageOfPoints;
        PercentageOfObstacle = percentageOfObstacle;
        PercentageOfEnemies = percentageOfEnemies;
    }
}