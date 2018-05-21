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
        public static int GridToX(this float size)
        {
            return (int)(size * (int)Model.GridSize.X);
        }

        /// <summary>
        /// convert grid quantity to Y-axis parameter, calculated using GridSize variable (конвертирует количество ячеек в значение для координаты Y, рассчитываемое по переменной GridSize)
        /// </summary>
        public static int GridToY(this float size)
        {
            return (int)(size * (int)Model.GridSize.Y);
        }

        //-------------------

        /// <summary>
        /// Конвертирует PosType в значение для координаты X
        /// </summary>
        /// <param name="posType">имя перечисляемого типа SideX</param>
        /// <param name="objectSize">размер объекта</param>
        /// <returns>значение для координаты X</returns>
        public static int PosTypeToX(this Object posType, float objectSize)
        {
            try
            {
                switch ((int)posType)
                {
                    case -1:
                        return (int)objectSize / 2 - (int)Model.WindowSize.X / 2;//+ Shift
                    case 0:
                        return 0;
                    case 1:
                        return (int)Model.WindowSize.X / 2 - (int)objectSize / 2;//- Shift
                    default:
                        goto case 0;
                }
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// Конвертирует PosType в значение для координаты Y
        /// </summary>
        /// <param name="PosType">имя перечисляемого типа SideY</param>
        /// <param name="ObjectSize">размер объекта</param>
        /// <returns>значение для координаты Y</returns>
        public static int PosTypeToY(this Object PosType, float ObjectSize)//
        {
            try
            {
                switch ((int)PosType)
                {
                    case -1:
                        return (int)ObjectSize / 2 - (int)Model.WindowSize.Y / 2;//+ Shift
                    case 0:
                        return 0;
                    case 1:
                        return (int)Model.WindowSize.Y / 2 - (int)ObjectSize / 2;//- Shift
                    default:
                        goto case 0;
                }
            }
            catch
            {
                return 0;
            }
        }
    }
}
