using System;

namespace ALB
{
    /// <summary>class to contain X/Y axes parameters (класс для хранения параметров объекта по осям X/Y)</summary>
    class Vector : Model
    {
        public int GetX { get; private set; }
        private object x { set { GetX = value.GetType().Name == "Single" ? (int)((float)value * GridSize.GetX) : (int)value; } }
        public int GetY { get; private set; }
        private object y { set { GetY = value.GetType().Name == "Single" ? (int)((float)value * GridSize.GetY) : (int)value; } }
        //====== конструкторы =======
        /// <summary>class constructor to contain X/Y axes parameters(конструктор экземпляра класса для хранения параметров объекта по осям X/Y)</summary>
        /// <param name="X">X-axis parameter, calculated using console pixels (параметр по оси X, рассчитываемый по пикселям консоли)</param>  
        /// <param name="Y">Y-axis parameter, calculated using console pixels (параметр по оси Y, рассчитываемый по пикселям консоли)</param>
        public Vector(int X, int Y)
        { x = X; y = Y; }
        /// <summary>class constructor to contain X/Y axes parameters(конструктор экземпляра класса для хранения параметров объекта по осям X/Y)</summary>
        /// <param name="Xf">X-axis parameter, calculated using grid cells from GridSize (параметр по оси X, рассчитываемый по ячейкам координатной сетки (зависят от переменной GridSize))</param>
        /// <param name="Yf">Y-axis parameter, calculated using grid cells from GridSize (параметр по оси Y, рассчитываемый по ячейкам координатной сетки (зависят от переменной GridSize))</param>
        public Vector(float Xf, float Yf, ObjectSingle Parent = null)
        { x = Xf; y = Yf; }
        //====== методы присваивания =======        
        /// <summary>X-axis parameter setter (метод для присваивания значения размеру по оси X)</summary>
        /// <param name="X">X-axis parameter, calculated using console pixels (параметр по оси X, рассчитываемый по пикселям консоли)</param>  
        public void SetX(int X)
        { x = X; }
        /// <summary>X-axis parameter setter (метод для присваивания значения размеру по оси X)</summary>
        /// <param name="Xf">X-axis parameter, calculated using grid cells from GridSize (параметр по оси X, рассчитываемый по ячейкам координатной сетки (зависят от переменной GridSize))</param>
        public void SetX(float Xf)
        { x = Xf; }
        /// <summary>Y-axis parameter setter Y-axis parameter setter (метод для присваивания значения размеру по оси Y)</summary>
        /// <param name="Y">Y-axis parameter, calculated using console pixels (параметр по оси Y, рассчитываемый по пикселям консоли)</param>
        public void SetY(int Y)
        { y = Y; }
        /// <summary>Y-axis parameter setter (метод для присваивания значения размеру по оси Y)</summary>
        /// <param name="Yf">Y-axis parameter, calculated using grid cells from GridSize (параметр по оси Y, рассчитываемый по ячейкам координатной сетки (зависят от переменной GridSize))</param>
        public void SetY(float Yf)
        { y = Yf; }

        //=============
    }
}
