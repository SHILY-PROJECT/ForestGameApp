using System;
using System.Threading.Tasks;
using MyForestGame.Core.Interfaces.Services;

namespace MyForestGame.Core.Components;

public class PlayerControl : IPlayerControlService
{
    public PlayerControl(IGameManagerService gameManager)
    {
        GameManager = gameManager;
    }

    private IGameManagerService GameManager { get; set; }

    public void Connect()
    {
        Parallel.Invoke(() => UpdatePlayerControl());
    }

    private void UpdatePlayerControl()
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
}