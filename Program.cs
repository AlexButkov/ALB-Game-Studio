using System;
using System.Threading;
using System.Collections;
using System.Diagnostics;

namespace ALB
{
    class Program : Model
    {
        static ObjectSingle obj ;
        static ObjectSingle obh ;
        static float speed = 1;
        static View view = new View();
        static Controller control = new Controller();
        /// <summary> стартовый метод </summary>
        static void Start()
        {
            obj = new ObjectSingle(ObjType.Car,1,null,null,3.GridToX(), 2.GridToY());
            obh = new ObjectSingle(ObjType.House, 0);
            obj.AlignWithSide(SideX.Middle, SideY.Middle);
            obj.ChildList.Add(obh);
        }

        /// <summary> основной цикл </summary>
        static void Update()
        {
            control.MoveByKey(obj, speed);
            //control.MoveTowards(obh, speed, new Vector(30,30));
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
