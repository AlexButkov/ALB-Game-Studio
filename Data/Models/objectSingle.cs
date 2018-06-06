using System;
using System.Threading;
using System.Collections.Generic;

namespace ALB
{
    /// <summary>
    /// game object class (класс игрового объекта)
    /// </summary>
    class ObjectSingle : Model
    {
        /// <summary>
        /// XY-axis position coordinates (координата позиции по осям XY)
        /// </summary>
        public Vector Position;

        /// <summary>
        /// XY-axis sizes (размеры по осям XY)
        /// </summary>
        public Vector Size;

        /// <summary>
        /// hides object if true (скрывает объект если true)
        /// </summary>
        public bool IsDrawn
        {
            get { return Value(Param.isDrawn) ?? default(bool); }
            set
            {
                ref var temp = ref Value(Param.isDrawn);
                if (temp != value)
                {
                    temp = value;
                    if (!value)
                    {
                        Inspection?.AddTask(Param.isDrawn);
                    }
                    else
                    {
                        Inspection?.AddTask(Param.color);
                    }
                    if (ChildList.Count > 0)
                    {
                        foreach (ObjectSingle Child in ChildList)
                        {
                            Child.IsDrawn = value;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// layer number (номер слоя)
        /// </summary>
        public float Layer
        {
            get { return Value(Param.layer) ?? default(float); }
            set
            {
                ref var temp = ref Value(Param.layer);
                if (temp != value)
                {
                    temp = value;
                    Inspection?.AddTask(Param.layer);
                }
            }
        }
        /// <summary>
        /// defines color (определяет цвет)
        /// </summary>
        public ConsoleColor Color
        {
            get { return Value(Param.color) ?? default(ConsoleColor); }
            set
            {
                ref var temp = ref Value(Param.color);
                if (temp != value)
                {
                    temp = value;
                    Inspection?.AddTask(Param.color);
                }
            }
        }
        /// <summary>
        /// object to copy all parameters from. Use method <see cref="CopyFrom"/> to set 
        /// (Объект, из которого копируются все параметры. Используйте метод <see cref="CopyFrom"/> для изменения значения)
        /// </summary>
        public ObjectSingle CopyObject
        {
            get { return Value(Param.copyObject) ?? default(ObjectSingle); }
            protected set
            {
                ref var temp = ref Value(Param.copyObject);
                if (temp != value)
                {
                    temp = value;
                    Inspection?.AddTask(Param.copyObject);
                }
            }
        }
        /// <summary>
        /// position-dependent objects (объекты с зависимыми позициями)
        /// </summary>
        public List<ObjectSingle> ChildList = new List<ObjectSingle>();
        /// <summary>
        /// translates changed variables info into rendering class <see cref="View"/> 
        /// (передает информацию об измененных переменных в класс для рендеринга <see cref="View"/>)
        /// </summary>
        public Inspector Inspection { get; protected set; }
        /// <summary>
        /// contains relative state for "Trigger" methods (содержит относительное состояние для "Trigger" методов)
        /// </summary>
        public bool IsInside;
        /// <summary>
        /// whether contains a text (содержит ли текст)
        /// </summary>
        public bool IsTextured;
        /// <summary>
        /// defines an object type (определяет тип объекта)
        /// </summary>
        public string Tag;

        //---
        /// <summary>rendering variable values array (массив значений переменных, влияющих на рендеринг)</summary>
        protected dynamic[] values;
        /// <summary>(объект для поочередного доступа к массиву)</summary>
        protected static object valuesBlocker = new object();


        //========
        /// <summary>
        /// constructs game object class instance (создает экземпляр класса игрового объекта)
        /// </summary>
        /// /// <param name="preset">preset object (преднастроенный объект)</param>
        /// <param name="tag">defines an object in "Trigger" methods (определяет объект в "Trigger" методах)</param>
        /// <param name="layer">layer number (номер слоя)</param>
        /// <param name="positionX">X-axis position coordinate (координата позиции по оси X)</param>
        /// <param name="positionY">Y-axis position coordinate (координата позиции по оси Y)</param>
        /// <param name="sizeX">X-axis size (размер по оси X)</param>
        /// <param name="sizeY">Y-axis size (размер по оси Y)</param>
        /// <param name="color">defines color (определяет цвет)</param>
        /// <param name="childObject">position-dependent objects (объекты с зависимыми позициями)</param>
        public ObjectSingle(Preset? preset = null, ConsoleColor? color = null, string tag = null, float? layer = null, float? positionX = null, float? positionY = null, float? sizeX = null, float? sizeY = null, params ObjectSingle[] childObject)
        {
            values = new Object[(int)Param.max];
            Inspection = new Inspector(this);
            Position = new Vector(null, null, this, Param.positionX, Param.positionY);
            Size = new Vector(null, null, this, Param.sizeX, Param.sizeY);
            //---default values (значения по умолчанию)
            switch (preset)
            {   
                case Preset.boxS:   { Size.X = sizeX ??  1.GridToX(); Size.Y = sizeY ??  1.GridToY(); } break;
                case Preset.boxM:   { Size.X = sizeX ??  2.GridToX(); Size.Y = sizeY ??  2.GridToY(); } break;
                case Preset.boxL:   { Size.X = sizeX ??  4.GridToX(); Size.Y = sizeY ??  4.GridToY(); } break;
                case Preset.boxXL:  { Size.X = sizeX ??  8.GridToX(); Size.Y = sizeY ??  8.GridToY(); } break;
                case Preset.boxXXL: { Size.X = sizeX ?? 16.GridToX(); Size.Y = sizeY ?? 16.GridToY(); } break;
                case Preset.plane:  { Size.X = sizeX ?? WindowSize.X; Size.Y = sizeY ?? WindowSize.Y; } break;
                default: goto case Preset.boxS;
            }
            Color = color ?? ConsoleColor.DarkGray;
            Tag = tag ?? "";
            Layer = layer ?? default(float);
            Position.X = positionX ?? default(float);
            Position.Y = positionY ?? default(float);
            //---
            for (int i = 0; i < childObject.Length; i++)
            {
                if (childObject[i]!=null)
                {
                    ChildList.Add(childObject[i]);
                }
            }

            SceneList.Add(this);
            if (GetType() != typeof(ObjectGroup))
            {
                Inspection.SetArrayFull();
            }
        }
        //========
        /// <summary>provides access to rendering variable values array (обеспечивает доступ к массиву значений переменных, влияющих на рендеринг)</summary>
        public ref dynamic Value(Param task)
        {
            lock (valuesBlocker)
            {
                return ref values[(int)task];
            }
        }

        //---
        /// <summary>
        /// provides copying selected object parameters to this one.
        /// (обеспечивает копирование параметров из выбранного объекта в этот)
        /// </summary>
        /// <param name="copyObject">object to copy all parameters from (Объект, из которого копируются все параметры)</param>
        public void CopyFrom(ObjectSingle copyObject)
        {
            IsDrawn = false;
            Inspection.IsStarted = false;
            CopyObject = copyObject;
            int sleep = FixedTimeMs * 10;
            int max = (int)MainTimer.ElapsedMilliseconds + sleep;
            while (!IsDrawn && (int)MainTimer.ElapsedMilliseconds < max)
            {
                Thread.Sleep(FixedTimeMs);
            }
            if (IsDrawn)
            {
                Thread.Sleep(sleep);
            }
        }
        /// <summary>
        /// returns this object copy
        /// (возвращает копию этого объекта)
        /// </summary>
        public ObjectSingle CopyThis()
        {
            ObjectSingle newObject;
            if (GetType() != typeof(ObjectGroup))
            {
                newObject = new ObjectSingle();
            }
            else
            {
                newObject = new ObjectGroup() as ObjectSingle;
            }
            newObject.CopyFrom(this);
            if (ChildList.Count > 0)
            {
                for (int i = 0; i < ChildList.Count; i++)
                {
                    int j = 0;
                    if (newObject.Equals(ChildList[i]))
                    {
                        j++;
                    }
                    else
                    {
                        newObject.ChildList.Insert(i - j, ChildList[i]?.CopyThis());
                    }
                }
            }
            return newObject;
        }

        /// <summary>
        /// align object with screen side(выравнивает объект по стороне экрана)
        /// </summary>
        /// <param name="SideX">enum-тип SideX</param>
        /// <param name="SideY">enum-тип SideY</param>
        public virtual void AlignWithSide(SideX? sideX = null, SideY? sideY = null) 
        {
            if (sideX != null)
            {
                switch ((int)sideX)
                {
                    case -1:
                        Position.X = Size.X / 2 - (int)WindowSize.X / 2; break;//+ Shift
                    case 0:
                        Position.X = 0; break;
                    case 1:
                        Position.X = (int)WindowSize.X / 2 - Size.X / 2; break;//- Shift
                    default:
                        goto case 0;
                }
            }

            if (sideY != null)
            {
                switch ((int)sideY)
                {
                    case -1:
                        Position.Y = Size.Y / 2 - (int)WindowSize.Y / 2; break;//+ Shift
                    case 0:
                        Position.Y = 0; break;
                    case 1:
                        Position.Y = (int)WindowSize.Y / 2 - Size.Y / 2; break;//- Shift
                    default:
                        goto case 0;
                }
            }
        }
        //---

        /// <summary>
        /// (возвращает true в случае появления пересеченных объектов с текущим и добавляет их в принимаемый массив returnList)
        /// </summary>
        /// <param name="returnList">(массив пересеченных объектов)</param>
        /// <param name="tag">(Проверяются на пересечение только объекты с данным тегом. Если равен null, то проверяются все объекты на сцене)</param>
        public bool TriggerEnter(out List<ObjectSingle> returnList, string tag = null)
        {
            bool current = true;
            bool previous = false;
            return Trigger(out returnList, tag, current, previous);
        }

        /// <summary>
        /// (возвращает true в случае присутствия пересеченных объектов с текущим и добавляет их в принимаемый массив returnList)
        /// </summary>
        /// <param name="returnList">(массив пересеченных объектов)</param>
        /// <param name="tag">(Проверяются на пересечение только объекты с данным тегом. Если равен null, то проверяются все объекты на сцене)</param>
        public bool TriggerStay(out List<ObjectSingle> returnList, string tag = null)
        {
            bool current = true;
            bool previous = true;
            return Trigger(out returnList, tag, current, previous);
        }

        /// <summary>
        /// (возвращает true в случае выхода пересеченных объектов с текущим и добавляет их в принимаемый массив returnList)
        /// </summary>
        /// <param name="returnList">(массив пересеченных объектов)</param>
        /// <param name="tag">(Проверяются на пересечение только объекты с данным тегом. Если равен null, то проверяются все объекты на сцене)</param>
        public bool TriggerExit(out List<ObjectSingle> returnList, string tag = null)
        {
            bool current = false;
            bool previous = true;
            return Trigger(out returnList, tag, current, previous);
        }

        bool Trigger(out List<ObjectSingle> returnList, string tag, bool current, bool previous)
        {
            returnList = new List<ObjectSingle>();
            foreach (Object obj in SceneList)
            {
                ObjectGroup thisGroup = null;
                ObjectGroup otherGroup = null;
                bool otherIsGroup = (obj.GetType() != typeof(ObjectGroup)) ? false : true;
                bool thisIsGroup = (GetType() != typeof(ObjectGroup)) ? false : true;

                if (otherIsGroup)
                {
                    otherGroup = (ObjectGroup)obj;
                }
                if (thisIsGroup)
                {
                    thisGroup = (ObjectGroup)this;
                }

                var otherObject = otherIsGroup ? (ObjectGroup)obj : (ObjectSingle)obj;


                if ((tag == null || otherObject.Tag == tag) && otherObject.IsDrawn)
                {
                    int thisSizeX = thisIsGroup ? (int)Size.X + ((int)Size.X + (int)thisGroup.Gap.X) * ((int)thisGroup.Quant.X - 1) : (int)Size.X;
                    int thisSizeY = thisIsGroup ? (int)Size.Y + ((int)Size.Y + (int)thisGroup.Gap.Y) * ((int)thisGroup.Quant.Y - 1) : (int)Size.Y;
                    int otherSizeX = otherIsGroup ? (int)otherObject.Size.X + ((int)otherObject.Size.X + (int)otherGroup.Gap.X) * ((int)otherGroup.Quant.X - 1) : (int)otherObject.Size.X;
                    int otherSizeY = otherIsGroup ? (int)otherObject.Size.Y + ((int)otherObject.Size.Y + (int)otherGroup.Gap.Y) * ((int)otherGroup.Quant.Y - 1) : (int)otherObject.Size.Y;
                    int maxX = Math.Max((int)Position.X + thisSizeX / 2, (int)otherObject.Position.X + otherSizeX / 2);
                    int maxY = Math.Max((int)Position.Y + thisSizeY / 2, (int)otherObject.Position.Y + otherSizeY / 2);
                    int minX = Math.Min((int)Position.X - thisSizeX / 2, (int)otherObject.Position.X - otherSizeX / 2);
                    int minY = Math.Min((int)Position.Y - thisSizeY / 2, (int)otherObject.Position.Y - otherSizeY / 2);
                    int sumX = thisSizeX + otherSizeX;
                    int sumY = thisSizeY + otherSizeY;

                    bool currValue = (maxX - minX < sumX && maxY - minY < sumY) ? true : false;
                    bool prevValue = otherObject.IsInside;
                    if (currValue == current && prevValue == previous)
                    {
                        returnList.Add((ObjectSingle)obj);
                    }
                    if (prevValue != currValue)
                    {
                        otherObject.IsInside = currValue;
                    }
                }
            }
            return returnList.Count > 0 ? true : false;
        }
    }
}
