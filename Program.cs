using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace ALB
{
    class Program : Model
    {
        static ObjectSingle car ;
        static List<ObjectSingle> other ;
        static ObjectSingle house ;
        static float speed = 5f;
        static View view = new View();
        /// <summary> стартовый метод </summary>
        static void Start()
        {
            car = new ObjectSingle(ObjType.Car,1,null,null,3.GridToX(), 2.GridToY());
            house = new ObjectSingle(ObjType.House,0,0);
            car.AlignWithSide(SideX.Left, SideY.Middle);
            //house.AlignWithSide(SideX.Left, SideY.Up);
            car.ChildList.Add(house);
            Thread.Sleep(3000);
            car.CopyFrom(house);
            //car.Color = ConsoleColor.Cyan;
        }

        /// <summary> основной цикл </summary>
        static void Update()
        {
            house.MoveAside(1, SideX.Left );
            car.MoveByKey(speed);

            if (house.TriggerExit(out other, ObjType.Car))
            {
                other[0].Color = (ConsoleColor)new Random().Next(1,16);
            }
            //control.MoveTowards(house, 1, car);
        }
        

        //================
        static void Main()
        {
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
