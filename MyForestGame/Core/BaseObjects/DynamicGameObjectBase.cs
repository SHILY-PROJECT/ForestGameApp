using Game.Core.Models;
using Game.Core.Interfaces;

namespace Game.Core.BaseObjects
{
    public class DynamicGameObjectBase : GameObjectBase
    {
        public IMovementModule Move { get; set; }
        public PositionModel PastPosition { get; set; }

        public DynamicGameObjectBase(PositionModel currentPosition)
        {
            CurrentPosition = new(currentPosition);
            PastPosition = new(currentPosition);
        }

        public void SetMoveModule(MovementModule movementModule)
            => Move = movementModule;

        public bool CurrentAndPastPositionIsEqual
            => CurrentPosition.Width == PastPosition.Width && CurrentPosition.Height == PastPosition.Height;

        public void SetPastPosition(int width, int hight)
            => PastPosition = new(width, hight);

        public void SetPastPosition(PositionModel position)
            => SetPastPosition(position.Width, position.Height);

    }
}
