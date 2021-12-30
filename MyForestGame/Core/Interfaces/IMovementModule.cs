namespace Game.Core.Interfaces
{
    public interface IMovementModule
    {
        /// <summary>
        /// Движение вверх.
        /// </summary>
        void Up();

        /// <summary>
        /// Движение вниз.
        /// </summary>
        void Down();

        /// <summary>
        /// Движение влево.
        /// </summary>
        void Right();

        /// <summary>
        /// Движение вправо.
        /// </summary>
        void Left();
    }
}
