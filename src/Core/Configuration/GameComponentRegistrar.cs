namespace ForestGame.Core.Configuration;

internal static class GameComponentRegistrar
{
    public static IServiceCollection AddGameComponents(this IServiceCollection services)
    {
        services
            .AddScoped<Startup>()
            .AddScoped<Player>()
            .AddScoped<PlayerSettings>(s => GetPlayerSettings())
            .AddScoped<IGameBalance, GameBalance>()
            .AddScoped<IGameGridSize, GameGridSize>(s => GetGridSize())
            .AddScoped<IGameCounter, GameCounter>()
            .AddScoped<IMainGame, MainGame>()
            .AddScoped<IGameManager, GameManager>()
            .AddScoped<ICollisionHandler, CollisionHandler>()
            .AddScoped<IRenderEngine, RenderEngine>()
            .AddScoped<IWorldEngine, WorldEngine>()
            .AddScoped<IPlayerControl, PlayerControl>()
            .AddScoped<IGlobalGameEngine, GameEngine>();

        return services;
    }

    private static GameGridSize GetGridSize()
    {
        var gridSize = new GameGridSize(25, 15);
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