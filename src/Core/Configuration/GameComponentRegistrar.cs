namespace ForestGame.Core.Configuration;

internal static class GameComponentRegistrar
{
    public static IServiceCollection AddGameComponents(this IServiceCollection services)
    {
        SetConsoleSettings();

        services
            .AddSingleton<Startup>()
            .AddSingleton<PlayerObject>()
            .AddSingleton<IPlayerSettings, PlayerSettings>(s => GetPlayerSettings())
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
        return new GameGridSize(25, 15);
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

    private static void SetConsoleSettings()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;
        Console.SetWindowSize(120, 40);
        Console.CursorVisible = false;
    }
}