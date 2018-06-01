using System;
using System.Threading;
using System.Collections.Generic;

namespace ALB
{
    /// <summary>contains X/Y axes parameters (содержит параметры объекта по осям X/Y)</summary>
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
                    ref var temp = ref ParentObject.Value(TypeX);
                    if (temp != value)
                    {
                        prevX = (int)(temp ?? 0);
                        temp = value;
                        if (prevX != (int)value)
                        {
                            ParentObject.Inspection?.AddTask(TypeX);
                            if (TypeX == Param.positionX && ParentObject.ChildList.Count > 0)
                            {
                                foreach (ObjectSingle Child in ParentObject.ChildList)
                                {
                                    if (Child != null)
                                    {
                                        Child.Position.X += (int)value - prevX;
                                    }
                                }
                            }
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
                    ref var temp = ref ParentObject.Value(TypeY);
                    if (temp != value)
                    {
                        prevY = (int)(temp ?? 0);
                        temp = value;
                        if (prevY != (int)value)
                        {
                            ParentObject.Inspection?.AddTask(TypeY);
                            if (TypeY == Param.positionY && ParentObject.ChildList.Count > 0)
                            {
                                foreach (ObjectSingle Child in ParentObject.ChildList)
                                {
                                    if (Child != null)
                                    {
                                        Child.Position.Y += (int)value - prevY;
                                    }
                                }
                            }
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
        public Param TypeX { private get; set; }
        public Param TypeY { private get; set; }
        //---
        protected float x;
        protected float y;
        protected int prevX;
        protected int prevY;

        //====== конструкторы =======
        /// <summary>contain X/Y axes parameters(содержит параметры объекта по осям X/Y)</summary>
        /// <param name="x">X-axis parameter, calculated using console pixels (параметр по оси X, рассчитываемый по пикселям консоли)</param> 
        /// <param name="y">Y-axis parameter, calculated using console pixels (параметр по оси Y, рассчитываемый по пикселям консоли)</param> 
        /// <param name="inspector">parent controller (родительский контроллер)</param>
        /// <param name="typeX">X-axis Task-enum parameter (Task-enum параметр по оси X)</param>
        /// <param name="typeY">Y-axis Task-enum parameter (Task-enum параметр по оси Y)</param>
        public Vector(float? x = null, float? y = null, ObjectSingle parentObject = null, Param? typeX = null, Param? typeY = null)
        {
            TypeX = typeX ?? Param.max;
            TypeY = typeY ?? Param.max;
            ParentObject = parentObject;
            X = x ?? 0;
            Y = y ?? 0;
        }
        //=============
    }
}
