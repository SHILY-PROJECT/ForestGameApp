namespace MyForestGame.Core.Engines;

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
        while (true)
        {
            WorldEngine.UpdateStateWorld();
            RenderEngine.UpdateRender();

            if (GameManager.IsGameOver) Task.FromResult(true);

            Task.Delay(10);
        }
    }
}