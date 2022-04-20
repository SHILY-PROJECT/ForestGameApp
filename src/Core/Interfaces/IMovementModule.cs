using MyForestGame.Core.BaseObjects;

namespace MyForestGame.Core.Interfaces;

public interface IMovementModule
{
    void Up(DynamicGameObjectBase dynamicObj);
    void Down(DynamicGameObjectBase dynamicObj);
    void Right(DynamicGameObjectBase dynamicObj);
    void Left(DynamicGameObjectBase dynamicObj);
}