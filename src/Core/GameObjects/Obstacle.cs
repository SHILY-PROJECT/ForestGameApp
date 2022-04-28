namespace ForestGame.Core.GameObjects;

internal class Obstacle : BaseImmovableGameObject
{
    public Obstacle(PositionModel currentPosition) : base(currentPosition)
    {
        var objects = new List<(string name, string model, ConsoleColor colorObject, ConsoleColor colorBackground)>
        {
            ("Stone", "   ", ConsoleColor.White, ConsoleColor.DarkMagenta),
            ("Tree", "   ", ConsoleColor.White, ConsoleColor.DarkMagenta)
        };
        (Name, Model, ColorObject, ColorBackground) = objects[new Random().Next(objects.Count)];
    }
}