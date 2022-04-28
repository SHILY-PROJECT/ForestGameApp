namespace ForestGame.Core;

internal class MainGame : IMainGame
{
    private readonly IGlobalGameEngine _gameEngine;

    public MainGame(IGlobalGameEngine globalGame)
    {
        _gameEngine = globalGame;
    }

    public void Run() => _gameEngine.Connect();
}