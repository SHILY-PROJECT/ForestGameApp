using MyForestGame.Core.Interfaces.Services;

namespace MyForestGame.Core;

internal class MainGame : IMainGameService
{
    private IGlobalGameEngineService GameEngine { get; }
    public MainGame(IGlobalGameEngineService globalGame) => GameEngine = globalGame;
    public void Run() => GameEngine.Connect();   
}