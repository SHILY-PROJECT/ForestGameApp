namespace ForestGame.Core.GameComponents;

internal class PlayerControl : IPlayerControl
{
    private readonly IGameManager _manager;

    public PlayerControl(IGameManager gameManager)
    {
        _manager = gameManager;
    }

    private PlayerObject Player { get => _manager.Player; }

    public void Connect()
    {
        Player.SetMoveModule(new MovementModule(_manager.GameGridSize, _manager.CollisionHandler));
        UpdatePlayerControl();
    }

    private void UpdatePlayerControl()
    {
        while (true)
        {
            if (_manager.IsGameOver) return;

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.W or ConsoleKey.UpArrow:
                    _manager.Player.MoveUp();
                    break;

                case ConsoleKey.S or ConsoleKey.DownArrow:
                    _manager.Player.MoveDown();
                    break;

                case ConsoleKey.A or ConsoleKey.LeftArrow:
                    _manager.Player.MoveLeft();
                    break;

                case ConsoleKey.D or ConsoleKey.RightArrow:
                    _manager.Player.MoveRight();
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