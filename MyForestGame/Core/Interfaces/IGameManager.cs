using System;
using System.Collections.Generic;
using Game.Core.Models;
using Game.Core.BaseObjects;
using Game.Core.GameObjects;

namespace Game.Core.Interfaces
{
    public interface IGameManager
    {
        /// <summary>
        /// Движок мира (создание и обновление игровых объектов).
        /// </summary>
        IWorldEngine WorldEngine { get; init; }

        /// <summary>
        /// Движок отрисовки (отрисовка игровых компонентов и объектов)
        /// </summary>
        IRenderEngine RenderEngine { get; init; }

        /// <summary>
        /// Игровой счетчик (очки, уровни).
        /// </summary>
        GameCounterModel GameCounter { get; set; }

        /// <summary>
        /// Объект игрока.
        /// </summary>
        PlayerObject Player { get; set; }

        /// <summary>
        /// Коллекция всех игровых объектов (динамические и статические объекты).
        /// </summary>
        List<GameObjectBase> GameObjectsСollection { get; set; }

        /// <summary>
        /// Настройки объектов мира (количество объектов на карте в процентах).
        /// </summary>
        GameObjectsSettingsModel GameObjectsSettings { get; set; }

        /// <summary>
        /// Размер игровой области.
        /// </summary>
        GameGridSizeModel GridSize { get; set; }

        /// <summary>
        /// Флаг окончания игры.
        /// </summary>
        bool IsGameOver { get; set; }

        /// <summary>
        /// Текущее время (в UnixTime).
        /// </summary>
        public static int CurrentTime => (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
    }
}
