namespace ForestGame.Core.Engines;

internal class GameEngine : IGlobalGameEngine
{
    public GameEngine(
        IGameManager gameManager,
        IWorldEngine worldEngine,
        IRenderEngine renderEngine,
        IPlayerControl playerControl)
    {
        GameManager = gameManager;
        WorldEngine = worldEngine;
        RenderEngine = renderEngine;
        PlayerControl = playerControl;
    }

    public IRenderEngine RenderEngine { get; init;}
    public IWorldEngine WorldEngine { get; init; }
    public IPlayerControl PlayerControl { get; init; }
    public IGameManager GameManager { get; init; }

    public void Connect()
    {
        WorldEngine.Connect();
        RenderEngine.Connect();

        Task.WaitAll(new[]
        {
            Task.Run(() => UpdateView()),
            Task.Run(() => PlayerControl.Connect())
        });
    }

    public Task UpdateView()
    {
        try
        {
            while (true)
            {
                WorldEngine.UpdateStateWorld();
                RenderEngine.UpdateRender();

                if (GameManager.IsGameOver) break;

                Task.Delay(10);
            }
        }
        catch (OperationCanceledException ex) when (ex.Message.Equals("good", StringComparison.OrdinalIgnoreCase))
        {
            GameEnd(true);
        }
        catch (OperationCanceledException ex) when (ex.Message.Equals("bad", StringComparison.OrdinalIgnoreCase))
        {
            GameEnd(false);
        }
        return Task.CompletedTask;
    }

    private Task GameEnd(bool goodOrBad)
    {
        GameManager.IsGameOver = true;
        Task.Delay(100).Wait();
        Console.Clear();

        new (string text, ConsoleColor color)[]
        {
            (goodOrBad ? "< GOOD GAME >" : "< GAME OVER >", goodOrBad ? ConsoleColor.DarkGreen : ConsoleColor.DarkRed),
            ($"LEVEL REACHED: {GameManager.GameCounter.CurrentLevel}", ConsoleColor.DarkBlue),
            ($"POINTS EARNED: {GameManager.GameCounter.PointsCounter}", ConsoleColor.DarkBlue)
        }
        .ToList().ForEach(x => Print(x.text, x.color));

        while (true)
        {
            Console.WriteLine($"\nPress to exit 'Escape' or 'Enter'...");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.Escape or ConsoleKey.Enter: Environment.Exit(0); break;
            }
        }
    }

    private static void Print(string text, ConsoleColor color)
    {
        Console.BackgroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }
}