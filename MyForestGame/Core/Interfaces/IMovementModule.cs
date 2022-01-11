using MyForestGame.Core.BaseObjects;

namespace MyForestGame.Core.Interfaces
{
    public interface IMovementModule
    {
        /// <summary>
        /// Движение вверх.
        /// </summary>
        void Up(DynamicGameObjectBase dynamicObj);

        /// <summary>
        /// Движение вниз.
        /// </summary>
        void Down(DynamicGameObjectBase dynamicObj);

        /// <summary>
        /// Движение влево.
        /// </summary>
        void Right(DynamicGameObjectBase dynamicObj);

        /// <summary>
        /// Движение вправо.
        /// </summary>
        void Left(DynamicGameObjectBase dynamicObj);
    }
}
