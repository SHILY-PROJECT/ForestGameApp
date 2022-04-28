namespace ForestGame.Core.GameComponents;

internal class GameManager : IGameManager
{
    public PlayerObject Player { get; set; }
    public IGameGridSize GameGridSize { get; set; }
    public IGameCounter GameCounter { get; set; }
    public ICollisionHandler CollisionHandler { get; set; }
    public IGameBalance GameObjectsSettings { get; set; }
    public IList<IGameObject> GameObjectsСollection { get; set; } = new List<IGameObject>();
    public bool IsGameOver { get; set; }

    public GameManager(
        PlayerObject player,
        IGameGridSize gameGridSize,
        IGameCounter gameCounter,
        IGameBalance gameObjectsSettings)
    {
        Player = player;
        GameGridSize = gameGridSize;
        GameCounter = gameCounter;
        GameObjectsSettings = gameObjectsSettings;
        CollisionHandler = new CollisionHandler(this);
    }
}