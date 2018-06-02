using System;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;

namespace ALB
{
    /// <summary>
    /// contains main data and entry point of a application (содержит основные данные и точку входа приложения)
    /// </summary>
    class Model 
    {
        /// <summary>(базовый цвет для рендеринга сцены)</summary>
        public const ConsoleColor DefaultColor = ConsoleColor.Black;
        /// <summary>(символ для рендеринга сцены)</summary>
        public static Char DefaultSymbol { get; } = ' ';
        /// <summary>(размер базовой ячейки координатной сетки)</summary>
        public static Vector GridSize { get; } = new Vector(6, 4);// >=(3,2)
        /// <summary>(масштаб окна консоли относительно экрана)</summary>
        public static float WindowScaler { get; } = 1.0f; // <=1
        /// <summary>(размер окна консоли)</summary>
        public static Vector WindowSize { get; } = new Vector((int)Math.Abs(Console.LargestWindowWidth * WindowScaler), (int)Math.Abs(Console.LargestWindowHeight * WindowScaler));
        /// <summary>(минимальное время между кадрами в миллисекундах)</summary>
        public static int FixedTimeMs { get; } = 16;
        /// <summary>(текущее время между кадрами в секундах)</summary>
        public static float DeltaTime { get; private set; } = FixedTimeMs / 1000;
        /// <summary>(динамическое время между кадрами в миллисекундах)</summary>
        public static int SleepTime { get; private set; }
        /// <summary>(список объектов на сцене)</summary>
        public static List<Object> SceneList = new List<Object>();
        /// <summary>(3D копия окна для хранения фонового цвета и № слоев)</summary>
        public static List<float[]>[,] WindowArray = new List<float[]>[(int)WindowSize.X, (int)WindowSize.Y];
        /// <summary>(объект для поочередного доступа)</summary>
        public static object ArrayBlocker = new object();
        /// <summary>main game timer (главный игровой таймер)</summary>
        public static Stopwatch MainTimer { get; } = new Stopwatch();

        //---
        static View view = new View();

        //================
        /// <summary>
        /// entry point of a application (точка входа приложения)
        /// </summary>
        static void Main()
        {
            MainTimer.Start();
            view.Initializer();
            view.StartSum();
            int currentTime;
            while (true)
            {
                currentTime = (int)MainTimer.ElapsedMilliseconds;
                view.UpdateSum();
                SleepTime = Math.Max(0,FixedTimeMs - ((int)MainTimer.ElapsedMilliseconds - currentTime));
                DeltaTime = (float)Math.Max(FixedTimeMs, (int)MainTimer.ElapsedMilliseconds - currentTime) / 1000;
                Thread.Sleep(SleepTime);
            }
        }
        //================
        /// <summary>
        /// (Проверяет наличие нулевого значения)
        /// </summary>
        /// <param name="value">Принимаемое значение для проверки</param>
        /// <param name="replace">Возвращаемое значение в случае нуля</param>
        /// <returns></returns>
        public static int CheckNull(int value, int replace = 1)
        {
            return value != 0 ? value : replace;
        }

        /// <summary>
        /// launches method in another thread (запускает метод в отдельном потоке)
        /// </summary>
        /// <param name="methodToStart">method to start (метод для запуска)</param>
        public static void StartThread(ThreadStart methodToStart)
        {
            Thread thread = new Thread(methodToStart);
            thread.Start();
        }

        //========
        /// <summary>
        /// сhecks position X overflow (проверка на выход позиции за пределы сцены)
        /// </summary>
        /// <param name="Pos">position X</param>
        public static int CheckSizeX(int Pos)
        {
            return Math.Max(Math.Min(Pos , (int)WindowSize.X - 1), 0);
        }

        /// <summary>
        /// сhecks position overflow (проверка на выход позиции за пределы сцены)
        /// </summary>
        /// <param name="Pos">position Y</param>
        public static int CheckSizeY(int Pos)
        {
            return Math.Max(Math.Min(Pos , (int)WindowSize.Y - 1), 0);
        }

        /// <summary>
        /// сhecks position overflow (проверка на выход позиции за пределы сцены)
        /// </summary>
        /// <param name="PosX">position X</param>
        /// <param name="PosY">position Y</param>
        public static bool CheckPrint(int PosX, int PosY)
        {
            if (PosX > WindowSize.X - 1 || PosX < 0 || PosY > WindowSize.Y - 1 || PosY < 0)
            return false;
            else
            return true;
        }
    }
}
