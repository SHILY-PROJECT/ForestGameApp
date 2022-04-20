using MyForestGame.Core.BaseObjects;
using MyForestGame.Core;

namespace MyForestGame
{
    internal class Start
    {
        internal static void Main()
            => new MainGame(new GameManager()).Start();
    }
}
