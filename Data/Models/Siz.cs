using System;

namespace ALB
{
    /// <summary>(класс для хранения размеров объекта по осям X/Y)</summary>
    class Siz : Model
    {
        public int GetX { get; private set; }
        private object x { set { GetX = value.GetType().Name == "Single" ? (int)((float)value * GridWidth) : (int)value; } }
        public int GetY { get; private set; }
        private object y { set { GetY = value.GetType().Name == "Single" ? (int)((float)value * GridHeight) : (int)value; } }
        //====== конструкторы =======
        /// <summary>(конструктор экземпляра класса для хранения размеров объекта по осям X/Y)</summary>
        /// <param name="X">(размер по оси X, рассчитываемый по пикселям консоли)</param>  
        /// <param name="Y">(размер по оси Y, рассчитываемый по пикселям консоли)</param>
        public Siz(int X, int Y)
        { x = X; y = Y; }
        /// <summary>(конструктор экземпляра класса для хранения размеров объекта по осям X/Y)</summary>
        /// <param name="Xf">(размер по оси X, рассчитываемый по ячейкам координатной сетки (зависят от переменной GridWidth))</param>
        /// <param name="Yf">(размер по оси Y, рассчитываемый по ячейкам координатной сетки (зависят от переменной GridHeight))</param>
        public Siz(float Xf, float Yf)
        { x = Xf; y = Yf; }
        //====== методы присваивания =======        
        /// <summary>(метод для присваивания значения размеру по оси X)</summary>
        /// <param name="X">(размер по оси X, рассчитываемый по пикселям консоли)</param>  
        public void SetX(int X)
        { x = X; }
        /// <summary>(метод для присваивания значения размеру по оси X)</summary>
        /// <param name="Xf">(размер по оси X, рассчитываемый по ячейкам координатной сетки (зависят от переменной GridWidth))</param>
        public void SetX(float Xf)
        { x = Xf; }
        /// <summary>(метод для присваивания значения размеру по оси Y)</summary>
        /// <param name="Y">(размер по оси Y, рассчитываемый по пикселям консоли)</param>
        public void SetY(int Y)
        { y = Y; }
        /// <summary>(метод для присваивания значения размеру по оси Y)</summary>
        /// <param name="Yf">(размер по оси Y, рассчитываемый по ячейкам координатной сетки (зависят от переменной GridHeight))</param>
        public void SetY(float Yf)
        { y = Yf; }

        //=============
    }
}
