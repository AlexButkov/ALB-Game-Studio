using System;
using System.Collections.Generic;
using System.Threading;

namespace ALB
{
    class SampleGame : ALBGame
    {
        ObjectSingle plane;
        ObjectSingle car;
        ObjectSingle newCar;
        List<ObjectSingle> other;
        ObjectSingle house;
        float speed = 30f;


        public override void Start()
        {
            plane = new ObjectSingle(ObjType.Tree, 0, null, null, WindowSize.X, WindowSize.Y, ConsoleColor.White);
            house = new ObjectSingle(ObjType.House, 0.5f, null, null, 14.GridToX(), 14.GridToY(), ConsoleColor.Green);
            car = new ObjectSingle(ObjType.Car, 1, null, null, 10.GridToX(), 10.GridToY(), null);
            car.AlignWithSide(SideX.Right, SideY.Up);

            house.AlignWithSide(SideX.Right, SideY.Up);

            car.Position.X -= 1.GridToX();
            car.Position.Y += 1.GridToX();

            car.ChildList.Add(house);

            //car.CopyFrom(house);

            newCar = car.CopyThis();
            Thread.Sleep(DeltaTimeMs * 2);
            newCar.Layer = 2;
            newCar.Color = ConsoleColor.Red;

        }

        /// <summary> основной цикл </summary>
        public override void Update()
        {
            house.MoveAside(2, SideX.Left, SideY.Down);
            //car.MoveAside(speed, SideX.Right, SideY.Up);
            newCar.MoveByKey(speed);

            if (house.TriggerExit(out other, ObjType.Car))
            {
                other[0].Color = (ConsoleColor)new Random().Next(0, 15);
            }
            //control.MoveTowards(house, 1, car);
        }
    }
}
