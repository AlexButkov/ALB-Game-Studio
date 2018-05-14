using System;

namespace ALB
{
    /// <summary>
    /// Базовый класс для объекта в игре
    /// </summary>
    class ObjectSingle : Model
    {
        public int X { get { return x; } set { CheckX(); } }
        public int Y { get { return y; } set { CheckY(); } }
        public int Width { get { return width;  } set { CheckX(); width = Math.Abs(value); } }
        public int Height { get { return height; } set { CheckY(); height =  Math.Abs(value); } }
        public ConsoleColor Color { get { return color; } set { color = value; } }
        public PositionX PosX { get { return posX; } set { CheckX(); posX = value; } }
        public PositionY PosY { get { return posY; } set { CheckY(); posY = value; } }
        //----
        protected int x;
        protected int y;
        protected int width;
        protected int height;
        protected ConsoleColor color;
        protected PositionX posX = PositionX.Default;
        protected PositionY posY = PositionY.Default;

        //========
        /// <summary>
        /// конструктор экземпляра базового класса для объекта в игре (в первом параметре указывается объект перечисляемого типа "Scene", иначе создается объект с нулевыми значениями)
        /// </summary>
        public ObjectSingle() { }
        /// <summary>
        /// конструктор экземпляра базового класса для объекта в игре (в первом параметре указывается объект перечисляемого типа "Scene", иначе создается объект с нулевыми значениями)
        /// </summary>
        /// <param name="objectType">тип объекта</param>
        /// <param name="parentObject">ограничивающий движение объект</param>
        /// <param name="x">позиция по оси X</param>
        /// <param name="y">позиция по оси Y</param>
        /// <param name="width">ширина</param>
        /// <param name="height">высота</param>
        /// <param name="color">цвет</param>
        public ObjectSingle(Scene objectType, ObjectSingle parentObject = null, PositionX x = 0, PositionY y = 0, int width = 0, int height = 0, ConsoleColor color = 0)
        {
            switch (objectType)
            {   //характеристики объектов по умолчанию
                case Scene.Car:      { this.x = x != 0 ? (int)x : 40; this.y = y != 0 ? (int)y : 20; this.width = width != 0 ? width : 12; this.height = height != 0 ? height : 08; this.color = color != 0 ? color : ConsoleColor.Blue; } break;
                case Scene.Wheel:    { this.x = x != 0 ? (int)x : 39; this.y = y != 0 ? (int)y : 21; this.width = width != 0 ? width : 01; this.height = height != 0 ? height : 01; this.color = color != 0 ? color : ConsoleColor.DarkGray; } break;
                case Scene.Line:     { this.x = x != 0 ? (int)x : 20; this.y = y != 0 ? (int)y : 10; this.width = width != 0 ? width : 02; this.height = height != 0 ? height : 05; this.color = color != 0 ? color : ConsoleColor.Gray; } break;
                case Scene.House:    { this.x = x != 0 ? (int)x : 05; this.y = y != 0 ? (int)y : 20; this.width = width != 0 ? width : 15; this.height = height != 0 ? height : 15; this.color = color != 0 ? color : ConsoleColor.DarkYellow; } break;
                case Scene.Tree:     { this.x = x != 0 ? (int)x : 40; this.y = y != 0 ? (int)y : 10; this.width = width != 0 ? width : 08; this.height = height != 0 ? height : 08; this.color = color != 0 ? color : ConsoleColor.Green; } break;
                default: break;
            }
            CheckX(x);
            CheckY(y);
        }

        //========
        /// <summary>
        /// Проверка значений по оси X на корректность
        /// </summary>
        /// <param name="PosX">позиция по оси X</param>
        /// <returns></returns>
        protected virtual void CheckX(PositionX PosX = PositionX.Default)
        {
            int sum = Width; //override
            
            int newSum = Math.Min(sum, MaxWidth);
            if (sum != newSum)
                width = newSum; //override
            Switcher((int)PosX, PosX, ref posX, X, ref x, MaxWidth, newSum);
        }
        /// <summary>
        /// Проверка значений по оси Y на корректность
        /// </summary>
        /// <param name="PosY">позиция по оси Y</param>
        /// <returns></returns>
        protected virtual void CheckY(PositionY PosY = PositionY.Default)
        {
            int sum = Height; //override

            int newSum = Math.Min(sum, MaxHeight);
            if (sum != newSum)
                height = newSum; //override
            Switcher((int)PosY, PosY, ref posY, Y, ref y, MaxHeight, newSum);
        }

        /// <summary>
        /// Часть кода, повторяющаяся в методах Check
        /// </summary>
        /// <typeparam name="T">PositionX/PositionY</typeparam>
        /// <param name="PosT">(int)PosX/(int)PosY</param>
        /// <param name="PosType">PosX/PosY</param>
        /// <param name="posType">posX/posY</param>
        /// <param name="Pos">X/Y</param>
        /// <param name="pos">x/y</param>
        /// <param name="MaxVal">MaxWidth/MaxHeight</param>
        /// <param name="newSum">newSum</param>
        protected virtual void Switcher<T>(int PosT, T PosType, ref T posType, int Pos, ref int pos, int MaxVal, int newSum)
        {
            switch (PosT)
            {
                case 0:
                    pos = Math.Max(Math.Min(MaxVal - newSum / 2, Math.Abs(Pos)), 0 + newSum / 2); // убрать модуль
                    return;
                case 1:
                    pos = Math.Abs(0) + newSum / 2;//Buffer
                    break;
                case 2:
                    pos = Math.Max(MaxVal / 2, 0); // - newSum / 2
                    break;
                case 3:
                    pos = Math.Max(MaxVal - newSum / 2 - Math.Abs(0), 0);//BufferX
                    break;
                default:
                    goto case 0;
            }
            posType = PosType;
        }
    }
}
