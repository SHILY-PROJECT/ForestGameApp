using System.Collections.Generic;
using MyForestGame.Core.Models;
using MyForestGame.Core.Interfaces;
using MyForestGame.Core.GameObjects;
using MyForestGame.Core.Interfaces.Services;

namespace MyForestGame.Core.BaseObjects;

public class GameManager : IGameManagerService
{
    public ICollisionHandler CollisionHandler { get; set; }
    public GameCounterModel GameCounter { get; set; }
    public PlayerObject Player { get; set; }
    public List<IGameObject> GameObjectsСollection { get; set; }
    public GameObjectsSettingsModel GameObjectsSettings { get; set; }
    public GameGridSizeModel GridSize { get; set; }
    public bool IsGameOver { get; set; }       

    public void Connect()
    {
        GridSize = new GameGridSizeModel(25, 15);
        CollisionHandler = new CollisionHandler(this);
        Player = new PlayerObject(new MovementModule(CollisionHandler, GridSize), new PositionModel(0, 0), "Player");
    }
}