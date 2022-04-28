namespace ForestGame.Core.GameComponents;

internal class MovementModule : IMovementModule
{
    private readonly ICollisionHandler _collisionHandler;
    private readonly IGameGridSize _gridSize;

    public MovementModule(IGameGridSize gameGridSize, ICollisionHandler collisionHandler)
    {
        _collisionHandler = collisionHandler;
        _gridSize = gameGridSize;
    }

    public void Up(BaseMovableGameObject movableObj)
    {
        var currentPosition = movableObj.CurrentPosition;

        if (currentPosition.Height < _gridSize.Height && currentPosition.Height > 0 &&
            _collisionHandler.IsCollision(movableObj, currentPosition.Width, currentPosition.Height - 1) is false)
        {
            currentPosition.Height -= 1;
        }
    }

    public void Down(BaseMovableGameObject movableObj)
    {
        var currentPosition = movableObj.CurrentPosition;

        if (currentPosition.Height < _gridSize.Height - 1 &&
            _collisionHandler.IsCollision(movableObj, currentPosition.Width, currentPosition.Height + 1) is false)
        {
            currentPosition.Height += 1;
        }
    }

    public void Right(BaseMovableGameObject movableObj)
    {
        var currentPosition = movableObj.CurrentPosition;

        if (currentPosition.Width < _gridSize.Width - 1 &&
            _collisionHandler.IsCollision(movableObj, currentPosition.Width + 1, currentPosition.Height) is false)
        {
            currentPosition.Width += 1;
        }
    }

    public void Left(BaseMovableGameObject movableObj)
    {
        var currentPosition = movableObj.CurrentPosition;

        if (currentPosition.Width < _gridSize.Width && currentPosition.Width > 0 &&
            _collisionHandler.IsCollision(movableObj, currentPosition.Width - 1, currentPosition.Height) is false)
        {
            currentPosition.Width -= 1;
        }
    }

}