using Game.Core.BaseObjects;
using MyForestGame.Core;

namespace MyForestGame
{
    internal class DemoProgram
    {
        internal static void Main()
            => new MainGame(new GameManager()).Start();
    }
}
