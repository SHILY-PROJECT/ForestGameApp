using Game.Core.Models;
using Game.Core.Interfaces;

namespace Game.Core.BaseObjects
{
    public class MovementModule : IMovementModule
    {
        private IGameManager GameManager { get; set; }
        private DynamicGameObjectBase DynamicGameObject { get; init; }
        private CollisionHandler CollisionHandler { get; set; }

        private GameGridSizeModel GridSize { get => GameManager.GridSize; }
        private PositionModel CurrentPosition { get => DynamicGameObject.CurrentPosition; }

        public MovementModule(IGameManager manager, DynamicGameObjectBase obj)
        {
            GameManager = manager;
            DynamicGameObject = obj;
            DynamicGameObject.PastPosition = new(CurrentPosition);
            CollisionHandler = new(manager, DynamicGameObject);
        }

        public void Up()
        {
            if (CurrentPosition.Height < GridSize.Height && CurrentPosition.Height > 0 &&
                CollisionHandler.IsCollision(CurrentPosition.Width, CurrentPosition.Height - 1) is false)
            {
                CurrentPosition.Height -= 1;
            }
        }

        public void Down()
        {
            if (CurrentPosition.Height < (GridSize.Height - 1) &&
                CollisionHandler.IsCollision(CurrentPosition.Width, CurrentPosition.Height + 1) is false)
            {
                CurrentPosition.Height += 1;
            }
        }

        public void Right()
        {
            if (CurrentPosition.Width < (GridSize.Width - 1) &&
                CollisionHandler.IsCollision(CurrentPosition.Width + 1, CurrentPosition.Height) is false)
            {
                CurrentPosition.Width += 1;
            }
        }

        public void Left()
        {
            if (CurrentPosition.Width < GridSize.Width && CurrentPosition.Width > 0 &&
                CollisionHandler.IsCollision(CurrentPosition.Width - 1, CurrentPosition.Height) is false)
            {
                CurrentPosition.Width -= 1;
            }
        }
    }
}
