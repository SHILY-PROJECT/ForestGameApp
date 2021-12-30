namespace Game.Core.Models
{
    public class GameCounterModel
    {
        public int CurrentLevel { get; set; } = 1;
        public int PointsCounter { get; set; } = 0;
        public int VictoryPoints { get; set; } = 0;
        public bool PointsIsEqual => PointsCounter == VictoryPoints;
    }
}
