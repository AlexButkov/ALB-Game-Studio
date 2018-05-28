using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace ALB
{
    class Program : Model
    {
        static ObjectSingle plane;
        static ObjectSingle car ;
        static List<ObjectSingle> other ;
        static ObjectSingle house ;
        static float speed = 8f;
        static View view = new View();
        /// <summary> стартовый метод </summary>
        static void Start()
        {
            plane = new ObjectSingle(ObjType.Tree, -1, null, null, WindowSize.X, WindowSize.Y, ConsoleColor.White);
            house = new ObjectSingle(ObjType.House, 0, null, null, 15.GridToX(), 15.GridToY(), ConsoleColor.Green);
            car = new ObjectGroup(ObjType.Car,1,null,null,2.GridToX(), 2.GridToY(), null ,4.GridToX(), 4.GridToY(), 2, 2);
            //car.AlignWithSide(SideX.Left, SideY.Middle);
            house.AlignWithSide(SideX.Right, SideY.Up);
            car.ChildList.Add(house);
            //Thread.Sleep(3000);
            //car.CopyFrom(house);
            //car.Color = ConsoleColor.Cyan;
        }

        /// <summary> основной цикл </summary>
        static void Update()
        {
            house.MoveAside(2, SideX.Left, SideY.Down );
            //car.MoveAside(speed, SideX.Right, SideY.Up);
            car.MoveByKey(speed);

            if (house.TriggerExit(out other, ObjType.Car))
            {
                other[0].Color = (ConsoleColor)new Random().Next(0,15);
            }
            //control.MoveTowards(house, 1, car);
        }
        

        //================
        static void Main()
        {
            view.Initializer();
            Thread.Sleep(DeltaTimeMs);
            Start();
            while (true)
            {
                Update();
                Thread.Sleep(DeltaTimeMs);
            }
            //Console.ReadKey();
        }
    }
}
