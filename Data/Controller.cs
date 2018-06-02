﻿using System;
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
                MoveTowards(objectToMove, speed, new Vector(posX, posY));
            }
            else
            {
                Move(objectToMove, speed, keyX, keyY);
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
            Move(objectToMove, speed, kX, kY);
        }

        /// <summary>
        /// (перемещает этот объект в направлении целевого объекта)
        /// </summary>
        /// <param name="speed">(скорость перемещения)</param>
        /// <param name="targetObject">(целевой объект)</param>
        public static void MoveTowards(this ObjectSingle objectToMove, float speed, ObjectSingle targetObject)
        {
            int kX = (int)(targetObject.Position.X - objectToMove.Position.X);
            int kY = (int)(targetObject.Position.Y - objectToMove.Position.Y);
            Move(objectToMove, speed, kX, kY);       
        }

        /// <summary>
        /// (перемещает этот объект в направлении целевой позиции)
        /// </summary>
        /// <param name="speed">(скорость перемещения)</param>
        /// <param name="targetObject">(целевая позиция)</param>
        public static void MoveTowards(this ObjectSingle objectToMove, float speed, Vector targetPosition)
        {
            int kX = (int)(targetPosition.X - objectToMove.Position.X);
            int kY = (int)(targetPosition.Y - objectToMove.Position.Y);
            Move(objectToMove, speed, kX, kY);
        }
        //---private---
        /// <summary>
        /// (перемещает объект в направлении целевого вектора)
        /// </summary>
        /// <param name="speed">(скорость перемещения)</param>
        /// <param name="targetX">(целевой вектор X)</param>
        /// <param name="targetX">(целевой вектор Y)</param>
        static void Move(ObjectSingle objectToMove, float speed, int targetX, int targetY )
        {
            if (targetX != 0 || targetY != 0)
            {
                double atang = Math.Atan2(targetY, targetX);
                objectToMove.Position.X += ((float)Math.Cos(atang)).GridX() * speed * Model.DeltaTime;
                objectToMove.Position.Y += ((float)Math.Sin(atang)).GridY() * speed * Model.DeltaTime;
            }
        }

        //------
        static void ResetKey()
        {
            float mult = (keyX != 0 && keyY != 0) ? sqrt : 1;
            posX = (int)(ObjectToMove.Position.X + keyX * stopGap.GridX() * mult);
            posY = (int)(ObjectToMove.Position.Y + keyY * stopGap.GridY() * mult);
        }

        static void GetKey()
        {
            while (true)
            {
                //ConsoleKeyInfo InfoKey = Console.ReadKey(true);
                //if (InfoKey != null && stopDelta != null)
                switch (isFirst ? keyFirst = Console.ReadKey(true).Key : keySecond)
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
                switch (keySecond = Console.ReadKey(true).Key)
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
        /// writes this string to position inside object (пишет эту строку на позиции внутри объекта)
        /// </summary>
        /// <param name="objectTarget">target object (целевой объект)</param>
        /// <param name="positionX">object-dependent X-axis position coordinate (координата позиции по оси X относительно центра объекта)</param>
        /// <param name="positionY">object-dependent Y-axis position coordinate (координата позиции по оси Y относительно центра объекта)</param>
        /// <param name="stringColor">string color (цвет строки)</param>
        public static void WriteTo(this string str, ObjectSingle objectTarget, int positionX = 0, int positionY = 0, ConsoleColor stringColor = ConsoleColor.Black)
        {
            int max = (int)Model.MainTimer.ElapsedMilliseconds + Model.FixedTimeMs;
            while (!objectTarget.IsDrawn && (int)Model.MainTimer.ElapsedMilliseconds < max)
            { }
            str.WriteTo((int)(objectTarget.Position.X + positionX), (int)(objectTarget.Position.Y + positionY), objectTarget.Color, stringColor);
            objectTarget.IsTextured = true;
        }
        /// <summary>
        /// writes this string to position (пишет эту строку на позиции)
        /// </summary>
        /// <param name="positionX">X-axis position coordinate (координата позиции по оси X)</param>
        /// <param name="positionY">Y-axis position coordinate (координата позиции по оси Y)</param>
        /// <param name="backColor">background color (цвет фона)</param>
        /// <param name="stringColor">string color (цвет строки)</param>
        public static void WriteTo(this string str, int positionX = 0, int positionY = 0, ConsoleColor backColor = ConsoleColor.White, ConsoleColor stringColor = ConsoleColor.Black)
        {
            lock (Model.ArrayBlocker)
            {
                int x = positionX + (int)Model.WindowSize.X / 2 - str.Length / 2;
                int y = positionY + (int)Model.WindowSize.Y / 2;
                if (x > (int)Model.WindowSize.X - str.Length)
                {
                    str = null;
                }
                if (Model.CheckPrint(x, y))
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
