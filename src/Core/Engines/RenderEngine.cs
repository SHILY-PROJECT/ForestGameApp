using System;
using System.Text;
using System.Linq;
using MyForestGame.Core.BaseObjects;
using MyForestGame.Core.Interfaces;
using MyForestGame.Core.Models;
using MyForestGame.Core.Interfaces.Services;

namespace MyForestGame.Core.Engines;

internal class RenderEngine : IRenderEngineService
{
    private int _counterWidth;
    private int _counterHight;

    private int _pastNumberLevel = 0;
    private int _pastNumberPoints = 0;

    private IGameManagerService GameManager { get; }
    private GameGridSizeModel GridSize { get; }
    private GameCounterModel GameCounter { get; }

    public RenderEngine(IGameManagerService gameManager)
    {
        GameManager = gameManager;
        GridSize = GameManager.GridSize;
        GameCounter = GameManager.GameCounter;
    }

    public void Connect()
    {
        _counterWidth = (GridSize.Width * 4 + 1) - 33;
        _counterHight = (GridSize.Height * 2);

        this.RenderStartMap();
        this.RenderStartAllGameObjects();
    }

    public void UpdateRender()
    {
        foreach (var obj in GameManager.GameObjectsСollection.Where(obj => obj is DynamicGameObjectBase && obj.IsVisible).Select(x => (DynamicGameObjectBase)x))
        {
            if (obj.CurrentAndPastPositionIsEqual is false)
            {
                RenderGameObject(obj);
                obj.PastPosition = new(obj.CurrentPosition);
            }
        }
        UpdatePointsCounter();
    }

    private void RenderStartAllGameObjects()
    {
        GameManager.GameObjectsСollection.ForEach(obj => RenderGameObject(obj));
        RenderPointsCounter();
    }

    private void RenderStartMap()
    {
        var buffer = new StringBuilder();

        var sizeWidth = (GridSize.Width + 1);
        var sizeHeight = (GridSize.Height * 2 + 1);

        for (int indexHeight = 0; indexHeight < sizeHeight; indexHeight++)
        {
            for (int indexWidth = 0; indexWidth < sizeWidth; indexWidth++)
            {
                if (indexHeight == 0)
                {
                    AppendToBuffer(buffer, ("╔═══", "╤═══", "╗"), (indexWidth, sizeWidth));
                }
                else if ((indexHeight + 1) == sizeHeight)
                {
                    AppendToBuffer(buffer, ("╚═══", "╧═══", "╝"), (indexWidth, sizeWidth));
                }
                else if ((indexHeight + 1) % 2 == 0)
                {
                    AppendToBuffer(buffer, ("║   ", "│   ", "║"), (indexWidth, sizeWidth));
                }
                else
                {
                    AppendToBuffer(buffer, ("╟───", "┼───", "╢"), (indexWidth, sizeWidth));
                }
            }
            buffer.AppendLine();
        }

        Console.WriteLine(buffer);
    }

    private static void AppendToBuffer(StringBuilder buffer, (string first, string middle, string last) obj, (int index, int size) width)
    {
        if (width.index == 0)
        {
            buffer.Append(obj.first);
        }
        else if ((width.index + 1) == width.size)
        {
            buffer.Append(obj.last);
        }
        else buffer.Append(obj.middle);
    }

    private static void RenderGameObject(IGameObject obj)
    {
        int left, top;

        if (obj is DynamicGameObjectBase dObj)
        {
            left = dObj.PastPosition.Width * 4 + 1;
            top = dObj.PastPosition.Height * 2 + 1;

            RenderGameObject("   ", left, top, default, default);
        }

        left = obj.CurrentPosition.Width * 4 + 1;
        top = obj.CurrentPosition.Height * 2 + 1;

        RenderGameObject(obj.Model, left, top, obj.ColorObject, obj.ColorBackground);
    }

    private static void RenderGameObject(string text, int left, int top, ConsoleColor colorObject, ConsoleColor colorBackground)
    {
        Console.SetCursorPosition(left, top);
        Console.ForegroundColor = colorObject;
        Console.BackgroundColor = colorBackground;
        Console.Write(text);
        Console.ResetColor();
        Console.CursorVisible = false;
    }

    private void RenderPointsCounter()
    {
        RenderGameObject("║  POINTS: 0    │  LEVEL: 0     ║", _counterWidth, _counterHight + 1, ConsoleColor.White, default);
        RenderGameObject("╚═══════════════╧═══════════════╝", _counterWidth, _counterHight + 2, ConsoleColor.White, default);

        UpdatePointsCounter();
    }

    private void UpdatePointsCounter()
    {
        if (GameCounter.PointsCounter != _pastNumberPoints)
        {
            _pastNumberPoints = GameCounter.PointsCounter;
            RenderGameObject(GameCounter.PointsCounter.ToString(), _counterWidth + 11, _counterHight + 1, ConsoleColor.White, default);
        }
        if (GameCounter.CurrentLevel != _pastNumberLevel)
        {
            _pastNumberLevel = GameCounter.CurrentLevel;
            RenderGameObject(GameCounter.CurrentLevel.ToString(), _counterWidth + 26, _counterHight + 1, ConsoleColor.White, default);
        }
    }
}