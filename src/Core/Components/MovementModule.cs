using MyForestGame.Core.Interfaces;
using MyForestGame.Core.Interfaces;

namespace MyForestGame.Core.BaseObjects
{
    public class MovementModule : IMovementModule
    {
        private ICollisionHandler CollisionHandler { get; set; }
        private GameGridSizeModel GridSize { get; set; }

        public MovementModule(ICollisionHandler collisionHandler, GameGridSizeModel gridSize)
        {
            CollisionHandler = collisionHandler;
            GridSize = gridSize;
        }

        public void Up(DynamicGameObjectBase dynamicObj)
        {
            var currentPosition = dynamicObj.CurrentPosition;

            if (currentPosition.Height < GridSize.Height && currentPosition.Height > 0 &&
                CollisionHandler.IsCollision(dynamicObj, currentPosition.Width, currentPosition.Height - 1) is false)
            {
                currentPosition.Height -= 1;
            }
        }

        public void Down(DynamicGameObjectBase dynamicObj)
        {
            var currentPosition = dynamicObj.CurrentPosition;

            if (currentPosition.Height < (GridSize.Height - 1) &&
                CollisionHandler.IsCollision(dynamicObj, currentPosition.Width, currentPosition.Height + 1) is false)
            {
                currentPosition.Height += 1;
            }
        }

        public void Right(DynamicGameObjectBase dynamicObj)
        {
            var currentPosition = dynamicObj.CurrentPosition;

            if (currentPosition.Width < (GridSize.Width - 1) &&
                CollisionHandler.IsCollision(dynamicObj, currentPosition.Width + 1, currentPosition.Height) is false)
            {
                currentPosition.Width += 1;
            }
        }

        public void Left(DynamicGameObjectBase dynamicObj)
        {
            var currentPosition = dynamicObj.CurrentPosition;

            if (currentPosition.Width < GridSize.Width && currentPosition.Width > 0 &&
                CollisionHandler.IsCollision(dynamicObj, currentPosition.Width - 1, currentPosition.Height) is false)
            {
                currentPosition.Width -= 1;
            }
        }
    }
}
