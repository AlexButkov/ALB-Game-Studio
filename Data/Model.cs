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
        //---- public ----
        /// <summary>(базовый цвет для рендеринга сцены)</summary>
        public const ConsoleColor DefaultColor = ConsoleColor.Black;
        /// <summary>(символ для рендеринга сцены)</summary>
        public static Char DefaultSymbol { get; } = ' ';
        /// <summary>(размер базовой ячейки координатной сетки)</summary>
        public static Vector GridSize { get; } = new Vector(6, 4);// >=(3,2)
        /// <summary>(масштаб окна консоли относительно экрана)</summary>
        public static float WindowScaler { get; } = 1; // <=1
        /// <summary>(размер окна консоли)</summary>
        public static Vector WindowSize { get; } = new Vector(Math.Min((int)Math.Abs(Console.LargestWindowWidth * WindowScaler), 240), Math.Min((int)Math.Abs(Console.LargestWindowHeight * WindowScaler), 84));
        /// <summary>main game timer (главный игровой таймер)</summary>
        public static Stopwatch MainTimer { get; } = new Stopwatch();
        /// <summary>(минимальное время между кадрами в миллисекундах)</summary>
        public static int FixedTimeMs { get; } = 16;
        /// <summary>(текущее время между кадрами в секундах)</summary>
        public static float DeltaTime { get; private set; } = FixedTimeMs / 1000;
        /// <summary>(динамическое время между кадрами в миллисекундах)</summary>
        public static int SleepTime = FixedTimeMs;
        /// <summary>(список объектов на сцене)</summary>
        public static List<Object> SceneList = new List<Object>();
        /// <summary>(3D копия окна для хранения фонового цвета и № слоев)</summary>
        public static List<float[]>[,] WindowArray = new List<float[]>[(int)WindowSize.X, (int)WindowSize.Y];
        /// <summary>(объект для поочередного рендеринга)</summary>
        public static object ConsoleBlocker = new object();
        /// <summary>(объект для поочередного доступа к массиву <see cref="WindowArray"/>)</summary>
        public static Mutex ArrayBlocker = new Mutex();

        //---- private ----
        /// <summary>writes exception info into console if true (выводит информацию об ошибке в окно консоли если true)</summary>
        static bool DoExceptionView { get; } = false;
        static object arrayBlocker = new object();
        static View view = new View();
        //================
        /// <summary>
        /// entry point of a application (точка входа приложения)
        /// </summary>
        static void Main()
        {
            void MainMethod()
            {
                MainTimer.Start();
                view.MainInitialize();
                view.StartSum();
                Thread.Sleep(SleepTime);
                int currentTime;
                while (true)
                {
                    currentTime = (int)MainTimer.ElapsedMilliseconds;
                    view.UpdateSum();
                    SleepTime = (FixedTimeMs - ((int)MainTimer.ElapsedMilliseconds - currentTime)).Range(0, FixedTimeMs);
                    DeltaTime = (float)Math.Max(FixedTimeMs, (int)MainTimer.ElapsedMilliseconds - currentTime) / 1000;
                    Thread.Sleep(SleepTime);
                }
            }
            if (!DoExceptionView)
            {
                MainMethod();
            }
            else
            {
                try
                {
                    MainMethod();
                }
                catch (Exception Ex)
                {
                    string message = Ex.Source + " error! " + Ex.Message;
                    try
                    {
                        ObjectSingle plane = new ObjectSingle(Preset.plane, ConsoleColor.White);
                        while (true)
                        {
                            message.WriteTo(plane);
                        }
                    }
                    catch
                    {
                        Console.WriteLine(message);
                        Console.ReadKey();
                    }
                }
            }
        }
        //================
        /// <summary>
        /// (Проверяет наличие нулевого значения)
        /// </summary>
        /// <param name="value">Принимаемое значение для проверки</param>
        /// <param name="replace">Возвращаемое значение в случае нуля</param>
        /// <returns></returns>
        public static int ZeroCheck(int value, int replace = 1)
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
        /// <summary> returns random color except givens. (возвращает случайный цвет, за исключением данных)</summary>
        public static ConsoleColor RandomColor(params ConsoleColor[] colors)
        {
            ConsoleColor color = DefaultColor;
            bool isWrong = true;
            while (isWrong)
            {
                color = (ConsoleColor)new Random().Next(0, 16);
                if (colors.Length > 0)
                {
                    for (int i = 0; i < colors.Length; i++)
                    {
                        if (color == colors[i])
                        {
                            break;
                        }
                        if (i == colors.Length - 1)
                        {
                            isWrong = false;
                        }
                    }
                }
                else
                {
                    isWrong = false;
                }
            }
            return color;
        }
        //---
    }
}
