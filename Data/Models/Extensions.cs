using System;
using System.Threading;
using System.Collections.Generic;

namespace ALB
{
    /// <summary>
    /// extension class (класс для расширений)
    /// </summary>
    public static class Extensions
    {

        /// <summary>
        /// returns checked by range value (возвращает значение, проверенное по диапазону)
        /// </summary>
        /// <param name="min"> range minimal value (минимальное значение диапазона)</param>
        /// <param name="max"> range maximal value (максимальное значение диапазона)</param>
        /// <returns></returns>
        public static float Range(this float val, float? min, float? max)
        {
            float mn = min ?? val;
            float mx = max ?? val;
            return Math.Min(Math.Max(mn, val), mx); 
        }

        /// <summary>
        /// returns checked by range value (возвращает значение, проверенное по диапазону)
        /// </summary>
        /// <param name="min"> range minimal value (минимальное значение диапазона)</param>
        /// <param name="max"> range maximal value (максимальное значение диапазона)</param>
        /// <returns></returns>
        public static int Range(this int val, int? min, int? max)
        {
            int mn = min ?? val;
            int mx = max ?? val;
            return Math.Min(Math.Max(mn, val), mx); 
        }
        
        /// <summary>
        /// converts grid quantity to X-axis parameter using <see cref="Model.GridSize"/> (конвертирует количество ячеек в значение для координаты X, используя <see cref="Model.GridSize"/>)
        /// </summary>
        public static float GridToX(this float size)
        {
            return size.GridTo(Model.GridSize.X); 
        }

        /// <summary>
        /// converts grid quantity to X-axis parameter using <see cref="Model.GridSize"/> (конвертирует количество ячеек в значение для координаты X, используя <see cref="Model.GridSize"/>)
        /// </summary>
        public static int GridToX(this int size)
        {
            return size.GridTo(Model.GridSize.X);
        }

        /// <summary>
        /// converts grid quantity to Y-axis parameter using <see cref="Model.GridSize"/> (конвертирует количество ячеек в значение для координаты Y, используя <see cref="Model.GridSize"/>)
        /// </summary>
        public static float GridToY(this float size)
        {
            return size.GridTo(Model.GridSize.Y);
        }

        /// <summary>
        /// converts grid quantity to Y-axis parameter using <see cref="Model.GridSize"/> (конвертирует количество ячеек в значение для координаты Y, используя <see cref="Model.GridSize"/>)
        /// </summary>
        public static int GridToY(this int size)
        {
            return size.GridTo(Model.GridSize.Y);
        }

        //------

        /// <summary>
        /// converts X-axis parameter to grid quantity using <see cref="Model.GridSize"/> (конвертирует значение для координаты X в количество ячеек, используя <see cref="Model.GridSize"/>)
        /// </summary>
        public static float XToGrid(this float size)
        {
            return size.ToGrid(Model.GridSize.X);
        }

        /// <summary>
        /// converts X-axis parameter to grid quantity using <see cref="Model.GridSize"/> (конвертирует значение для координаты X в количество ячеек, используя <see cref="Model.GridSize"/>)
        /// </summary>
        public static float XToGrid(this int size)
        {
            return ((float)size).ToGrid(Model.GridSize.X);
        }

        /// <summary>
        /// converts Y-axis parameter to grid quantity using <see cref="Model.GridSize"/> (конвертирует значение для координаты Y в количество ячеек, используя <see cref="Model.GridSize"/>)
        /// </summary>
        public static float YToGrid(this float size)
        {
            return size.ToGrid(Model.GridSize.Y);
        }

        /// <summary>
        /// converts Y-axis parameter to grid quantity using <see cref="Model.GridSize"/> (конвертирует значение для координаты Y в количество ячеек, используя <see cref="Model.GridSize"/>)
        /// </summary>
        public static float YToGrid(this int size)
        {
            return ((float)size).ToGrid(Model.GridSize.Y);
        }

        //======
        static int GridTo(this int size, float mult)
        {
            return size * (int)mult;
        }

        static float GridTo(this float size, float mult)
        {
            return size * (int)mult;
        }

        static float ToGrid(this float size, float mult)
        {
            return size / Model.ZeroCheck((int)mult);
        }
    }
}
