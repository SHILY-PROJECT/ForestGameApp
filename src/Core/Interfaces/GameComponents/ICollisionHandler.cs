namespace MyForestGame.Core.Interfaces.GameComponents;

public interface ICollisionHandler
{
    bool IsCollision(BaseMovableGameObject dynamicObj, int newWidthPosition, int newHightPosition);
}