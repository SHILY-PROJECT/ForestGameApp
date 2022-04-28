namespace ForestGame.Core.GameComponents;

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
        if (movableObject is Enemy enemyObj)
        {
            if (GameObjects.FirstOrDefault(x => x is Player && x.IsCurrentPosition(newWidthPosition, newHightPosition)) is Player)
            {
                GameEnd(false);
            }
            else if (GameObjects.FirstOrDefault(x => !ReferenceEquals(x, movableObject) && x.IsCurrentPosition(newWidthPosition, newHightPosition)) is not null)
            {
                return true;
            }
        }
        else if (movableObject is Player playerObj)
        {
            if (GameObjects.FirstOrDefault(x => x is Enemy && x.IsCurrentPosition(newWidthPosition, newHightPosition)) is Enemy)
            {
                GameEnd(false);
            }
            else if (GameObjects.FirstOrDefault(x => x is Point && x.IsCurrentPosition(newWidthPosition, newHightPosition)) is Point obj)
            {
                GameCounter.PointsCounter += obj.Points;
                if (GameCounter.PointsIsEqual) GameEnd(true);
                obj.IsVisible = false;
            }
            else if (GameObjects.FirstOrDefault(x => x is Obstacle && x.IsCurrentPosition(newWidthPosition, newHightPosition)) is Obstacle)
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