namespace ForestGame.Core.Interfaces.GameObjects;

internal interface IMoveController
{
    IMovementModule MovementModule { get; set; }

    void MoveUp();
    void MoveDown();
    void MoveRight();
    void MoveLeft();
}