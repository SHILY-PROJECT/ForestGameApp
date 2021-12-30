using System;
using System.Collections.Generic;
using Game.Core.Models;
using Game.Core.Interfaces;
using Game.Core.BaseObjects;

namespace Game.Core.GameObjects
{
    public class EnemyObject : DynamicGameObjectBase, IEnemyObject
    {
        public int StepSpeedInMilliseconds { get; set; }
        public TimeSpan LastStep { get; set; } = TimeSpan.FromSeconds(IGameManager.CurrentTime);
        public bool IsTimeToTakeStep => CalculateTimeForStep();

        public EnemyObject(PositionModel currentPosition) : base(currentPosition)
        {
            var objects = new List<(string name, string model, int stepSpeed, ConsoleColor colorObject, ConsoleColor colorBackground)>
            {
                ("Wolve", ">-<", new Random().Next(200, 400), ConsoleColor.White, ConsoleColor.DarkRed),
                ("Bear", "<^>", new Random().Next(400, 600), ConsoleColor.White, ConsoleColor.DarkRed)
            };
            (Name, Model, StepSpeedInMilliseconds, ColorObject, ColorBackground) = objects[new Random().Next(objects.Count)];
        }

        private bool CalculateTimeForStep()
        {
            var currentTime = TimeSpan.FromSeconds(IGameManager.CurrentTime);

            if ((currentTime.TotalMilliseconds - LastStep.TotalMilliseconds) < StepSpeedInMilliseconds) return false;

            LastStep = currentTime;
            return true;
        }
    }
}
