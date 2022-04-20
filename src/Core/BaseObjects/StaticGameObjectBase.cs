using MyForestGame.Core.Models;

namespace MyForestGame.Core.BaseObjects
{
    public abstract class StaticGameObjectBase : GameObjectBase
    {
        public StaticGameObjectBase(PositionModel currentPosition)
        {
            CurrentPosition = new PositionModel
            {
                Width = currentPosition.Width,
                Height = currentPosition.Height
            };
        }
    }
}
