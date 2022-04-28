namespace ForestGame.Core.GameObjects;

internal class Enemy : BaseMovableGameObject, IEnemyGameObject
{
    public Enemy(EnemySettings enemySettings, IMovementModule movementModule) : base(movementModule, enemySettings.StartPosition)
    {
        Name = enemySettings.Name;
        Model = enemySettings.DisplayedModel;
        StepSpeedInMilliseconds = enemySettings.StepSpeedInMilliseconds;
        ColorObject = enemySettings.ColorObject;
        ColorBackground = enemySettings.ColorBackground;
    }

    public int StepSpeedInMilliseconds { get; set; }
    public TimeSpan LastStep { get; set; } = TimeSpan.FromSeconds(IGameManager.CurrentTime);
    public bool IsTimeToTakeStep => CalculateTimeForStep();

    private bool CalculateTimeForStep()
    {
        var currentTime = TimeSpan.FromSeconds(IGameManager.CurrentTime);

        if (currentTime.TotalMilliseconds - LastStep.TotalMilliseconds < StepSpeedInMilliseconds) return false;

        LastStep = currentTime;
        return true;
    }
}