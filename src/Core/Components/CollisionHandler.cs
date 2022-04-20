using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyForestGame.Core.Models;
using MyForestGame.Core.Interfaces;
using MyForestGame.Core.GameObjects;
using MyForestGame.Core.Interfaces.Services;

namespace MyForestGame.Core.BaseObjects
{
    public class CollisionHandler : ICollisionHandler
    {
        private IGameManagerService GameManager { get; }       
        private GameCounterModel GameCounter { get => GameManager.GameCounter; }
        private List<IGameObject> ObjСollection { get => GameManager.GameObjectsСollection; }

        /// <summary>
        /// Конструктор обработчика столкновений.
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="dynamicGameObject"></param>
        public CollisionHandler(IGameManagerService manager)
        {
            GameManager = manager;
        }

        /// <summary>
        /// Проверка столкновения.
        /// </summary>
        /// <returns>True - столкновение с объектом; иначе - False.</returns>
        public bool IsCollision(DynamicGameObjectBase dynamicObj, int newWidthPosition, int newHightPosition)
        {
            if (dynamicObj is EnemyObject enemyObj)
            {
                if (ObjСollection.FirstOrDefault(x => x is PlayerObject && x.IsCurrentPosition(newWidthPosition, newHightPosition)) is PlayerObject)
                {
                    GameEnd(false);
                }
                else if (ObjСollection.FirstOrDefault(x => !ReferenceEquals(x, dynamicObj) && x.IsCurrentPosition(newWidthPosition, newHightPosition)) is not null)
                {
                    return true;
                }
            }
            else if (dynamicObj is PlayerObject playerObj)
            {
                if (ObjСollection.FirstOrDefault(x => x is EnemyObject && x.IsCurrentPosition(newWidthPosition, newHightPosition)) is EnemyObject)
                {
                    GameEnd(false);
                }
                else if (ObjСollection.FirstOrDefault(x => x is PointObject && x.IsCurrentPosition(newWidthPosition, newHightPosition)) is PointObject obj)
                {
                    GameCounter.PointsCounter += obj.Points;
                    if (GameCounter.PointsIsEqual) GameEnd(true);
                    obj.IsVisible = false;
                }
                else if (ObjСollection.FirstOrDefault(x => x is ObstacleObject && x.IsCurrentPosition(newWidthPosition, newHightPosition)) is ObstacleObject)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Завершение игры.
        /// </summary>
        /// <param name="goodOrBad"></param>
        /// <returns></returns>
        private Task GameEnd(bool goodOrBad)
        {
            GameManager.IsGameOver = true;
            Task.Delay(100).Wait();
            Console.Clear();

            if (goodOrBad)
            {
                Print("< GOOD GAME >", ConsoleColor.DarkGreen);
            }
            else
            {
                Print("< GAME OVER >", ConsoleColor.DarkRed);
            }

            Print($"LEVEL REACHED: {GameCounter.CurrentLevel}", ConsoleColor.DarkBlue);
            Print($"POINTS EARNED: {GameCounter.PointsCounter}", ConsoleColor.DarkBlue);

            while (true)
            {
                Console.WriteLine($"\nPress to exit 'Escape' or 'Enter'...");
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Escape || key == ConsoleKey.Enter) Environment.Exit(0);
            }
        }

        /// <summary>
        /// Отрисовка объектов.
        /// </summary>
        private static void Print(string text, ConsoleColor color)
        {
            Console.BackgroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();           
        }
    }
}
