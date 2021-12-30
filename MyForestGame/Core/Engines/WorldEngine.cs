using System;
using System.Linq;
using System.Collections.Generic;
using Game.Core.Models;
using Game.Core.Interfaces;
using Game.Core.BaseObjects;
using Game.Core.GameObjects;
using Game.Core.Engines.WorldEngineScripts;

namespace Game.Core.Engines
{
    public class WorldEngine : IWorldEngine
    {
        private IGameManager GameManager { get; set; }
        private EnemyBehaviorHandler EnemyBehaviorHandler { get; set; }
        private Random Rnd { get; } = new();

        private GameGridSizeModel GridSize { get => GameManager.GridSize; }
        private PlayerObject Player { get => GameManager.Player; }
        private List<GameObjectBase> GameObjectsСollection { get => GameManager.GameObjectsСollection; set => GameManager.GameObjectsСollection = value; }
        private GameObjectsSettingsModel GameObjectsSettings { get => GameManager.GameObjectsSettings; set => GameManager.GameObjectsSettings = value; }

        /// <summary>
        /// Конструктор движка игрового мира.
        /// </summary>
        /// <param name="manager"></param>
        public WorldEngine(IGameManager manager)
        {
            GameManager = manager;
            GameManager.GameCounter = new();
            GameManager.GameObjectsСollection = new();
            GameManager.GameObjectsSettings = new GameObjectsSettingsModel()
            {
                PercentageOfPoints = Rnd.Next(6, 8),
                PercentageOfEnemies = Rnd.Next(3, 5),
                PercentageOfObstacle = Rnd.Next(5, 7)
            };
            EnemyBehaviorHandler = new(manager);
            GenerateWorld();
        }

        /// <summary>
        /// Обновления состояния мира.
        /// </summary>
        public void UpdateStateWorld()
        {
            GameObjectsСollection.ForEach(x =>
            {
                if (x is EnemyObject obj && obj.IsTimeToTakeStep)
                    EnemyBehaviorHandler.Action(obj);
            });
        }

        /// <summary>
        /// Генерация мира.
        /// </summary>
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
        {
            SetMoveModuleForDynamicGameObject(obj);
            GameObjectsСollection.Add(obj);
        }

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
                    Type type when type == typeof(EnemyObject) => new EnemyObject(listFreePositions[indexPosition]),
                    Type type when type == typeof(PointObject) => new PointObject(listFreePositions[indexPosition]),
                    Type type when type == typeof(ObstacleObject) => new ObstacleObject(listFreePositions[indexPosition]),
                    _ => throw new Exception($"{typeGameObject.GetType().Name} - Invalid game object")
                };

                SetMoveModuleForDynamicGameObject(obj);
                listFreePositions.RemoveAt(indexPosition);
                GameObjectsСollection.Add(obj);
            }
        }

        /// <summary>
        /// Установка модуля движения динамическим объектам.
        /// </summary>
        private void SetMoveModuleForDynamicGameObject(GameObjectBase obj)
        {
            if (obj is DynamicGameObjectBase dynamicObj)
                dynamicObj.SetMoveModule(new MovementModule(GameManager, dynamicObj));
        }

        /// <summary>
        /// Расчет количества объектов в зависимости от размеров сетки.
        /// </summary>
        private int CalculateNumberOfObjects(int percentageOfObjects)
            => GridSize.Width * GridSize.Height * percentageOfObjects / 100;
    }
}
