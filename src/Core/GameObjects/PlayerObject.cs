using System;
using System.Linq;
using MyForestGame.Core.BaseObjects;
using MyForestGame.Core.Interfaces;
using MyForestGame.Core.Models;

namespace MyForestGame.Core.GameObjects
{
    public class PlayerObject : DynamicGameObjectBase
    {
        internal int Points { get; set; }

        public PlayerObject(IMovementModule movementModule, PositionModel currentPosition, string name) :
            base(movementModule, currentPosition)
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
