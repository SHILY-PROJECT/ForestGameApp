using Game.Core.Models;

namespace Game.Core.BaseObjects
{
    public class StaticGameObjectBase : GameObjectBase
    {
        public StaticGameObjectBase(PositionModel currentPosition)
        {
            CurrentPosition = new(currentPosition);
        }
    }
}
