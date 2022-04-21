namespace MyForestGame.Core.GameComponents;

internal class CollisionHandler : ICollisionHandler
{
    private readonly IGameManager _manager;

    public CollisionHandler(IGameManager gameManager)
    {
        _manager = gameManager;
    }

    private IGameCounter GameCounter { get => _manager.GameCounter; }
    private IList<IGameObject> GameObjects { get => _manager.GameObjectsСollection; }

    public bool IsCollision(BaseMovableGameObject movableObject, int newWidthPosition, int newHightPosition)
    {
        if (movableObject is EnemyObject enemyObj)
        {
            if (GameObjects.FirstOrDefault(x => x is PlayerObject && x.IsCurrentPosition(newWidthPosition, newHightPosition)) is PlayerObject)
            {
                GameEnd(false);
            }
            else if (GameObjects.FirstOrDefault(x => !ReferenceEquals(x, movableObject) && x.IsCurrentPosition(newWidthPosition, newHightPosition)) is not null)
            {
                return true;
            }
        }
        else if (movableObject is PlayerObject playerObj)
        {
            if (GameObjects.FirstOrDefault(x => x is EnemyObject && x.IsCurrentPosition(newWidthPosition, newHightPosition)) is EnemyObject)
            {
                GameEnd(false);
            }
            else if (GameObjects.FirstOrDefault(x => x is PointObject && x.IsCurrentPosition(newWidthPosition, newHightPosition)) is PointObject obj)
            {
                GameCounter.PointsCounter += obj.Points;
                if (GameCounter.PointsIsEqual) GameEnd(true);
                obj.IsVisible = false;
            }
            else if (GameObjects.FirstOrDefault(x => x is ObstacleObject && x.IsCurrentPosition(newWidthPosition, newHightPosition)) is ObstacleObject)
            {
                return true;
            }
        }
        return false;
    }

    private Task GameEnd(bool goodOrBad)
    {
        _manager.IsGameOver = true;
        Task.Delay(100).Wait();
        Console.Clear();

        if (goodOrBad)
        {
            Print("< GOOD GAME >", ConsoleColor.DarkGreen);
        }
        else
        {
            Print("< GAME OVER >", ConsoleColor.DarkRed);
        }

        Print($"LEVEL REACHED: {GameCounter.CurrentLevel}", ConsoleColor.DarkBlue);
        Print($"POINTS EARNED: {GameCounter.PointsCounter}", ConsoleColor.DarkBlue);

        while (true)
        {
            Console.WriteLine($"\nPress to exit 'Escape' or 'Enter'...");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.Escape or ConsoleKey.Enter: Environment.Exit(0); break;
            }
        }
    }

    private static void Print(string text, ConsoleColor color)
    {
        Console.BackgroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }

}