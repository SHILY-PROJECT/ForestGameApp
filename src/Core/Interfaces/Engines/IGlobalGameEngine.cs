namespace ForestGame.Core.Interfaces.Engines;

internal interface IGlobalGameEngine : IService
{
    IRenderEngine RenderEngine { get; }
    IWorldEngine WorldEngine { get; }
    IPlayerControl PlayerControl { get; }
    IGameManager GameManager { get; }
}