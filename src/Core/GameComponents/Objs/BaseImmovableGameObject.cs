namespace ForestGame.Core.GameComponents.Objs;

internal abstract class BaseImmovableGameObject : BaseGameObject
{
    public BaseImmovableGameObject(PositionModel currentPosition)
    {
        SetCurrentPosition(currentPosition);
    }
}