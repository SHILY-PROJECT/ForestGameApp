namespace MyForestGame.Core.Engines;

internal class WorldEngine : IWorldEngine
{
    private readonly IGameManager _manager;
    private readonly Random _rnd = new();

    public WorldEngine(IGameManager gameManager)
    {
        _manager = gameManager;
    }

    private EnemyBehaviorHandler EnemyBehaviorHandler { get; set; }
    private IGameGridSize GridSize { get => _manager.GameGridSize; }
    private PlayerObject Player { get => _manager.Player; }
    private IList<IGameObject> GameObjects { get => _manager.GameObjectsСollection; }
    private IGameBalance GameObjectsSettings { get => _manager.GameObjectsSettings; }
    private IGameCounter GameCounter { get => _manager.GameCounter; }

    public void Connect()
    {
        (_manager.GameObjectsSettings as GameBalance).SetValues(_rnd.Next(6, 8), _rnd.Next(5, 7), _rnd.Next(3, 5));
        EnemyBehaviorHandler = new EnemyBehaviorHandler(_manager);
        GenerateWorld();
    }

    public void UpdateStateWorld()
    {
        for (var index = 0; index < GameObjects.Count; index++)
        {
            if (GameObjects[index] is EnemyObject enemy && enemy.IsTimeToTakeStep)
            {
                EnemyBehaviorHandler.Action(enemy);
            }
        }
    }

    public void GenerateWorld()
    {
        var freePositionsCollection = new List<PositionModel>();

        for (int indexHeight = 0; indexHeight < GridSize.Height; indexHeight++)
        {
            for (int indexWidth = 0; indexWidth < GridSize.Width; indexWidth++)
            {
                var (pWidth, pHeight) = (Player.CurrentPosition.Width, Player.CurrentPosition.Height);

                if ((indexWidth < pWidth + 2 || indexWidth < pWidth - 2) &&
                    (indexHeight < pHeight + 2 || indexHeight < pHeight - 2)) continue;

                freePositionsCollection.Add(new(indexWidth, indexHeight));
            }
        }

        AddGameObject(Player);
        AddGameObject(typeof(PointObject), CalculateNumberOfObjects(GameObjectsSettings.PercentageOfPoints), freePositionsCollection);
        AddGameObject(typeof(EnemyObject), CalculateNumberOfObjects(GameObjectsSettings.PercentageOfEnemies), freePositionsCollection);
        AddGameObject(typeof(ObstacleObject), CalculateNumberOfObjects(GameObjectsSettings.PercentageOfObstacle), freePositionsCollection);

        GameCounter.VictoryPoints = GameObjects.Sum(x => x is PointObject p ? p.Points : 0);
    }

    private void AddGameObject(Type typeGameObject, int numberOfObjects, List<PositionModel> listFreePositions)
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            var indexPosition = _rnd.Next(listFreePositions.Count);

            BaseGameObject obj = typeGameObject switch
            {
                var t when t == typeof(EnemyObject) => new EnemyObject(new MovementModule(GridSize, _manager.CollisionHandler), listFreePositions[indexPosition]),
                var t when t == typeof(PointObject) => new PointObject(listFreePositions[indexPosition]),
                var t when t == typeof(ObstacleObject) => new ObstacleObject(listFreePositions[indexPosition]),
                _ => throw new Exception($"{typeGameObject.GetType().Name} - Invalid game object")
            };

            listFreePositions.RemoveAt(indexPosition);
            GameObjects.Add(obj);
        }
    }

    private void AddGameObject(PlayerObject obj)
    {
        GameObjects.Add(obj);
    }

    private int CalculateNumberOfObjects(int percentageOfObjects)
    { 
        return GridSize.Width* GridSize.Height* percentageOfObjects / 100;
    }

}