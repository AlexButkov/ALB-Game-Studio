using System;

namespace ALB
{
    /// <summary>(класс для хранения координат объекта по осям X/Y)</summary>
    class Pos : Model
    {
        public int GetX { get; private set; }
        private object x { set { GetX = value.GetType().Name == "Single" ? (int)((float)value * GridWidth) : (int)value; } }
        public int GetY { get; private set; }
        private object y { set { GetY = value.GetType().Name == "Single" ? (int)((float)value * GridHeight) : (int)value; } }
        //====== конструкторы =======
        /// <summary>(конструктор экземпляра класса для хранения координат объекта по осям X/Y относительно центра экрана)</summary>
        /// <param name="X">(координата X, рассчитываемая по пикселям консоли)</param>  
        /// <param name="Y">(координата Y, рассчитываемая по пикселям консоли)</param>
        public Pos(int X , int Y)
        {x = X; y = Y;}
        /// <summary>(конструктор экземпляра класса для хранения координат объекта по осям X/Y относительно центра экрана)</summary>
        /// <param name="Xf">(координата по оси X, рассчитываемая по ячейкам координатной сетки (зависят от переменной GridWidth))</param>
        /// <param name="Yf">(координата по оси Y, рассчитываемая по ячейкам координатной сетки (зависят от переменной GridHeight))</param>
        public Pos(float Xf , float Yf)
        {x = Xf; y = Yf;}
        //====== методы присваивания =======        
        /// <summary>(метод для присваивания значения координате X)</summary>
        /// <param name="X">(координата X, рассчитываемая по пикселям консоли)</param>  
        public void SetX(int X)
        {x = X;}
        /// <summary>(метод для присваивания значения координате X)</summary>
        /// <param name="Xf">(координата по оси X, рассчитываемая по ячейкам координатной сетки (зависят от переменной GridWidth))</param>
        public void SetX(float Xf)
        {x = Xf;}
        /// <summary>(метод для присваивания значения координате Y)</summary>
        /// <param name="Y">(координата Y, рассчитываемая по пикселям консоли)</param>
        public void SetY(int Y)
        {y = Y;}
        /// <summary>(метод для присваивания значения координате Y)</summary>
        /// <param name="Yf">(координата по оси Y, рассчитываемая по ячейкам координатной сетки (зависят от переменной GridHeight))</param>
        public void SetY(float Yf)
        { y = Yf; }

        //=============
    }
}
