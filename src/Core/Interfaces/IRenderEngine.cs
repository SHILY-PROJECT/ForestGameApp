namespace MyForestGame.Core.Interfaces
{
    public interface IRenderEngine
    {
        /// <summary>
        /// Обновление отрисовки динамических объектов.
        /// </summary>
        void UpdateRenderDynamicGameObjects();

        /// <summary>
        /// Отрисовка всех объектов игры.
        /// </summary>
        void RenderAllGameObjects();

        /// <summary>
        /// Отрисовка игровой карты.
        /// </summary>
        void RenderMap();
    }
}
