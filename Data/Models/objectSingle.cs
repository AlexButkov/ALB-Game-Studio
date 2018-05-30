using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace ALB
{
    /// <summary>
    /// Базовый класс для объекта в игре
    /// </summary>
    class ObjectSingle : Model
    {
        public Vector Position;
        public Vector Size;
        public bool IsDestroyed
        {
            get { return Value(Task.isDestroyed) ?? default(bool); }
            set
            {
                ref var temp = ref Value(Task.isDestroyed);
                if (temp != value)
                {
                    temp = value;
                    if (value)
                    {
                        Inspection?.AddTask(Task.isDestroyed);
                    }
                    else
                    {
                        Inspection?.AddTask(Task.color);
                    }
                }
            }
        }
        public float Layer
        {
            get { return Value(Task.layer) ?? default(float); }
            set
            {
                ref var temp = ref Value(Task.layer);
                if (temp != value)
                {
                    temp = value;
                    Inspection?.AddTask(Task.layer);
                }
            }
        }
        public ConsoleColor Color
        {
            get { return Value(Task.color) ?? default(ConsoleColor); }
            set
            {
                ref var temp = ref Value(Task.color);
                if (temp != value)
                {
                    temp = value;
                    Inspection?.AddTask(Task.color);
                }
            }
        }
        /// <summary>(Для указания значения используется метод CopyFrom())</summary>
        public ObjectSingle CopyObject
        {
            get { return Value(Task.copyObject) ?? default(ObjectSingle); }
            set
            {
                ref var temp = ref Value(Task.copyObject);
                if (temp != value)
                {
                    temp = value;
                    Inspection?.AddTask(Task.copyObject);
                }
            }
        }
        public List<ObjectSingle> ChildList = new List<ObjectSingle>();
        public Inspector Inspection;
        public bool IsInside;
        public ObjType ObjectType;
        //---
        /// <summary>(массив значений переменных, влияющих на отображение объекта)</summary>
        protected dynamic[] values;


        //========
        /// <summary>
        /// конструктор экземпляра базового класса для объекта в игре (в первом параметре указывается объект перечисляемого типа "Scene", иначе создается объект с нулевыми значениями)
        /// </summary>
        //public ObjectSingle() { }
        /// <summary>
        /// конструктор экземпляра базового класса для объекта в игре (в первом параметре указывается объект перечисляемого типа "Scene", иначе создается объект с нулевыми значениями)
        /// </summary>
        /// <param name="objectType">тип объекта</param>
        /// <param name="renderLayer">№ слоя для рендеринга</param>
        /// <param name="parentObject">ограничивающий движение объект</param>
        /// <param name="positionX">координата позиции по оси X</param>
        /// <param name="positionY">координата позиции по оси Y</param>
        /// <param name="sizeX">размер по оси X</param>
        /// <param name="sizeY">размер по оси Y</param>
        /// <param name="color">цвет объекта</param>
        /// <param name="childObject">список объектов, позиции которых будут связаны с текущим объектом</param>
        public ObjectSingle(ObjType objectType, float? layer = null, float? positionX = null, float? positionY = null, float? sizeX = null, float? sizeY = null, ConsoleColor? color = null, params ObjectSingle[] childObject)
        {
            values = new Object[(int)Task.max];
            Inspection = new Inspector(this);
            Position = new Vector(null, null, this, Task.positionX, Task.positionY);
            Size = new Vector(null, null, this, Task.sizeX, Task.sizeY);
            switch (objectType)
            {   //характеристики объектов по умолчанию
                case ObjType.Car:   { Position.X = positionX ?? 00; Position.Y = positionY ?? 00; Size.X = sizeX ?? 12; Size.Y = sizeY ?? 08; Color = color ?? ConsoleColor.Blue; } break;
                case ObjType.Wheel: { Position.X = positionX ?? 00; Position.Y = positionY ?? 00; Size.X = sizeX ?? 01; Size.Y = sizeY ?? 01; Color = color ?? ConsoleColor.DarkGray; } break;
                case ObjType.Line:  { Position.X = positionX ?? 00; Position.Y = positionY ?? 00; Size.X = sizeX ?? 02; Size.Y = sizeY ?? 05; Color = color ?? ConsoleColor.Gray; } break;
                case ObjType.House: { Position.X = positionX ?? 00; Position.Y = positionY ?? 00; Size.X = sizeX ?? 120; Size.Y = sizeY ?? 80; Color = color ?? ConsoleColor.DarkYellow; } break;
                case ObjType.Tree:  { Position.X = positionX ?? 00; Position.Y = positionY ?? 00; Size.X = sizeX ?? 08; Size.Y = sizeY ?? 08; Color = color ?? ConsoleColor.Green; } break;
                default: break;
            }
            ObjectType = objectType;
            Layer = layer ?? default(float);
            for (int i = 0; i < childObject.Length; i++)
            {
                ChildList.Add(childObject[i]);
            }
            SceneList.Add(this);
            if (GetType() != typeof(ObjectGroup))
            {
                Inspection.SetArrayFull();
            }
        }
        //========
        public ref dynamic Value(Task task)
        {
            return ref values[(int)task];
        }
        
        //---
        public void CopyFrom(ObjectSingle copyObject)
        {
            CopyObject = copyObject;
        }

        public virtual ObjectSingle CopyThis()
        {
            ObjectSingle newObject = new ObjectSingle(ObjectType);
            newObject.CopyFrom(this);
            if (ChildList.Count > 0)
            {
                for (int i = 0; i < ChildList.Count; i++)
                {
                    newObject.ChildList.Insert(i, ChildList[i].CopyThis());
                }
            }
            return newObject;
        }

        /// <summary>
        /// align object with screen side(выравнивает объект по стороне экрана)
        /// </summary>
        /// <param name="SideX">enum-тип SideX</param>
        /// <param name="SideY">enum-тип SideY</param>
        public void AlignWithSide(SideX? sideX = null, SideY? sideY = null)
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
        
        public bool TriggerEnter(out List<ObjectSingle> returnList, ObjType? objType = null)
        {
            bool current = true;
            bool previous = false;
            return Trigger(out returnList, objType, current, previous);
        }

        public bool TriggerStay(out List<ObjectSingle> returnList, ObjType? objType = null)
        {
            bool current = true;
            bool previous = true;
            return Trigger(out returnList, objType, current, previous);
        }

        public bool TriggerExit(out List<ObjectSingle> returnList, ObjType? objType = null)
        {
            bool current = false;
            bool previous = true;
            return Trigger(out returnList, objType, current, previous);
        }

        bool Trigger(out List<ObjectSingle> returnList, ObjType? objType, bool current, bool previous)
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


                if ((objType == null || otherObject.ObjectType == objType) && !otherObject.IsDestroyed)
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
