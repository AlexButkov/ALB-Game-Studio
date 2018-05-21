using System;

namespace ALB
{
    /// <summary>contain X/Y axes parameters (класс для хранения параметров объекта по осям X/Y)</summary>
    class Vector : Model
    {
        public float X
        {
            get { return x; }
            set
            {
                x = value;
                if (prevX != (int)x && Inspector != null)
                {
                    Inspector.AddTask(TypeX);
                    prevX = (int)x;
                }
            }
        }
        public float Y
        {
            get { return y; }
            set
            {
                y = value;
                if (prevY != (int)y && Inspector != null)
                {
                    Inspector.AddTask(TypeY);
                    prevY = (int)y;
                }
            }
        }
        public Controller Inspector { private get; set; } = null;
        public Task TypeX { private get; set; }
        public Task TypeY { private get; set; }
        //---
        protected float x;
        protected float y;
        protected int? prevX = null;
        protected int? prevY = null;

        //====== конструкторы =======
        /// <summary>contain X/Y axes parameters(содержит параметры объекта по осям X/Y)</summary>
        /// <param name="x">X-axis parameter, calculated using console pixels (параметр по оси X, рассчитываемый по пикселям консоли)</param> 
        /// <param name="y">Y-axis parameter, calculated using console pixels (параметр по оси Y, рассчитываемый по пикселям консоли)</param> 
        /// <param name="inspector">parent controller (родительский контроллер)</param>
        /// <param name="typeX">X-axis Task-enum parameter (Task-enum параметр по оси X)</param>
        /// <param name="typeY">Y-axis Task-enum parameter (Task-enum параметр по оси Y)</param>
        public Vector(int? x = null, int? y = null, Controller inspector = null, Task? typeX = null, Task? typeY = null)
        {
            TypeX = typeX ?? Task.max;
            TypeY = typeY ?? Task.max;
            Inspector = inspector;
            this.x = x ?? 0;
            this.y = y ?? 0;
        }
        //=============
    }
}
