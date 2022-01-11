namespace MyForestGame.Core.Models
{
    public class PositionModel
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public PositionModel() { }

        public PositionModel(PositionModel position) : this(position.Width, position.Height) { }

        public PositionModel(int widthPosition, int heightPosition)
        {         
            Width = widthPosition;
            Height = heightPosition;
        }
    }
}
