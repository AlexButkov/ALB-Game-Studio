using System;
using System.Collections.Generic;
using System.Threading;

namespace ALB
{
    /// <summary> 
    /// sample game logic (образец игровой логики)
    /// </summary>
    class SampleGame : ALBGame
    {
        ObjectSingle plane;
        ObjectSingle car;
        ObjectSingle textA;
        ObjectSingle textB;
        List<ObjectSingle> other;
        ObjectSingle house;
        float speed = 15f;
        string message;

        /// <summary> gets called on startup only (выполняется только при запуске) </summary>
        public override void Start()
        {
            plane = new ObjectSingle(Preset.plane, ConsoleColor.White);
            textA = new ObjectSingle(Preset.boxXL, ConsoleColor.Gray, null, 10f, null, null, null, 2.GridY());
            textB = new ObjectSingle(Preset.boxXL, ConsoleColor.Black, null, textA.Layer - 0.01f, textA.Position.X + 1, textA.Position.Y + 1, null, 2.GridY());
            textA.ChildList.Add(textB);

            house = new ObjectSingle(Preset.boxXL, ConsoleColor.Green, null, 0.5f, null, null, 14.GridX(), 14.GridY());
            car = new ObjectSingle(Preset.boxS, ConsoleColor.Blue, null, 1, null, null, 10.GridX(), 10.GridY());
            car.AlignWithSide(SideX.right, SideY.up);
            house.AlignWithSide(SideX.right, SideY.up);
            car.Position.X -= 1.GridX();
            car.Position.Y += 1.GridX();
        }

        /// <summary> gets called every frame (выполняется для каждого кадра) </summary>
        public override void Update()
        {
            message = "Hello! Press direction key to move.";
            message.WriteTo(textA);
            house.MoveTowards(2, car);
            //car.MoveAside(speed, SideX.right, SideY.up);
            car.MoveByWASD(speed);

            if (house.TriggerExit(out other))
            {
                other[0].Color = (ConsoleColor)new Random().Next(0, 15);
            }
        }
    }
}
