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
        public Vector Position { get; set; }
        public Vector Size { get; set; }
        public ConsoleColor Color
        {
            get { return color; }
            set
            {
                if (color != value && Inspection != null)
                {
                    Inspection.AddTask(Task.color);
                }
                color = value;
            }
        }
        public float Layer
        {
            get { return layer; }
            set
            {
                if (layer != value && Inspection != null)
                {
                    Inspection.AddTask(Task.layer);
                }
                layer = value;
            }
        }
        public bool IsDestroyed
        {
            get { return isDestroyed; }
            set
            {
                if (isDestroyed != value && Inspection != null)
                {
                    Inspection.AddTask(Task.isDestroyed);
                }
                isDestroyed = value;
            }
        }
        public List<ObjectSingle> ChildList { get; set; } = new List<ObjectSingle>();
        public Inspector Inspection;
        //----
        protected ConsoleColor color = DefaultColor;
        protected float layer;
        protected bool isDestroyed;
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
            Inspection = new Inspector(this);
            Position = new Vector(null, null, Inspection, Task.positionX, Task.positionY);
            Size = new Vector(null, null, Inspection, Task.sizeX, Task.sizeY);
            switch (objectType)
            {   //характеристики объектов по умолчанию
                case ObjType.Car:   { Position.X = positionX ?? 00; Position.Y = positionY ?? 00; Size.X = sizeX ?? 12; Size.Y = sizeY ?? 08; Color = color ?? ConsoleColor.Blue; } break;
                case ObjType.Wheel: { Position.X = positionX ?? 00; Position.Y = positionY ?? 00; Size.X = sizeX ?? 01; Size.Y = sizeY ?? 01; Color = color ?? ConsoleColor.DarkGray; } break;
                case ObjType.Line:  { Position.X = positionX ?? 00; Position.Y = positionY ?? 00; Size.X = sizeX ?? 02; Size.Y = sizeY ?? 05; Color = color ?? ConsoleColor.Gray; } break;
                case ObjType.House: { Position.X = positionX ?? 00; Position.Y = positionY ?? 00; Size.X = sizeX ?? 120; Size.Y = sizeY ?? 80; Color = color ?? ConsoleColor.DarkYellow; } break;
                case ObjType.Tree:  { Position.X = positionX ?? 00; Position.Y = positionY ?? 00; Size.X = sizeX ?? 08; Size.Y = sizeY ?? 08; Color = color ?? ConsoleColor.Green; } break;
                default: break;
            }
            Layer = layer ?? 0;
            for (int i = 0; i < childObject.Length; i++)
                ChildList.Add(childObject[i]);
            //SceneList.Add(new Controller((ObjectGroup)this));
            if (GetType() != typeof(ObjectGroup))
            {
                Inspection.SetArrayFull();
            }
        }

        //========
        
            /// <summary>
            /// align object with screen side(выравнивает объект по стороне экрана)
            /// </summary>
            /// <param name="SideX">enum-тип SideX</param>
            /// <param name="SideY">enum-тип SideY</param>
            public void AlignWithSide(SideX? sideX = null, SideY? sideY = null)
        {
            if (sideX != null)
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

            if (sideY != null)
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
}
