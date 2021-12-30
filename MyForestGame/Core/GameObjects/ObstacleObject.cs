using System;
using System.Collections.Generic;
using Game.Core.BaseObjects;
using Game.Core.Models;

namespace Game.Core.GameObjects
{
    public class ObstacleObject : StaticGameObjectBase
    {
        public ObstacleObject(PositionModel currentPosition) : base(currentPosition)
        {
            var objects = new List<(string name, string model, ConsoleColor colorObject, ConsoleColor colorBackground)>
            {
                ("Stone", "   ", ConsoleColor.White, ConsoleColor.DarkMagenta),
                ("Tree", "   ", ConsoleColor.White, ConsoleColor.DarkMagenta)
            };
            (Name, Model, ColorObject, ColorBackground) = objects[new Random().Next(objects.Count)];
        }
    }
}
