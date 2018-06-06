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
                float val = value.Range(MinX, MaxX);
                if (ParentObject != null)
                {
                    ref var temp = ref ParentObject.Value(NameX);
                    if (temp != val)
                    {
                        tempX = (int)(temp ?? 0);
                        temp = val;
                        if (tempX != (int)val)
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
                                        Child.Position.X += (int)val - tempX;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    x = val;
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
                float val = value.Range(MinY, MaxY);
                if (ParentObject != null)
                {
                    ref var temp = ref ParentObject.Value(NameY);
                    if (temp != val)
                    {
                        tempY = (int)(temp ?? 0);
                        temp = val;
                        if (tempY != (int)val)
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
                                        Child.Position.Y += (int)val - tempY;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    y = val;
                }
            }
        }
        /// <summary>
        /// X minimal value (минимальное значение X)
        /// </summary>
        public float? MinX;
        /// <summary>
        /// Y minimal value (минимальное значение Y)
        /// </summary>
        public float? MinY;
        /// <summary>
        /// X maximal value (максимальное значение X)
        /// </summary>
        public float? MaxX;
        /// <summary>
        /// Y maximal value (максимальное значение Y)
        /// </summary>
        public float? MaxY;
        /// <summary>
        /// X previous value (предыдущее значение X)
        /// </summary>
        public int PrevX;
        /// <summary>
        /// Y previous value (предыдущее значение Y)
        /// </summary>
        public int PrevY;
        //---
        /// <summary>
        /// parent object (родительский объект)
        /// </summary>
        protected ObjectSingle ParentObject = null;
        /// <summary>
        /// X-axis <see cref="Param"/> name (<see cref="Param"/> название по оси X)
        /// </summary>
        protected Param NameX;
        /// <summary>
        /// Y-axis <see cref="Param"/> name (<see cref="Param"/> название по оси Y) 
        /// </summary>
        protected Param NameY;
        //---
        float x;
        float y;
        int tempX;
        int tempY;

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
