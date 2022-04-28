namespace ForestGame.Core.GameObjects;

internal abstract class BaseMovableGameObject : BaseGameObject, IMoveController
{
    public BaseMovableGameObject(PositionModel position)
    {
        CurrentPosition = new PositionModel(position.Width, position.Height);
        PastPosition = new PositionModel(position.Width, position.Height);
    }

    public BaseMovableGameObject(IMovementModule movementModule, PositionModel position) : this(position)
    {
        MovementModule = movementModule;
    }

    public IMovementModule MovementModule { get; set; }
    public PositionModel PastPosition { get; set; }
    public bool CurrentAndPastPositionIsEqual => CurrentPosition.Width == PastPosition.Width && CurrentPosition.Height == PastPosition.Height;

    public void SetMoveModule(IMovementModule movementModule)
    {
        MovementModule = movementModule;
    }

    public void SetPastPosition(PositionModel position)
    {
        SetPastPosition(position.Width, position.Height);
    }

    public void SetPastPosition(int width, int hight)
    {
        if (PastPosition != null)
        {
            PastPosition.Width = width;
            PastPosition.Height = hight;
        }
        else PastPosition = new PositionModel(width, hight);
    }

    public void MoveUp() => MovementModule.Up(this);
    public void MoveDown() => MovementModule.Down(this);
    public void MoveRight() => MovementModule.Right(this);
    public void MoveLeft() => MovementModule.Left(this);
}