using System;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;

namespace ALB
{
    /// <summary>
    /// accepts input and provides game objects movement (принимает входные данные и обеспечивает перемещение игровых объектов)
    /// </summary>
    static class Controller
    {
        /// <summary>
        /// Last pressed key (последняя нажатая клавиша)
        /// </summary>
        public static ConsoleKey? LastKey
        {
            get
            {
                if (!getKeyIsOn)
                {
                    Model.StartThread(GetKey);
                    getKeyIsOn = true;
                }
                return lastKey;
            }
            private set { lastKey = value; }
        }
        //---
        static ConsoleKey? lastKey;
        static Stopwatch keyTimer = new Stopwatch();
        static ConsoleKey? keyFirst;
        static ConsoleKey? keySecond;
        static bool isFirst;
        static bool getKeyIsOn;
        static int keyDelta = 100;
        static float stopGap;
        static ObjectSingle ObjectToMove;
        static int keyX;
        static int keyY;
        static float posX;
        static float posY;
        static float sqrt = (float)(1 / Math.Sqrt(2));
        
        //======


        /// <summary>
        /// (перемещает этот объект в направлении нажатой клавиши-стрелки или WASD)
        /// </summary>
        /// <param name="speed">(скорость перемещения)</param>
        /// <param name="stopDistance">(Дистанция до остановки. В случае null остановки нет.)</param>
        public static void MoveByWASD(this ObjectSingle objectToMove, float speed, float? stopDistance = null )
        {
            if (!getKeyIsOn)
            {
                Model.StartThread(GetKey);
                getKeyIsOn = true;
            }
            if (stopDistance != null)
            {
                stopGap = (float)stopDistance;
                ObjectToMove = objectToMove;
                if (LastKey != null && LastKey != ConsoleKey.Enter)
                {
                    MoveTowards(objectToMove, speed, new Vector(posX, posY));
                }
            }
            else
            {
                Move(objectToMove, speed, keyX.GridToX(), keyY.GridToY());
            }
            
        }

        /// <summary>
        /// (перемещает этот объект в выбранном направлении)
        /// </summary>
        /// <param name="speed">(скорость перемещения)</param>
        /// <param name="typeX">(<see cref="SideX"/> направление по оси X)</param>
        /// <param name="typeY">(<see cref="SideY"/> направление по оси X)</param>
        public static void MoveAside(this ObjectSingle objectToMove, float speed, SideX typeX = SideX.middle, SideY typeY = SideY.middle)
        {
            int kX = 0;
            int kY = 0;
            switch (typeX)
            {
                case SideX.left:
                    kX = -1; break;
                case SideX.middle:
                    kX = 0; break;
                case SideX.right:
                    kX = 1; break;
            }
            switch (typeY)
            {
                case SideY.up:
                    kY = -1; break;
                case SideY.middle:
                    kY = 0; break;
                case SideY.down:
                    kY = 1; break;
            }
            Move(objectToMove, speed, kX.GridToX(), kY.GridToY());
        }

        /// <summary>
        /// (перемещает этот объект в направлении целевого объекта)
        /// </summary>
        /// <param name="speed">(скорость перемещения)</param>
        /// <param name="targetObject">(целевой объект)</param>
        public static void MoveTowards(this ObjectSingle objectToMove, float speed, ObjectSingle targetObject)
        {
            int kX = (int)targetObject.Position.X - (int)objectToMove.Position.X;
            int kY = (int)targetObject.Position.Y - (int)objectToMove.Position.Y;
            Move(objectToMove, speed, kX, kY);       
        }

        /// <summary>
        /// (перемещает этот объект в направлении целевой позиции)
        /// </summary>
        /// <param name="speed">(скорость перемещения)</param>
        /// <param name="targetObject">(целевая позиция)</param>
        public static void MoveTowards(this ObjectSingle objectToMove, float speed, Vector targetPosition)
        {
            int kX = (int)targetPosition.X - (int)objectToMove.Position.X;
            int kY = (int)targetPosition.Y - (int)objectToMove.Position.Y;
            Move(objectToMove, speed, kX, kY);
        }
        //---private---
        /// <summary>
        /// (перемещает объект в направлении целевого вектора)
        /// </summary>
        /// <param name="speed">(скорость перемещения)</param>
        /// <param name="targetX">(целевой вектор X)</param>
        /// <param name="targetY">(целевой вектор Y)</param>
        static void Move(ObjectSingle objectToMove, float speed, int targetX, int targetY)
        {
            objectToMove.Position.PrevX = (int)objectToMove.Position.X;
            objectToMove.Position.PrevY = (int)objectToMove.Position.Y;

            if (targetX != 0 || targetY != 0)
            {
                float gX = targetX.XToGrid();
                float gY = targetY.YToGrid();
                double atang = Math.Atan2(gY, gX);
                float velocityX = (float)Math.Cos(atang) * speed * Model.DeltaTime;
                float velocityY = (float)Math.Sin(atang) * speed * Model.DeltaTime;
                objectToMove.Position.X += Math.Sign(gX) * Math.Min(Math.Abs(targetX), Math.Abs(velocityX).GridToX());
                objectToMove.Position.Y += Math.Sign(gY) * Math.Min(Math.Abs(targetY), Math.Abs(velocityY).GridToY());
            }
        }

