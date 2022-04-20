using System;
using MyForestGame.Core.Models;

namespace MyForestGame.Core.Interfaces;

public interface IGameObject
{
    string Name { get; set; }
    string Model { get; set; }
    ConsoleColor ColorBackground { get; set; }
    ConsoleColor ColorObject { get; set; }
    PositionModel CurrentPosition { get; set; }
    bool IsVisible { get; set; }

    void SetCurrentPosition(int width, int hight);
    void SetCurrentPosition(PositionModel position);
    bool IsCurrentPosition(int width, int hight);
    bool IsCurrentPosition(PositionModel position);
}