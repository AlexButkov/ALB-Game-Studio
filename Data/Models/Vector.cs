using System;

namespace ALB
{
    /// <summary>contain X/Y axes parameters (класс для хранения параметров объекта по осям X/Y)</summary>
    class Vector : Model
    {
        public float X
        {
            get
            {
                if (ParentObject != null)
                {
                    return ParentObject.Value(TypeX) ?? default(float);
                }
                else
                {
                    return x;
                }
            }
            set
            {
                if (ParentObject != null)
                {
                    if (ParentObject.Value(TypeX) != value)
                    {
                        ref var temp = ref ParentObject.Value(TypeX);
                        prevX = temp as int?;
                        temp = value;
                        if (prevX != (int)value)
                        {
                            ParentObject.Inspection?.AddTask(TypeX);
                        }
                    }
                }
                else
                {
                    x = value;
                }
            }
        }

        public float Y
        {
            get
            {
                if (ParentObject != null)
                {
                    return ParentObject.Value(TypeY) ?? default(float);
                }
                else
                {
                    return y;
                }
            }
            set
            {
                if (ParentObject != null)
                {
                    if (ParentObject.Value(TypeY) != value)
                    {
                        ref var temp = ref ParentObject.Value(TypeY);
                        prevX = temp as int?;
                        temp = value;
                        if (prevY != (int)value)
                        {
                            ParentObject.Inspection?.AddTask(TypeY);
                        }
                    }
                }
                else
                {
                    y = value;
                }
            }
        }
        public ObjectSingle ParentObject { private get; set; } = null;
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
        public Vector(int? x = null, int? y = null, ObjectSingle parentObject = null, Task? typeX = null, Task? typeY = null)
        {
            TypeX = typeX ?? Task.max;
            TypeY = typeY ?? Task.max;
            ParentObject = parentObject;
            this.x = x ?? 0;
            this.y = y ?? 0;
        }
        //=============
    }
}
