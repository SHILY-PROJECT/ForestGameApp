using MyForestGame.Core.Interfaces.Services;
using System.Threading.Tasks;

namespace MyForestGame.Core.Engines;

public class GameEngine : IGlobalGameEngineService
{
    public IRenderEngineService RenderEngine { get; init;}
    public IWorldEngineService WorldEngine { get; init; }
    public IPlayerControlService PlayerControl { get; init; }
    public IGameManagerService GameManager { get; init; }

    public GameEngine(
        IGameManagerService gameManager,
        IWorldEngineService worldEngine,
        IRenderEngineService renderEngine,
        IPlayerControlService playerControl)
    {
        GameManager = gameManager;
        WorldEngine = worldEngine;
        RenderEngine = renderEngine;
        PlayerControl = playerControl;
    }

    public void Connect()
    {
        RenderEngine.Connect();
        PlayerControl.Connect();
        Parallel.Invoke(() => this.Update());
    }

    private void Update()
    {
        Task.Run(() =>
        {
            while (true)
            {
                WorldEngine.UpdateStateWorld();
                RenderEngine.UpdateRender();

                if (GameManager.IsGameOver) return;

                Task.Delay(10).Wait();
            }
        });
    }
}