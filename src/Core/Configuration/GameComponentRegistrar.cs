namespace ForestGame.Core.Configuration;

internal static class GameComponentRegistrar
{
    public static IServiceCollection AddGameComponents(this IServiceCollection services)
    {
        services
            .AddSingleton<Startup>()
            .AddSingleton<Player>()
            .AddSingleton<PlayerSettings>(s => GetPlayerSettings())
            .AddSingleton<IGameBalance, GameBalance>()
            .AddSingleton<IGameGridSize, GameGridSize>(s => GetGridSize())
            .AddSingleton<IGameCounter, GameCounter>()
            .AddSingleton<IMainGame, MainGame>()
            .AddSingleton<IGameManager, GameManager>()
            .AddSingleton<ICollisionHandler, CollisionHandler>()
            .AddSingleton<IRenderEngine, RenderEngine>()
            .AddSingleton<IWorldEngine, WorldEngine>()
            .AddSingleton<IPlayerControl, PlayerControl>()
            .AddSingleton<IGlobalGameEngine, GameEngine>();

        return services;
    }

    private static GameGridSize GetGridSize()
    {
        var gridSize = new GameGridSize(35, 20);
        ConsoleConfiguration.Set(gridSize);
        return gridSize;
    }

    private static PlayerSettings GetPlayerSettings()
    {
        return new PlayerSettings
        {
            Name = "Player",
            DisplayedModel = "^-^",
            ColorObject = ConsoleColor.White,
            ColorBackground = ConsoleColor.DarkBlue,
            StartPosition = new(0, 0)
        };
    }
}