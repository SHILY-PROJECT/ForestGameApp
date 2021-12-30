using System;

namespace Game.Core.Interfaces
{
    public interface IEnemyObject
    {
        IMovementModule Move { get; set; }
        bool IsTimeToTakeStep { get; }
        int StepSpeedInMilliseconds { get; set; }
        TimeSpan LastStep { get; set; }
    }
}
