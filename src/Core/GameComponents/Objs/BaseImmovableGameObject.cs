namespace MyForestGame.Core.GameComponents.Objs;

internal abstract class BaseImmovableGameObject : BaseGameObject
{
    public BaseImmovableGameObject(PositionModel currentPosition)
    {
        SetCurrentPosition(currentPosition);
    }
}