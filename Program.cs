using System;
using System.Threading;
using System.Collections;

namespace ALB
{
    class Program
    {
        static ObjectSingle obj ;
        static ObjectSingle obh ;
        static ConsoleKeyInfo keyInfo;
        static View view = new View();
        static int dX = 1;
        static int dY = 1;
        /// <summary> стартовый метод </summary>
        static void Start()
        {
            obj = new ObjectSingle(ObjType.Car,1,null,null,5.GridToX(), 5.GridToY());
            obh = new ObjectSingle(ObjType.House, 0);
            obj.Position.X = PosTypeX.Left.PosTypeToX(obj.Size.X);
            obj.Position.Y = PosTypeY.Up.PosTypeToY(obj.Size.Y);
        }

        /// <summary> основной цикл </summary>
        static void Update()
        {
            obj.Position.X += dX;
            obj.Position.Y += dY;
        }
        
        static void GetKey()
        {
            while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        dX = 0; dY = -1; break;
                    case ConsoleKey.RightArrow:
                        dX = 1; dY = 0; break;
                    case ConsoleKey.DownArrow:
                        dX = 0; dY = 1; break;
                    case ConsoleKey.LeftArrow:
                        dX = -1; dY = 0; break;
                }
            }
        }

        //================
        static void Main()
        {
            Start();
            View.StartThread(GetKey);
            while (true)
            {
                Thread.Sleep(200);
                Update();
            }
            //Console.ReadKey();
        }
    }
}
