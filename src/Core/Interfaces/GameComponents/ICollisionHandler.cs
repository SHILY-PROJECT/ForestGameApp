namespace ForestGame.Core.Interfaces.GameComponents;

internal interface ICollisionHandler
{
    bool IsCollision(BaseMovableGameObject dynamicObj, int newWidthPosition, int newHightPosition);
}