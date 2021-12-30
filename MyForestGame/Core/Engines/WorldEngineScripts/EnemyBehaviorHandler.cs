using System;
using Game.Core.Interfaces;

namespace Game.Core.Engines.WorldEngineScripts
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
                    enemy.Move.Up();
                    break;

                case 1:
                    enemy.Move.Down();
                    break;

                case 2:
                    enemy.Move.Left();
                    break;

                case 3:
                    enemy.Move.Right();
                    break;

                default: throw new Exception();
            }
        }
    }
}
