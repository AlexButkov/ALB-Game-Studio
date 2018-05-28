using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace ALB
{
    class Model 
    {
        /// <summary>(базовый цвет для рендеринга сцены)</summary>
        public const ConsoleColor DefaultColor = ConsoleColor.Black;
        /// <summary>(символ для рендеринга сцены)</summary>
        public static Char DefaultSymbol { get; } = ' ';
        /// <summary>(размер базовой ячейки координатной сетки)</summary>
        public static Vector GridSize { get; } = new Vector(6, 4);// >=(3,2)
        /// <summary>(размер базовой ячейки координатной сетки)</summary>
        public static Vector CurPosition = new Vector(0, 0);
        /// <summary>(масштаб окна консоли относительно экрана)</summary>
        public static float WindowScaler { get; } = 1.0f; // <=1
        /// <summary>(время между кадрами в секундах)</summary>
        public static float DeltaTime { get; } = 0.015f; 
        /// <summary>(время между кадрами в миллисекундах)</summary>
        public static int DeltaTimeMs { get; } = (int)(DeltaTime * 1000); 
        /// <summary>(размер окна консоли)</summary>
        public static Vector WindowSize { get; } = new Vector((int)Math.Abs(Console.LargestWindowWidth * WindowScaler), (int)Math.Abs(Console.LargestWindowHeight * WindowScaler));
        /// <summary>(список объектов на сцене)</summary>
        public static List<Object> SceneList = new List<Object>();
        /// <summary>(3D копия окна для хранения фонового цвета и № слоев)</summary>
        public static List<ObjectSingle>[,] WindowArray = new List<ObjectSingle>[(int)WindowSize.X, (int)WindowSize.Y];
        /// <summary>(объект для поочередного доступа к консоли)</summary>
        public static object DrawBlocker = new object();
        public static object ArrayBlocker = new object();

        

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
        public static int CheckNull(int value, int replace = 1)
        {
            return value != 0 ? value : replace;
        }

        /// <summary>
        /// starting method in another thread (запускает метод в отдельном потоке)
        /// </summary>
        /// <param name="methodToStart">method to start (метод для запуска)</param>
        public static void StartThread(ThreadStart methodToStart)
        {
            Thread thread = new Thread(methodToStart);
            thread.Start();
        }

        //========
        /// <summary>
        /// сheck position X overflow (проверка на выход позиции за пределы сцены)
        /// </summary>
        /// <param name="Pos">position X</param>
        protected static int CheckSizeX(int Pos)
        {
            return Math.Max(Math.Min(Pos , (int)WindowSize.X - 1), 0);
        }

        /// <summary>
        /// сheck position overflow (проверка на выход позиции за пределы сцены)
        /// </summary>
        /// <param name="Pos">position Y</param>
        protected static int CheckSizeY(int Pos)
        {
            return Math.Max(Math.Min(Pos , (int)WindowSize.Y - 1), 0);
        }

        /// <summary>
        /// сheck position overflow (проверка на выход позиции за пределы сцены)
        /// </summary>
        /// <param name="PosX">position X</param>
        /// <param name="PosY">position Y</param>
        protected static bool CheckPrint(int PosX, int PosY)
        {
            if (PosX > WindowSize.X - 1 || PosX < 0 || PosY > WindowSize.Y - 1 || PosY < 0)
            return false;
            else
            return true;
        }
    }
}
