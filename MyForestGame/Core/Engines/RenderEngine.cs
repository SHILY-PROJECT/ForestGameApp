using System;
using System.Text;
using System.Linq;
using MyForestGame.Core.BaseObjects;
using MyForestGame.Core.Interfaces;
using MyForestGame.Core.Models;

namespace MyForestGame.Core.Engines
{
    public class RenderEngine : IRenderEngine
    {
        private readonly int _counterWidth;
        private readonly int _counterHight;

        private int _pastNumberLevel = 0;
        private int _pastNumberPoints = 0;

        /// <summary>
        /// Игровой менеджер.
        /// </summary>
        private IGameManager GameManager { get; init; }


        /// <summary>
        /// Игровая сетка.
        /// </summary>
        private GameGridSizeModel GridSize { get => GameManager.GridSize; }

        /// <summary>
        /// Игровой счетчик.
        /// </summary>
        private GameCounterModel GameCounter { get => GameManager.GameCounter; }

        /// <summary>
        /// Конструктор движка отрисовки объектов игры.
        /// </summary>
        /// <param name="manager"></param>
        public RenderEngine(IGameManager manager)
        {
            GameManager = manager;

            _counterWidth = (GridSize.Width * 4 + 1) - 33;
            _counterHight = (GridSize.Height * 2);
        }

        /// <summary>
        /// Обновление отрисовки динамических объектов.
        /// </summary>
        public void UpdateRenderDynamicGameObjects()
        {
            foreach (var obj in GameManager.GameObjectsСollection.Where(obj => obj is DynamicGameObjectBase && obj.IsVisible).Select(x => (DynamicGameObjectBase)x))
            {
                if (obj.CurrentAndPastPositionIsEqual is false)
                {
                    Print(obj);
                    obj.PastPosition = new(obj.CurrentPosition);
                }
            }
            UpdatePointsCounter();
        }

        /// <summary>
        /// Отрисовка всех объектов игры (статические, динамические и счетчик).
        /// </summary>
        public void RenderAllGameObjects()
        {
            GameManager.GameObjectsСollection.ForEach(obj => Print(obj));
            RenderPointsCounter();
        }

        /// <summary>
        /// Отрисовка игровой сетки.
        /// </summary>
        public void RenderMap()
        {
            var buffer = new StringBuilder();

            var sizeWidth = (GridSize.Width + 1);
            var sizeHeight = (GridSize.Height * 2 + 1);

            for (int indexHeight = 0; indexHeight < sizeHeight; indexHeight++)
            {
                for (int indexWidth = 0; indexWidth < sizeWidth; indexWidth++)
                {
                    if (indexHeight == 0)
                    {
                        AppendToBuffer(buffer, ("╔═══", "╤═══", "╗"), (indexWidth, sizeWidth));
                    }
                    else if ((indexHeight + 1) == sizeHeight)
                    {
                        AppendToBuffer(buffer, ("╚═══", "╧═══", "╝"), (indexWidth, sizeWidth));
                    }
                    else if ((indexHeight + 1) % 2 == 0)
                    {
                        AppendToBuffer(buffer, ("║   ", "│   ", "║"), (indexWidth, sizeWidth));
                    }
                    else
                    {
                        AppendToBuffer(buffer, ("╟───", "┼───", "╢"), (indexWidth, sizeWidth));
                    }
                }
                buffer.AppendLine();
            }

            Console.WriteLine(buffer);
        }

        /// <summary>
        /// Добавление элементов игровой сетки в буфер.
        /// </summary>
        private static void AppendToBuffer(StringBuilder buffer, (string first, string middle, string last) obj, (int index, int size) width)
        {
            if (width.index == 0)
            {
                buffer.Append(obj.first);
            }
            else if ((width.index + 1) == width.size)
            {
                buffer.Append(obj.last);
            }
            else buffer.Append(obj.middle);
        }

        /// <summary>
        /// Отрисовка игрового объекта.
        /// </summary>
        /// <param name="obj"></param>
        private static void Print(IGameObject obj)
        {
            int left, top;

            if (obj is DynamicGameObjectBase dObj)
            {
                left = dObj.PastPosition.Width * 4 + 1;
                top = dObj.PastPosition.Height * 2 + 1;

                Print("   ", left, top, default, default);
            }

            left = obj.CurrentPosition.Width * 4 + 1;
            top = obj.CurrentPosition.Height * 2 + 1;

            Print(obj.Model, left, top, obj.ColorObject, obj.ColorBackground);
        }

        /// <summary>
        /// Отрисовка объекта.
        /// </summary>
        private static void Print(string text, int left, int top, ConsoleColor colorObject, ConsoleColor colorBackground)
        {
            Console.SetCursorPosition(left, top);
            Console.ForegroundColor = colorObject;
            Console.BackgroundColor = colorBackground;
            Console.Write(text);
            Console.ResetColor();
            Console.CursorVisible = false;
        }

        /// <summary>
        /// Отрисовка макета счетчика игры.
        /// </summary>
        private void RenderPointsCounter()
        {
            Print("║  POINTS: 0    │  LEVEL: 0     ║", _counterWidth, _counterHight + 1, ConsoleColor.White, default);
            Print("╚═══════════════╧═══════════════╝", _counterWidth, _counterHight + 2, ConsoleColor.White, default);

            UpdatePointsCounter();
        }

        /// <summary>
        /// Обновление счетчика игры.
        /// </summary>
        private void UpdatePointsCounter()
        {
            if (GameCounter.PointsCounter != _pastNumberPoints)
            {
                _pastNumberPoints = GameCounter.PointsCounter;
                Print(GameCounter.PointsCounter.ToString(), _counterWidth + 11, _counterHight + 1, ConsoleColor.White, default);
            }
            if (GameCounter.CurrentLevel != _pastNumberLevel)
            {
                _pastNumberLevel = GameCounter.CurrentLevel;
                Print(GameCounter.CurrentLevel.ToString(), _counterWidth + 26, _counterHight + 1, ConsoleColor.White, default);
            }
        }

    }
}
