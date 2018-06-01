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
        /// convert grid quantity to X-axis parameter, calculated using GridSize variable (конвертирует количество ячеек в значение для координаты X, рассчитываемое по переменной GridSize)
        /// </summary>
        public static int GridX(this int size)
        {
            return size * (int)Model.GridSize.X;
        }

        /// <summary>
        /// convert grid quantity to Y-axis parameter, calculated using GridSize variable (конвертирует количество ячеек в значение для координаты Y, рассчитываемое по переменной GridSize)
        /// </summary>
        public static int GridY(this int size)
        {
            return size * (int)Model.GridSize.Y;
        }

        /// <summary>
        /// convert grid quantity to X-axis parameter, calculated using GridSize variable (конвертирует количество ячеек в значение для координаты X, рассчитываемое по переменной GridSize)
        /// </summary>
        public static float GridX(this float size)
        {
            return size * (int)Model.GridSize.X;
        }

        /// <summary>
        /// convert grid quantity to Y-axis parameter, calculated using GridSize variable (конвертирует количество ячеек в значение для координаты Y, рассчитываемое по переменной GridSize)
        /// </summary>
        public static float GridY(this float size)
        {
            return size * (int)Model.GridSize.Y;
        }

    }
}
