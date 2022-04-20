namespace MyForestGame.Core.Interfaces.Services;

public interface IGlobalGameEngineService : IService
{
    IRenderEngineService RenderEngine { get; }
    IWorldEngineService WorldEngine { get; }
    IPlayerControlService PlayerControl { get; }
    IGameManagerService GameManager { get; }
}