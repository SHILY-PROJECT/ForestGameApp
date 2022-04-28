namespace ForestGame;

internal class Startup
{
    public Startup(IMainGame mainGameService)
    {
        Game = mainGameService;
    }

    private IMainGame Game { get; set; }

    private void StartGame() => Game.Run();

    internal static void Main(string[] args) =>   
        CreateHostBuilder(args).Build().Services.GetRequiredService<Startup>().StartGame();

    private static IHostBuilder CreateHostBuilder(string[] args) => Host
        .CreateDefaultBuilder(args)
        .ConfigureServices(services => services.AddGameComponents());
}