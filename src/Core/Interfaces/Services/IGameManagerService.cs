using System;
using System.Collections.Generic;
using MyForestGame.Core.Models;
using MyForestGame.Core.BaseObjects;
using MyForestGame.Core.GameObjects;

namespace MyForestGame.Core.Interfaces.Services;

public interface IGameManagerService : IService
{
    ICollisionHandler CollisionHandler { get; set; }
    GameCounterModel GameCounter { get; set; }
    PlayerObject Player { get; set; }
    List<IGameObject> GameObjectsСollection { get; set; }
    GameObjectsSettingsModel GameObjectsSettings { get; set; }
    GameGridSizeModel GridSize { get; set; }

    bool IsGameOver { get; set; }
    public static int CurrentTime => (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
}