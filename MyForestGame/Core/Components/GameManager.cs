using System.Collections.Generic;
using Game.Core.Models;
using Game.Core.Interfaces;
using Game.Core.Engines;
using Game.Core.GameObjects;

namespace Game.Core.BaseObjects
{
    public class GameManager : IGameManager
    {
        public IWorldEngine WorldEngine { get; init; }
        public IRenderEngine RenderEngine { get; init; }
        public GameCounterModel GameCounter { get; set; }
        public PlayerObject Player { get; set; }
        public List<GameObjectBase> GameObjectsСollection { get; set; }
        public GameObjectsSettingsModel GameObjectsSettings { get; set; }
        public GameGridSizeModel GridSize { get; set; }
        public bool IsGameOver { get; set; }       

        public GameManager()
        {
            Player = new("Player", new PositionModel(0, 0));
            GridSize = new(25, 15);
            WorldEngine = new WorldEngine(this);
            RenderEngine = new RenderEngine(this);
        }
    }
}
