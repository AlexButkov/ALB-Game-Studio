using System;
using System.Threading;
using System.Collections;

namespace ALB
{

    class Model
    {
        public const ConsoleColor DefaultColor = ConsoleColor.Black;
        public static string DefaultSymbol { get; set; } = " ";
        /// <summary>масштаб окна консоли относительно экрана</summary>
        public static float WindowScaler { get; set; } = 1.0f;
        /// <summary>максимальная ширина объектов</summary>
        public static int MaxWidth { get; set; } = (int)(Console.LargestWindowWidth * WindowScaler);// Console.BufferWidth - 1;
        /// <summary>максимальная высота объектов</summary>
        public static int MaxHeight { get; set; } = (int)(Console.LargestWindowHeight * WindowScaler);// Console.BufferHeight - 1; 
        /// <summary>ширина базовой ячейки координатной сетки</summary>
        public static int GridWidth { get; set; } = 6;
        /// <summary>>высота базовой ячейки координатной сетки</summary>
        public static int GridHeight { get; set; } = 4;
        /// <summary>объект для поочередного доступа к консоли</summary>
        public static object Blocker { get; set; } = null;
        /// <summary>начало игры</summary>
        public MDelegate Starter;
        //========
        public Model() 
        {

        }
        //========
        /// <summary>
        /// Проверка наличия нулевого значения
        /// </summary>
        /// <param name="value">Принимаемое значение для проверки</param>
        /// <param name="replace">Возвращаемое значение в случае нуля</param>
        /// <returns></returns>
        public static int NullCheck(int value, int replace = 1)
        {
            return value != 0 ? value : replace;
        }
    }
}
