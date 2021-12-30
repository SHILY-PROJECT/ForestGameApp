using System;
using System.Text;
using System.Threading.Tasks;
using Game.Core.Interfaces;

namespace MyForestGame.Core
{
    public class MainGame
    {
        private IGameManager GameManager { get; init; }

        private IWorldEngine WorldEngine { get => GameManager.WorldEngine; }
        private IRenderEngine RenderEngine { get => GameManager.RenderEngine; }

        public MainGame(IGameManager manager)
        {
            SetConsoleSettings();
            GameManager = manager;
        }

        /// <summary>
        /// Старт игры.
        /// </summary>
        public void Start()
        {
            RenderEngine.RenderMap();
            RenderEngine.RenderAllGameObjects();

            Parallel.Invoke(
                () => { UpdatableHandlerPlayerControl(); },
                () => { UpdatableViewWorld(); });
        }

        /// <summary>
        /// Обновление состояния объектов и их отрисовка.
        /// </summary>
        private void UpdatableViewWorld()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    WorldEngine.UpdateStateWorld();
                    RenderEngine.UpdateRenderDynamicGameObjects();

                    if (GameManager.IsGameOver) return;

                    Task.Delay(10).Wait();
                }
            });
        }

        /// <summary>
        /// Обработка ввода.
        /// </summary>
        private void UpdatableHandlerPlayerControl()
        {
            while (true)
            {
                var key = Console.ReadKey(true).Key;

                if (GameManager.IsGameOver) return;

                if (key == ConsoleKey.W || key == ConsoleKey.UpArrow) GameManager.Player.Move.Up();
                if (key == ConsoleKey.S || key == ConsoleKey.DownArrow) GameManager.Player.Move.Down();
                if (key == ConsoleKey.A || key == ConsoleKey.LeftArrow) GameManager.Player.Move.Left();
                if (key == ConsoleKey.D || key == ConsoleKey.RightArrow) GameManager.Player.Move.Right();

                if (key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    Console.WriteLine("Game exited");
                    Environment.Exit(0);
                }
            }
        }

        /// <summary>
        /// Установка дефолтных настроек консоли.
        /// </summary>
        private static void SetConsoleSettings()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Console.SetWindowSize(120, 40);
            Console.CursorVisible = false;
        }
    }
}