        //------
        static void ResetKey()
        {
            float mult = (keyX != 0 && keyY != 0) ? sqrt : 1;
            posX = (int)(ObjectToMove.Position.X + keyX * stopGap.GridToX() * mult);
            posY = (int)(ObjectToMove.Position.Y + keyY * stopGap.GridToY() * mult);
        }

        static void GetKey()
        {
            while (true)
            {
                switch (isFirst ? keyFirst = LastKey = Console.ReadKey(true).Key : keySecond)
                {
                    case ConsoleKey.W:
                        goto case ConsoleKey.UpArrow;
                    case ConsoleKey.S:
                        goto case ConsoleKey.DownArrow;
                    case ConsoleKey.D:
                        goto case ConsoleKey.RightArrow;
                    case ConsoleKey.A:
                        goto case ConsoleKey.LeftArrow;
                    case ConsoleKey.UpArrow:
                        keyX = 0; keyY = -1; goto case null;
                    case ConsoleKey.DownArrow:
                        keyX = 0; keyY = 1; goto case null;
                    case ConsoleKey.RightArrow:
                        keyX = 1; keyY = 0; goto case null;
                    case ConsoleKey.LeftArrow:
                        keyX = -1; keyY = 0; goto case null;
                    case null:
                        keyTimer.Reset(); keyTimer.Start(); isFirst = true;
                        if (stopGap != 0)
                        {
                            ResetKey();
                        }
                        break;
                }
                switch (keySecond = LastKey = Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                        goto case ConsoleKey.UpArrow;
                    case ConsoleKey.S:
                        goto case ConsoleKey.DownArrow;
                    case ConsoleKey.D:
                        goto case ConsoleKey.RightArrow;
                    case ConsoleKey.A:
                        goto case ConsoleKey.LeftArrow;
                    case ConsoleKey.UpArrow:
                        if (keyTimer.ElapsedMilliseconds < keyDelta)
                        { keyY = -1; goto default; }
                        else
                        { goto case null; }
                    case ConsoleKey.DownArrow:
                        if (keyTimer.ElapsedMilliseconds < keyDelta)
                        { keyY = 1; goto default; }
                        else
                        { goto case null; }
                    case ConsoleKey.RightArrow:
                        if (keyTimer.ElapsedMilliseconds < keyDelta)
                        { keyX = 1; goto default; }
                        else
                        { goto case null; }
                    case ConsoleKey.LeftArrow:
                        if (keyTimer.ElapsedMilliseconds < keyDelta)
                        { keyX = -1; goto default; }
                        else
                        { goto case null; }
                    case null:
                        isFirst = false; goto default;
                    default:
                        if (stopGap != 0)
                        {
                            ResetKey();
                        }
                        break;
                }
            }
        }
        /// <summary>
        /// Writes this string to position inside object. For <see cref="ALBGame.Update"/> method only. (Вписывает эту строку на позиции внутри объекта. Только для <see cref="ALBGame.Update"/> метода.)
        /// </summary>
        /// <param name="objectTarget">target object (целевой объект)</param>
        /// <param name="positionX">object-dependent X-axis position coordinate (координата позиции по оси X относительно центра объекта)</param>
        /// <param name="positionY">object-dependent Y-axis position coordinate (координата позиции по оси Y относительно центра объекта)</param>
        /// <param name="stringColor">string color (цвет строки)</param>
        public static void WriteTo(this string str, ObjectSingle objectTarget, int positionX = 0, int positionY = 0, ConsoleColor stringColor = ConsoleColor.Black)
        {
            if (objectTarget.Position.PrevX == (int)objectTarget.Position.X && objectTarget.Position.PrevY == (int)objectTarget.Position.Y)
            {
                str.WriteTo((int)(objectTarget.Position.X + positionX), (int)(objectTarget.Position.Y + positionY), objectTarget.Color, stringColor);
            }
            if (!objectTarget.IsTextured)
            {
                objectTarget.IsTextured = true;
            }
        }
        /// <summary>
        /// Writes this string to position inside object. For <see cref="ALBGame.Update"/> method only. (Вписывает эту строку на позиции внутри объекта. Только для <see cref="ALBGame.Update"/> метода.)
        /// </summary>
        /// <param name="positionX">X-axis position coordinate (координата позиции по оси X)</param>
        /// <param name="positionY">Y-axis position coordinate (координата позиции по оси Y)</param>
        /// <param name="backColor">background color (цвет фона)</param>
        /// <param name="stringColor">string color (цвет строки)</param>
        public static void WriteTo(this string str, int positionX = 0, int positionY = 0, ConsoleColor backColor = ConsoleColor.White, ConsoleColor stringColor = ConsoleColor.Black)
        {
            int x = positionX + (int)Model.WindowSize.X / 2 - str.Length / 2;
            int y = positionY + (int)Model.WindowSize.Y / 2;
            bool isRight = x <= (int)Model.WindowSize.X - str.Length;
            
            if (isRight && Model.CheckPrint(x, y))
            {
                lock (Model.ConsoleBlocker)
                {
                  Console.SetCursorPosition(x, y);
                  Console.BackgroundColor = backColor;
                  Console.ForegroundColor = stringColor;
                  Console.Write(str);
                }
            }
        }
        //---
    }
}
