using System;
using System.Threading;
using System.Collections.Generic;

namespace ALB
{
    /// <summary>contains X/Y axes parameters (содержит параметры объекта по осям X/Y)</summary>
    class Vector : Model
    {
        /// <summary>
        /// X-axis parameter (параметр по оси X)</param>
        /// </summary>
        public float X
        {
            get
            {
                if (ParentObject != null)
                {
                    return ParentObject.Value(NameX) ?? default(float);
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
                    ref var temp = ref ParentObject.Value(NameX);
                    if (temp != value)
                    {
                        prevX = (int)(temp ?? 0);
                        temp = value;
                        if (prevX != (int)value)
                        {
                            ParentObject.Inspection?.AddTask(NameX);
                            if (ParentObject.IsTextured)
                            {
                                ParentObject.Inspection?.AddTask(Param.color);
                            }
                            if (NameX == Param.positionX && ParentObject.ChildList.Count > 0)
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

        /// <summary>
        /// Y-axis parameter (параметр по оси Y)</param>
        /// </summary>
        public float Y
        {
            get
            {
                if (ParentObject != null)
                {
                    return ParentObject.Value(NameY) ?? default(float);
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
                    ref var temp = ref ParentObject.Value(NameY);
                    if (temp != value)
                    {
                        prevY = (int)(temp ?? 0);
                        temp = value;
                        if (prevY != (int)value)
                        {
                            ParentObject.Inspection?.AddTask(NameY);
                            if (ParentObject.IsTextured)
                            {
                                ParentObject.Inspection?.AddTask(Param.color);
                            }
                            if (NameY == Param.positionY && ParentObject.ChildList.Count > 0)
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
        /// <summary>
        /// parent object (родительский объект)
        /// </summary>
        public ObjectSingle ParentObject { private get; set; } = null;
        /// <summary>
        /// X-axis <see cref="Param"/> name (<see cref="Param"/> название по оси X)
        /// </summary>
        public Param NameX { private get; set; }
        /// <summary>
        /// Y-axis <see cref="Param"/> name (<see cref="Param"/> название по оси Y) 
        /// </summary>
        public Param NameY { private get; set; }
        //---
        protected float x;
        protected float y;
        protected int prevX;
        protected int prevY;

        //====== конструкторы =======
        /// <summary>contain X/Y axes parameters(содержит параметры объекта по осям X/Y)</summary>
        /// <param name="x">X-axis parameter (параметр по оси X)</param> 
        /// <param name="y">Y-axis parameter (параметр по оси Y)</param> 
        /// <param name="parentObject">parent object (родительский объект)</param>
        /// <param name="nameX">X-axis <see cref="Param"/> name (<see cref="Param"/> название по оси X)</param>
        /// <param name="nameY">Y-axis <see cref="Param"/> name (<see cref="Param"/> название по оси Y)</param>
        public Vector(float? x = null, float? y = null, ObjectSingle parentObject = null, Param? nameX = null, Param? nameY = null)
        {
            NameX = nameX ?? Param.max;
            NameY = nameY ?? Param.max;
            ParentObject = parentObject;
            X = x ?? 0;
            Y = y ?? 0;
        }
        //=============
    }
}
