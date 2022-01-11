using System.Collections.Generic;
using MyForestGame.Core.Models;
using MyForestGame.Core.Interfaces;
using MyForestGame.Core.Engines;
using MyForestGame.Core.GameObjects;
using MyForestGame.Core.Interfaces;

namespace MyForestGame.Core.BaseObjects
{
    public class GameManager : IGameManager
    {
        public IWorldEngine WorldEngine { get; init; }
        public IRenderEngine RenderEngine { get; init; }
        public ICollisionHandler CollisionHandler { get; init; }
        public GameCounterModel GameCounter { get; set; }
        public PlayerObject Player { get; set; }
        public List<IGameObject> GameObjectsСollection { get; set; }
        public GameObjectsSettingsModel GameObjectsSettings { get; set; }
        public GameGridSizeModel GridSize { get; set; }
        public bool IsGameOver { get; set; }       

        public GameManager()
        {
            GridSize = new GameGridSizeModel(25, 15);
            CollisionHandler = new CollisionHandler(this);
            Player = new PlayerObject(new MovementModule(CollisionHandler, GridSize), new PositionModel(0, 0), "Player");
            WorldEngine = new WorldEngine(this);
            RenderEngine = new RenderEngine(this);
        }
    }
}
