using System;

namespace MyForestGame.Core.Interfaces;

public interface IEnemyObject : IMoveController
{
    bool IsTimeToTakeStep { get; }
    int StepSpeedInMilliseconds { get; set; }
    TimeSpan LastStep { get; set; }
}