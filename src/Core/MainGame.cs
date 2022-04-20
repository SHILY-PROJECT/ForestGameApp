using System;
using System.Text;
using System.Threading.Tasks;
using MyForestGame.Core.Interfaces;

namespace MyForestGame.Core;

public class MainGame
{
    private IGameManager _gameManager { get; init; }

    private IWorldEngine _worldEngine { get => _gameManager.WorldEngine; }
    private IRenderEngine _renderEngine { get => _gameManager.RenderEngine; }

    public MainGame(IGameManager manager)
    {
        SetConsoleSettings();
        _gameManager = manager;
    }

    /// <summary>
    /// Старт игры.
    /// </summary>
    public void Start()
    {
        _renderEngine.RenderMap();
        _renderEngine.RenderAllGameObjects();

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
                _worldEngine.UpdateStateWorld();
                _renderEngine.UpdateRenderDynamicGameObjects();

                if (_gameManager.IsGameOver) return;

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
            if (_gameManager.IsGameOver) return;

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.W or ConsoleKey.UpArrow:
                    _gameManager.Player.MoveUp();
                    break;

                case ConsoleKey.S or ConsoleKey.DownArrow:
                    _gameManager.Player.MoveDown();
                    break;

                case ConsoleKey.A or ConsoleKey.LeftArrow:
                    _gameManager.Player.MoveLeft();
                    break;

                case ConsoleKey.D or ConsoleKey.RightArrow:
                    _gameManager.Player.MoveRight();
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