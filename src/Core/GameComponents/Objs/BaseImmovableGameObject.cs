namespace MyForestGame.Core.GameComponents.Objs;

public abstract class BaseImmovableGameObject : BaseGameObject
{
    public BaseImmovableGameObject(PositionModel currentPosition)
    {
        SetCurrentPosition(currentPosition);
    }
}