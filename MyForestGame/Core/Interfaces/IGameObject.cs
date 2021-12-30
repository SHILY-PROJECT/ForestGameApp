using System;
using Game.Core.Models;

namespace Game.Core.Interfaces
{
    public interface IGameObject
    {
        /// <summary>
        /// Имя объекта.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Визуальная модель объекта.
        /// </summary>
        string Model { get; set; }

        /// <summary>
        /// Цвет объекта (модели).
        /// </summary>
        ConsoleColor ColorBackground { get; set; }

        /// <summary>
        /// Задний фон объекта.
        /// </summary>
        ConsoleColor ColorObject { get; set; }

        /// <summary>
        /// Текущая позиция объекта на игровой сетке.
        /// </summary>
        PositionModel CurrentPosition { get; set; }

        /// <summary>
        /// Видимость объекта.
        /// </summary>
        bool IsVisible { get; set; }
    }
}
