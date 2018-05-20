using System;

namespace ALB
{
    /// <summary>contain X/Y axes parameters (класс для хранения параметров объекта по осям X/Y)</summary>
    class Vector : Model
    {
        public int GetX { get; private set; }
        protected object x {
            set
            {
                GetX = value.GetType().Name == "Single" ? (int)((float)value * GridSize.GetX) : (int)value;
                if (prevX != GetX && Inspector != null)
                {
                    Inspector.AddTask(TypeX);
                }
                prevX = GetX;
            }
        }
        public int GetY { get; private set; }
        protected object y {
            set
            {
                GetY = value.GetType().Name == "Single" ? (int)((float)value * GridSize.GetY) : (int)value;
                if (prevY != GetY && Inspector != null)
                {
                    Inspector.AddTask(TypeY);
                }
                prevY = GetY;
            }
        }
        public Controller Inspector { private get; set; } = null;
        public Task TypeX { private get; set; } = Task.max;
        public Task TypeY { private get; set; } = Task.max;
        protected int? prevX = null;
        protected int? prevY = null;

        //====== конструкторы =======
        /// <summary>contain X/Y axes parameters(содержит параметры объекта по осям X/Y)</summary>
        /// <param name="parent">X-axis parameter, calculated using console pixels (параметр по оси X, рассчитываемый по пикселям консоли)</param>  
        public Vector(Controller inspector, Task typeX = Task.max, Task typeY = Task.max )
        {
            TypeX = typeX;
            TypeY = typeY;
            Inspector = inspector;
            x = 0;
            y = 0;
        }
        /// <summary>contain X/Y axes parameters(содержит параметры объекта по осям X/Y)</summary>
        /// <param name="X">X-axis parameter, calculated using console pixels (параметр по оси X, рассчитываемый по пикселям консоли)</param>  
        /// <param name="Y">Y-axis parameter, calculated using console pixels (параметр по оси Y, рассчитываемый по пикселям консоли)</param>
        public Vector(int X, int Y)
        { x = X; y = Y; }
        /// <summary>contain X/Y axes parameters(содержит параметры объекта по осям X/Y)</summary>
        /// <param name="Xf">X-axis parameter, calculated using GridSize variable (параметр по оси X, рассчитываемый по переменной GridSize)</param>
        /// <param name="Yf">Y-axis parameter, calculated using GridSize variable (параметр по оси Y, рассчитываемый по переменной GridSize)</param>
        public Vector(float Xf, float Yf)
        { x = Xf; y = Yf; }
        //====== методы присваивания =======        
        /// <summary>X-axis parameter setter (присваивает значения параметру по оси X)</summary>
        /// <param name="X">X-axis parameter, calculated using console pixels (параметр по оси X, рассчитываемый по пикселям консоли)</param>  
        public void SetX(int X)
        { x = X; }
        /// <summary>X-axis parameter setter (присваивает значения параметру по оси X)</summary>
        /// <param name="Xf">X-axis parameter, calculated using GridSize variable (параметр по оси X, рассчитываемый по переменной GridSize)</param>
        public void SetX(float Xf)
        { x = Xf; }
        /// <summary>Y-axis parameter setter Y-axis parameter setter (присваивает значения параметру по оси Y)</summary>
        /// <param name="Y">Y-axis parameter, calculated using console pixels (параметр по оси Y, рассчитываемый по пикселям консоли)</param>
        public void SetY(int Y)
        { y = Y; }
        /// <summary>Y-axis parameter setter (присваивает значения параметру по оси Y)</summary>
        /// <param name="Yf">Y-axis parameter, calculated using GridSize variable (параметр по оси Y, рассчитываемый по переменной GridSize)</param>
        public void SetY(float Yf)
        { y = Yf; }

        //=============
    }
}
