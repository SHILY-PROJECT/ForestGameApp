using System;
using System.Linq;
using Game.Core.BaseObjects;
using Game.Core.Models;

namespace Game.Core.GameObjects
{
    public class PlayerObject : DynamicGameObjectBase
    {
        internal int Points { get; set; }

        public PlayerObject(string name, PositionModel currentPosition) : base(currentPosition)
        {
            if (name.All(x => char.IsLetter(x)) is false)
                throw new ArgumentException($"'{nameof(name)}:{name}' - invalid argument");

            Name = name;
            Model = "^-^";
            ColorObject = ConsoleColor.White;
            ColorBackground = ConsoleColor.DarkBlue;
        }
    }
}
