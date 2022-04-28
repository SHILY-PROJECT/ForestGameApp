namespace ForestGame.Core.GameObjects;

internal abstract class BaseImmovableGameObject : BaseGameObject
{
    public BaseImmovableGameObject(PositionModel currentPosition)
    {
        SetCurrentPosition(currentPosition);
    }
}