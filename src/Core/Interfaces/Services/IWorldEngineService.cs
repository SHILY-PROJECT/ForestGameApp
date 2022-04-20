namespace MyForestGame.Core.Interfaces.Services;

public interface IWorldEngineService : IService
{
    void GenerateWorld();
    void UpdateStateWorld();
}