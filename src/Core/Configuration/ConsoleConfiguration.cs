namespace ForestGame.Core.Configuration;

internal class ConsoleConfiguration
{
    public static void Set(IGameGridSize gridSize)
    {       
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;
        Console.SetWindowSize((int)(gridSize.Width * 4.1), (int)(gridSize.Height * 2.22));
        Console.CursorVisible = false;
    }
}
