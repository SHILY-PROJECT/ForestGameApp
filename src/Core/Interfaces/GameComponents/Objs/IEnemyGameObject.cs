namespace ForestGame.Core.Interfaces.GameComponents.Objs;

internal interface IEnemyGameObject : IMoveController
{
    bool IsTimeToTakeStep { get; }
    int StepSpeedInMilliseconds { get; set; }
    TimeSpan LastStep { get; set; }
}