using MyForestGame.Core.BaseObjects;

namespace MyForestGame.Core.Interfaces;

public interface ICollisionHandler
{
    bool IsCollision(DynamicGameObjectBase dynamicObj, int newWidthPosition, int newHightPosition);
}