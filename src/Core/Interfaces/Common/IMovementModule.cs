namespace MyForestGame.Core.Interfaces.Common;

public interface IMovementModule
{
    void Up(BaseMovableGameObject movableGameObject);
    void Down(BaseMovableGameObject movableGameObject);
    void Right(BaseMovableGameObject movableGameObject);
    void Left(BaseMovableGameObject movableGameObject);
}