namespace ForestGame.Core.Engines;

internal class WorldEngine : IWorldEngine
{
    private static readonly Random _rnd = new();

    private readonly EnemySettings[] _enemies =
    {
        new("Wolve", ">-<", ConsoleColor.White, ConsoleColor.DarkRed, _rnd.Next(200, 400)),
        new("Bear", "<^>", ConsoleColor.White, ConsoleColor.DarkRed, _rnd.Next(400, 600))
    };

    private readonly ObstacleSettings[] _obstacles =
    {
        new("Stone", "   ", ConsoleColor.White, ConsoleColor.DarkMagenta),
        new("Tree", "   ", ConsoleColor.White, ConsoleColor.DarkMagenta)
    };

    private readonly PointSettings[] _points =
    {
        new("Apples", "-$-", ConsoleColor.White, ConsoleColor.DarkGreen, 1),
        new("Cherries", "-$-", ConsoleColor.White, ConsoleColor.DarkGreen, 1),
        new("Bananas", "-$-", ConsoleColor.White, ConsoleColor.DarkGreen, 1)
    };

    private readonly IGameManager _manager;

    public WorldEngine(IGameManager gameManager)
    {
        _manager = gameManager;
    }

    private EnemyBehaviorHandler EnemyBehaviorHandler { get; set; }
    private IGameGridSize GridSize { get => _manager.GameGridSize; }
    private Player Player { get => _manager.Player; }
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
            if (GameObjects[index] is Enemy enemy && enemy.IsTimeToTakeStep)
            {
                EnemyBehaviorHandler.Action(enemy);
            }
        }
    }

    private void GenerateWorld()
    {
        var freePositions = new List<PositionModel>();

        for (int heightIndex = 0; heightIndex < GridSize.Height; heightIndex++)
        {
            for (int widthIndex = 0; widthIndex < GridSize.Width; widthIndex++)
            {
                if (!Player.IsCoordinatesInPlayerPositionOrAbout(widthIndex, heightIndex))
                {
                    freePositions.Add(new(widthIndex, heightIndex));
                }
            }
        }

        AddGameObject(Player);
        AddGameObject(typeof(Point), CalculateNumberOfObjects(GameObjectsSettings.PercentageOfPoints), freePositions);
        AddGameObject(typeof(Enemy), CalculateNumberOfObjects(GameObjectsSettings.PercentageOfEnemies), freePositions);
        AddGameObject(typeof(Obstacle), CalculateNumberOfObjects(GameObjectsSettings.PercentageOfObstacle), freePositions);

        GameCounter.VictoryPoints = GameObjects.Sum(go => go is Point point ? point.Points : 0);
    }

    private void AddGameObject(Player obj)
    {
        GameObjects.Add(obj);
    }

    private void AddGameObject(Type typeGameObject, int numberOfObjects, IList<PositionModel> listFreePositions)
    {
        int rndPositionIndex;

        (GameObjects as List<IGameObject>).AddRange(Enumerable.Range(0, numberOfObjects).Select(o =>
        {
            var position = listFreePositions[rndPositionIndex = _rnd.Next(listFreePositions.Count)];
            listFreePositions.RemoveAt(rndPositionIndex);
            return CreateGameObject(typeGameObject, position);
        }));
    }

    private IGameObject CreateGameObject(Type type, PositionModel position) => type switch
    {
        _ when type == typeof(Enemy)      => new Enemy(_enemies[_rnd.Next(_enemies.Length)] with { StartPosition = position }, new MovementModule(GridSize, _manager.CollisionHandler)),
        _ when type == typeof(Point)      => new Point(_points[_rnd.Next(_points.Length)] with { StartPosition = position }),
        _ when type == typeof(Obstacle)   => new Obstacle(_obstacles[_rnd.Next(_obstacles.Length)] with { StartPosition = position }),
        _                                       => throw new ArgumentException($"Invalid type game object.")
    };

    private int CalculateNumberOfObjects(int percentageOfObjects)
    { 
        return GridSize.Width * GridSize.Height * percentageOfObjects / 100;
    }
}