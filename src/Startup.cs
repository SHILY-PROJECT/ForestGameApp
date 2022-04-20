using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MyForestGame.Core;
using MyForestGame.Core.Interfaces.Services;

namespace MyForestGame;

internal class Startup
{
    public Startup(IMainGameService mainGameService) => Game = mainGameService;

    private IMainGameService Game { get; set; }

    private void StartGame() => Game.Run();

    internal static void Main(string[] args) =>   
        CreateHostBuilder(args).Build().Services.GetRequiredService<Startup>().StartGame();

    private static IHostBuilder CreateHostBuilder(string[] args) => Host
        .CreateDefaultBuilder(args)
        .ConfigureServices(services => services.AddGameComponents());
}