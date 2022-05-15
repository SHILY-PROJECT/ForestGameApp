namespace ForestGame.Core.GameComponents;

internal class CollisionHandler : ICollisionHandler
{
    private readonly IGameManager _manager;

    public CollisionHandler(IGameManager gameManager)
    {
        _manager = gameManager;
    }

    private IGameCounter GameCounter { get => _manager.GameCounter; }
    private IList<IGameObject> GameObjects { get => _manager.GameObjectsСollection; }

    public bool IsCollision(BaseMovableGameObject movableObject, int newWidthPosition, int newHightPosition)
    {
        if (movableObject is Enemy enemyObj)
        {
            if (GameObjects.FirstOrDefault(x => x is Player && x.IsCurrentPosition(newWidthPosition, newHightPosition)) is Player)
            {
                throw new OperationCanceledException("bad");
            }
            else if (GameObjects.FirstOrDefault(x => !ReferenceEquals(x, movableObject) && x.IsCurrentPosition(newWidthPosition, newHightPosition)) is not null)
            {
                return true;
            }
        }
        else if (movableObject is Player playerObj)
        {
            if (GameObjects.FirstOrDefault(x => x is Enemy && x.IsCurrentPosition(newWidthPosition, newHightPosition)) is Enemy)
            {
                throw new OperationCanceledException("bad");
            }
            else if (GameObjects.FirstOrDefault(x => x is Point && x.IsCurrentPosition(newWidthPosition, newHightPosition)) is Point obj)
            {
                GameCounter.PointsCounter += obj.Points;
                if (GameCounter.PointsIsEqual) throw new OperationCanceledException("good");
                obj.IsVisible = false;
            }
            else if (GameObjects.FirstOrDefault(x => x is Obstacle && x.IsCurrentPosition(newWidthPosition, newHightPosition)) is Obstacle)
            {
                return true;
            }
        }
        return false;
    }
}