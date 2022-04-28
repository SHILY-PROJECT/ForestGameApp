namespace ForestGame.Core.Interfaces.GameObjects;

internal interface IEnemyGameObject : IMoveController
{
    bool IsTimeToTakeStep { get; }
    int StepSpeedInMilliseconds { get; set; }
    TimeSpan LastStep { get; set; }
}