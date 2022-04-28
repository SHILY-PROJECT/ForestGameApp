namespace ForestGame.Core.Engines.Scripts;

internal class EnemyBehaviorHandler
{
    private readonly IGameManager _manager;
    private readonly Random _rnd = new();

    public EnemyBehaviorHandler(IGameManager manager)
    {
        _manager = manager;
    }

    public void Action(IEnemyGameObject enemy)
    {
        switch (_rnd.Next(4))
        {
            case 0:
                enemy.MoveUp();
                break;

            case 1:
                enemy.MoveDown();
                break;

            case 2:
                enemy.MoveLeft();
                break;

            case 3:
                enemy.MoveRight();
                break;

            default: throw new Exception();
        }
    }
}