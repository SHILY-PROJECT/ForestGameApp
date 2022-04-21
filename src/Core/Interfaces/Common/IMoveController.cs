namespace MyForestGame.Core.Interfaces.Common;

internal interface IMoveController
{
    IMovementModule MovementModule { get; set; }

    void MoveUp();
    void MoveDown();
    void MoveRight();
    void MoveLeft();
}