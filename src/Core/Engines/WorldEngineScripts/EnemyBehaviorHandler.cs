using System;
using MyForestGame.Core.Interfaces;

namespace MyForestGame.Core.Engines.WorldEngineScripts
{
    internal class EnemyBehaviorHandler
    {
        private IGameManager GameManager { get; set; }
        private Random Rnd { get; set; } = new();

        internal EnemyBehaviorHandler(IGameManager manager)
        {
            GameManager = manager;
        }

        internal void Action(IEnemyObject enemy)
        {
            switch (Rnd.Next(4))
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
}
