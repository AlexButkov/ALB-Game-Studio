using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace ALB
{

    class Model
    {
        /// <summary>(базовый цвет для рендеринга сцены)</summary>
        public const ConsoleColor DefaultColor = ConsoleColor.Black;
        /// <summary>(символ для рендеринга сцены)</summary>
        public static string DefaultSymbol { get; } = " ";
        /// <summary>(размер базовой ячейки координатной сетки)</summary>
        public static Vector GridSize { get; } = new Vector(6, 4);// >=(3,2)
        /// <summary>(масштаб окна консоли относительно экрана)</summary>
        public static float WindowScaler { get; } = 1.0f; // <=1
        //========
        /// <summary>(размер окна консоли)</summary>
        public static Vector WindowSize { get; } = new Vector((int)Math.Abs(Console.LargestWindowWidth * WindowScaler), (int)Math.Abs(Console.LargestWindowHeight * WindowScaler));
        /// <summary>(список объектов на сцене)</summary>
        public static List<ObjectGroup> SceneList = new List<ObjectGroup>();
        /// <summary>(3D копия окна для хранения фонового цвета и № слоев)</summary>
        public static List<ObjectSingle>[,] WindowArray = new List<ObjectSingle>[WindowSize.GetX, WindowSize.GetY];
        /// <summary>(объект для поочередного доступа к консоли)</summary>
        public static object Blocker { get; set; } = null;
        /// <summary>(начало игры)</summary>
        public MDelegate Starter;
        //========
        /*public Model() 
        {

        }*/
        //========


        //========
        /// <summary>
        /// Проверка наличия нулевого значения
        /// </summary>
        /// <param name="value">Принимаемое значение для проверки</param>
        /// <param name="replace">Возвращаемое значение в случае нуля</param>
        /// <returns></returns>
        public static int CheckNull(int value, int replace = 1)
        {
            return value != 0 ? value : replace;
        }

        /// <summary>
        /// сheck position X overflow (проверка на выход позиции за пределы сцены)
        /// </summary>
        /// <param name="Pos">position X</param>
        protected static int CheckSizeX(int Pos)
        {
            return Math.Max(Math.Min(Pos , WindowSize.GetX - 1), 0);
        }

        /// <summary>
        /// сheck position Y overflow (проверка на выход позиции за пределы сцены)
        /// </summary>
        /// <param name="Pos">position Y</param>
        protected static int CheckSizeY(int Pos)
        {
            return Math.Max(Math.Min(Pos , WindowSize.GetY - 1), 0);
        }

        /// <summary>
        /// Конвертирует имя перечисляемого типа SideX в значение для координаты X
        /// </summary>
        /// <param name="Side">имя перечисляемого типа SideX</param>
        /// <param name="ObjectSize">размер объекта</param>
        /// <returns>значение для координаты X</returns>
        public static int ConvertPosTypeX(object Side, int ObjectSize)
        {
            try
            {
                switch ((int)Side)
                {
                    case -1:
                        return ObjectSize / 2 - WindowSize.GetX / 2;//+ Shift
                    case 0:
                        return 0;
                    case 1:
                        return WindowSize.GetX / 2 - ObjectSize / 2;//- Shift
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
        /// Конвертирует имя перечисляемого типа SideY в значение для координаты Y
        /// </summary>
        /// <param name="Side">имя перечисляемого типа SideY</param>
        /// <param name="ObjectSize">размер объекта</param>
        /// <returns>значение для координаты Y</returns>
        public static int ConvertPosTypeY(object Side, int ObjectSize)//
        {
            try
            {
                switch ((int)Side)
                {
                    case -1:
                        return ObjectSize / 2 - WindowSize.GetY / 2;//+ Shift
                    case 0:
                        return 0;
                    case 1:
                        return WindowSize.GetY / 2 - ObjectSize / 2;//- Shift
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
