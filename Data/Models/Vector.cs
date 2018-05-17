using System;

namespace ALB
{
    /// <summary>contain X/Y axes parameters (класс для хранения параметров объекта по осям X/Y)</summary>
    class Vector : Model
    {
        public int GetX { get; private set; }
        private object x { set { GetX = value.GetType().Name == "Single" ? (int)((float)value * GridSize.GetX) : (int)value; } }
        public int GetY { get; private set; }
        private object y { set { GetY = value.GetType().Name == "Single" ? (int)((float)value * GridSize.GetY) : (int)value; } }
        public Controller Inspector { private get; set; } = null;
        public VarType? TypeX { private get; set; } = null;
        public VarType? TypeY { private get; set; } = null;
        //====== конструкторы =======
        /// <summary>contain X/Y axes parameters(содержит параметры объекта по осям X/Y)</summary>
        /// <param name="parent">X-axis parameter, calculated using console pixels (параметр по оси X, рассчитываемый по пикселям консоли)</param>  
        public Vector(Controller inspector = null, VarType typeX = VarType.def, VarType typeY = VarType.def )
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
