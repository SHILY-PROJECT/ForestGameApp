using System;
using System.Text;
using System.Threading.Tasks;
using MyForestGame.Core.Interfaces;

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
                () => UpdatableHandlerPlayerControl(),
                () => UpdatableViewWorld());
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
                if (GameManager.IsGameOver) return;

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W or ConsoleKey.UpArrow:
                        GameManager.Player.MoveUp();
                        break;

                    case ConsoleKey.S or ConsoleKey.DownArrow:
                        GameManager.Player.MoveDown();
                        break;

                    case ConsoleKey.A or ConsoleKey.LeftArrow:
                        GameManager.Player.MoveLeft();
                        break;

                    case ConsoleKey.D or ConsoleKey.RightArrow:
                        GameManager.Player.MoveRight();
                        break;

                    case ConsoleKey.Escape:
                        Console.Clear();
                        Console.WriteLine("Game exited");
                        Environment.Exit(0);
                        break;
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
