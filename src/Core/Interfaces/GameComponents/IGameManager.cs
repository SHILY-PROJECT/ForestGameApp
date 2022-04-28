namespace ForestGame.Core.Interfaces.GameComponents;

internal interface IGameManager
{
    PlayerObject Player { get; set; }
    IGameGridSize GameGridSize { get; set; }
    IGameCounter GameCounter { get; set; }
    ICollisionHandler CollisionHandler { get; }
    IGameBalance GameObjectsSettings { get; set; }
    IList<IGameObject> GameObjectsСollection { get; set; }

    bool IsGameOver { get; set; }
    public static int CurrentTime => (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
}