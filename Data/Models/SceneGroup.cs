using System;

namespace ALB
{
    /// <summary>
    /// Класс для группы объектов в игре
    /// </summary>
    class SceneGroup : SceneObject
    {
        public int GapX { get { return gapX; } set { CheckX(); gapX =  Math.Abs(value); } }
        public int GapY { get { return gapY; } set { CheckY(); gapY =  Math.Abs(value); } }
        public int CountX { get { return countX; } set { CheckX(); countX = Math.Abs(value); } }
        public int CountY { get { return countY; } set { CheckY(); countY = Math.Abs(value); } }

        int gapX;
        int gapY;
        int countX;
        int countY;

        //========
        /// <summary>
        /// Экземпляр класса для группы объектов в игре (в первом параметре указывается объект перечисляемого типа "Scene", иначе создается объект с нулевыми значениями)
        /// </summary>
        //public MultiObject() { } // : base() { }
        /// <summary>
        /// Экземпляр класса для группы объектов (в первом параметре указывается объект перечисляемого типа "Scene", иначе создается объект с нулевыми значениями)
        /// </summary>
        /// <param name="ObjectType">тип объекта</param>
        /// <param name="x">позиция по оси X</param>
        /// <param name="y">позиция по оси Y</param>
        /// <param name="width">ширина</param>
        /// <param name="height">высота</param>
        /// <param name="color">цвет</param>
        /// <param name="gapX"></param>
        /// <param name="gapY"></param>
        /// <param name="countX"></param>
        /// <param name="countY"></param>
        public SceneGroup(Scene ObjectType, SceneObject parentObject = null , PositionX x = 0, PositionY y = 0, int width = 0, int height = 0, ConsoleColor color = 0, int gapX = 0 , int gapY = 0, int countX = 0, int countY = 0)
            :base(ObjectType, parentObject, x , y, width, height , color)
        {
            switch (ObjectType)
            {   //характеристики объектов по умолчанию
                case Scene.Car:     { this.gapX = gapX != 0 ? gapX : 05; this.gapY = gapY != 0 ? gapY : 05; this.countX = countX != 0 ? countX : 05; this.countY = countY != 0 ? countY : 05; } break;
                case Scene.Wheel:   { this.gapX = gapX != 0 ? gapX : 05; this.gapY = gapY != 0 ? gapY : 05; this.countX = countX != 0 ? countX : 05; this.countY = countY != 0 ? countY : 05; } break;
                case Scene.Line:    { this.gapX = gapX != 0 ? gapX : 05; this.gapY = gapY != 0 ? gapY : 05; this.countX = countX != 0 ? countX : 05; this.countY = countY != 0 ? countY : 05; } break;
                case Scene.House:   { this.gapX = gapX != 0 ? gapX : 05; this.gapY = gapY != 0 ? gapY : 05; this.countX = countX != 0 ? countX : 05; this.countY = countY != 0 ? countY : 05; } break;
                case Scene.Tree:    { this.gapX = gapX != 0 ? gapX : 05; this.gapY = gapY != 0 ? gapY : 05; this.countX = countX != 0 ? countX : 05; this.countY = countY != 0 ? countY : 05; } break;
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
        protected override void CheckX(PositionX PosX = PositionX.Default)
        {
            int sum = Width + (Width + GapX) * (CountX - 1);
            int newSum = Math.Min(sum, MaxWidth);

            if (sum != newSum)
            gapX = (newSum - Width)/ NullCheck(CountX - 1) - Width;

            if (Width > MaxWidth)
                width = MaxWidth;

            Switcher((int)PosX, PosX, ref posX, X, ref x, MaxWidth, newSum);
        }
        /// <summary>
        /// Проверка значений по оси Y на корректность
        /// </summary>
        /// <param name="PosY">позиция по оси Y</param>
        /// <returns></returns>
        protected override void CheckY(PositionY PosY = PositionY.Default)
        {
            int sum = Height + (Height + GapY) * (CountY - 1);
            int newSum = Math.Min(sum, MaxHeight);

            if (sum != newSum)
            gapX = (newSum - Height) / NullCheck(CountY - 1) - Height;

            if (Height > MaxHeight)
                height = MaxHeight;

            Switcher((int)PosY, PosY, ref posY, Y, ref y, MaxHeight, newSum);
        }
    }
}
