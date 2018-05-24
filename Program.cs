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
        static float speed = 5;
        static View view = new View();
        static Controller control = new Controller();
        /// <summary> стартовый метод </summary>
        static void Start()
        {
            car = new ObjectSingle(ObjType.Car,1,null,null,3.GridToX(), 2.GridToY());
            house = new ObjectSingle(ObjType.House, 0);
            car.AlignWithSide(SideX.Middle, SideY.Middle);
            //car.ChildList.Add(house);
        }

        /// <summary> основной цикл </summary>
        static void Update()
        {
            control.MoveAside(car, speed, SideX.Left );
            //control.MoveByKey(car, speed);

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
