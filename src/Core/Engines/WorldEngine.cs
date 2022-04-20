using System;
using System.Linq;
using System.Collections.Generic;
using MyForestGame.Core.Models;
using MyForestGame.Core.Interfaces;
using MyForestGame.Core.BaseObjects;
using MyForestGame.Core.GameObjects;
using MyForestGame.Core.Engines.WorldEngineScripts;
using MyForestGame.Core.Interfaces.Services;

namespace MyForestGame.Core.Engines;

internal class WorldEngine : IWorldEngineService
{
    private IGameManagerService GameManager { get; set; }
    private EnemyBehaviorHandler EnemyBehaviorHandler { get; set; }
    private Random Rnd { get; } = new();

    private GameGridSizeModel GridSize { get => GameManager.GridSize; }
    private PlayerObject Player { get => GameManager.Player; }
    private List<IGameObject> GameObjectsСollection { get => GameManager.GameObjectsСollection; set => GameManager.GameObjectsСollection = value; }
    private GameObjectsSettingsModel GameObjectsSettings { get => GameManager.GameObjectsSettings; set => GameManager.GameObjectsSettings = value; }

    public WorldEngine(IGameManagerService manager)
    {
        GameManager = manager;
    }

    public void Connect()
    {
        GameManager.GameCounter = new GameCounterModel();
        GameManager.GameObjectsСollection = new List<IGameObject>();
        GameManager.GameObjectsSettings = new GameObjectsSettingsModel()
        {
            PercentageOfPoints = Rnd.Next(6, 8),
            PercentageOfEnemies = Rnd.Next(3, 5),
            PercentageOfObstacle = Rnd.Next(5, 7)
        };
        EnemyBehaviorHandler = new EnemyBehaviorHandler(GameManager);
        GenerateWorld();
    }

    public void UpdateStateWorld()
    {
        GameObjectsСollection.ForEach(x =>
        {
            if (x is EnemyObject obj && obj.IsTimeToTakeStep)
            {
                EnemyBehaviorHandler.Action(obj);
            }
        });
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

        GameManager.GameCounter.VictoryPoints = GameObjectsСollection.Sum(x => x is PointObject p ? p.Points : 0);
    }

    /// <summary>
    /// Добавление объекта 'Игрок' в общую коллекцию игровых объектов.
    /// </summary>
    private void AddGameObject(PlayerObject obj)
        => GameObjectsСollection.Add(obj);

    /// <summary>
    /// Добавление игровых объектов в общую коллекицю игровых объектов.
    /// </summary>
    private void AddGameObject(Type typeGameObject, int numberOfObjects, List<PositionModel> listFreePositions)
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            var indexPosition = Rnd.Next(listFreePositions.Count);

            GameObjectBase obj = typeGameObject switch
            {
                Type type when type == typeof(EnemyObject) => new EnemyObject(new MovementModule(GameManager.CollisionHandler, GridSize), listFreePositions[indexPosition]),
                Type type when type == typeof(PointObject) => new PointObject(listFreePositions[indexPosition]),
                Type type when type == typeof(ObstacleObject) => new ObstacleObject(listFreePositions[indexPosition]),
                _ => throw new Exception($"{typeGameObject.GetType().Name} - Invalid game object")
            };

            listFreePositions.RemoveAt(indexPosition);
            GameObjectsСollection.Add(obj);
        }
    }

    /// <summary>
    /// Расчет количества объектов в зависимости от размеров сетки.
    /// </summary>
    private int CalculateNumberOfObjects(int percentageOfObjects)
        => GridSize.Width * GridSize.Height * percentageOfObjects / 100;
}