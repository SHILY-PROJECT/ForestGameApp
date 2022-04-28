namespace ForestGame.Core.GameObjects.Settings;

internal record EnemySettings : BaseSettings
{
    public EnemySettings(string name, string displayedModel, ConsoleColor colorObject, ConsoleColor colorBackground, int stepSpeedInMilliseconds)
        : base(name, displayedModel, colorObject, colorBackground) => StepSpeedInMilliseconds = stepSpeedInMilliseconds;

    public int StepSpeedInMilliseconds { get; set; }
}