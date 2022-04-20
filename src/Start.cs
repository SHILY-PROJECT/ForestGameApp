using System;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MyForestGame.Core;
using MyForestGame.Core.Interfaces.Services;
using MyForestGame.Core.BaseObjects;
using MyForestGame.Core.Engines;

namespace MyForestGame;

internal class Start
{
    public Start(IGameService gameService)
    {
        Game = gameService;
    }

    private IGameService Game { get; set; }

    private void StartGame() => Game.Run();

    internal static void Main(string[] args)
    {
        SetConsoleSettings();
        var host = CreateHostBuilder(args).Build();
        host.Services.GetRequiredService<Start>().StartGame();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) => Host
        .CreateDefaultBuilder(args)
        .ConfigureServices(services =>
        {
            services
                .AddSingleton<IGameService, MainGame>()
                .AddSingleton<IGameManagerService, GameManager>()
                .AddSingleton<IRenderEngineService, RenderEngine>()
                .AddSingleton<IWorldEngineService, WorldEngine>();
        });

    private static void SetConsoleSettings()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;
        Console.SetWindowSize(120, 40);
        Console.CursorVisible = false;
    }
}