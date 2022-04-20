using MyForestGame.Core.Models;
using MyForestGame.Core.Interfaces;
using MyForestGame.Core.Interfaces;

namespace MyForestGame.Core.BaseObjects
{
    public abstract class DynamicGameObjectBase : GameObjectBase, IMoveController
    {
        public IMovementModule MovementModule { get; set; }
        public ICollisionHandler CollisionHandler { get; set; }
        public PositionModel PastPosition { get; set; }

        public DynamicGameObjectBase(IMovementModule movementModule, PositionModel position)
        {
            MovementModule = movementModule;

            CurrentPosition = new PositionModel(position.Width, position.Height);
            PastPosition = new PositionModel(position.Width, position.Height);
        }

        public void SetMoveModule(IMovementModule movementModule)
            => MovementModule = movementModule;

        public bool CurrentAndPastPositionIsEqual
            => CurrentPosition.Width == PastPosition.Width && CurrentPosition.Height == PastPosition.Height;

        public void SetPastPosition(int width, int hight)
        {
            if (PastPosition != null)
            {
                PastPosition.Width = width;
                PastPosition.Height = hight;
            }
            else PastPosition = new PositionModel(width, hight);
        }

        public void SetPastPosition(PositionModel position)
            => SetPastPosition(position.Width, position.Height);

        public void MoveUp()
            => MovementModule.Up(this);

        public void MoveDown()
            => MovementModule.Down(this);

        public void MoveRight()
            => MovementModule.Right(this);

        public void MoveLeft()
            => MovementModule.Left(this);
        
    }
}
