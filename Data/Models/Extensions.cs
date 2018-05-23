using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using ALB;

namespace ALB
{
    public static class Extensions
    {

        /// <summary>
        /// convert grid quantity to X-axis parameter, calculated using GridSize variable (конвертирует количество ячеек в значение для координаты X, рассчитываемое по переменной GridSize)
        /// </summary>
        public static int GridToX(this int size)
        {
            return size * (int)Model.GridSize.X;
        }

        /// <summary>
        /// convert grid quantity to Y-axis parameter, calculated using GridSize variable (конвертирует количество ячеек в значение для координаты Y, рассчитываемое по переменной GridSize)
        /// </summary>
        public static int GridToY(this int size)
        {
            return size * (int)Model.GridSize.Y;
        }

        /// <summary>
        /// convert grid quantity to X-axis parameter, calculated using GridSize variable (конвертирует количество ячеек в значение для координаты X, рассчитываемое по переменной GridSize)
        /// </summary>
        public static float GridToX(this float size)
        {
            return size * (int)Model.GridSize.X;
        }

        /// <summary>
        /// convert grid quantity to Y-axis parameter, calculated using GridSize variable (конвертирует количество ячеек в значение для координаты Y, рассчитываемое по переменной GridSize)
        /// </summary>
        public static float GridToY(this float size)
        {
            return size * (int)Model.GridSize.Y;
        }

    }
}
