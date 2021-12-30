using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Core.Models;
using Game.Core.Interfaces;
using Game.Core.GameObjects;

namespace Game.Core.BaseObjects
{
    public class CollisionHandler
    {
        private IGameManager GameManager { get; set; }
        private DynamicGameObjectBase DynamicGameObject { get; init; }

        private List<GameObjectBase> GameObjectsСollection { get => GameManager.GameObjectsСollection; }
        private GameCounterModel GameCounter { get => GameManager.GameCounter; }

        /// <summary>
        /// Конструктор обработчика столкновений.
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="dynamicGameObject"></param>
        public CollisionHandler(IGameManager manager, DynamicGameObjectBase dynamicGameObject)
        {
            GameManager = manager;
            DynamicGameObject = dynamicGameObject;
        }

        /// <summary>
        /// Проверка столкновения.
        /// </summary>
        /// <returns>True - столкновение с объектом; иначе - False.</returns>
        public bool IsCollision(int width, int hight)
        {
            if (DynamicGameObject is EnemyObject enemyObj)
            {
                if (GameObjectsСollection.FirstOrDefault(x => x is PlayerObject && x.IsCurrentPosition(width, hight)) is PlayerObject)
                {
                    GameEnd(false);
                }
                else if (GameObjectsСollection.FirstOrDefault(x => !ReferenceEquals(x, DynamicGameObject) && x.IsCurrentPosition(width, hight)) is not null)
                {
                    return true;
                }
            }
            else if (DynamicGameObject is PlayerObject playerObj)
            {
                if (GameObjectsСollection.FirstOrDefault(x => x is EnemyObject && x.IsCurrentPosition(width, hight)) is EnemyObject)
                {
                    GameEnd(false);
                }
                else if (GameObjectsСollection.FirstOrDefault(x => x is PointObject && x.IsCurrentPosition(width, hight)) is PointObject obj)
                {
                    GameCounter.PointsCounter += obj.Points;
                    if (GameCounter.PointsIsEqual) GameEnd(true);
                    obj.IsVisible = false;
                }
                else if (GameObjectsСollection.FirstOrDefault(x => x is ObstacleObject && x.IsCurrentPosition(width, hight)) is ObstacleObject)
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
