namespace ForestGame.Core.GameObjects;

internal class Obstacle : BaseImmovableGameObject
{
    public Obstacle(ObstacleSettings obstacleSettings) : base(obstacleSettings.StartPosition)
    {
        Name = obstacleSettings.Name;
        Model = obstacleSettings.DisplayedModel;
        ColorObject = obstacleSettings.ColorObject;
        ColorBackground = obstacleSettings.ColorBackground;
    }    
}