namespace MyForestGame.Core.Interfaces
{
    public interface IWorldEngine
    {
        /// <summary>
        /// Генерация мира.
        /// </summary>
        void GenerateWorld();

        /// <summary>
        /// Обновления состояния мира.
        /// </summary>
        void UpdateStateWorld();
    }
}
