namespace MyForestGame.Core.Interfaces.GameComponents.Objs;

public interface IEnemyGameObject : IMoveController
{
    bool IsTimeToTakeStep { get; }
    int StepSpeedInMilliseconds { get; set; }
    TimeSpan LastStep { get; set; }
}