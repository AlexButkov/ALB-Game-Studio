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
                if (color != value && Inspector != null)
                {
                    Inspector.AddTask(Task.color);
                }
                color = value;
            }
        }
        public float Layer
        {
            get { return layer; }
            set
            {
                if (layer != value && Inspector != null)
                {
                    Inspector.AddTask(Task.layer);
                }
                layer = value;
            }
        }
        public bool IsDestroyed
        {
            get { return isDestroyed; }
            set
            {
                if (isDestroyed != value && Inspector != null)
                {
                    Inspector.AddTask(Task.isDestroyed);
                }
                isDestroyed = value;
            }
        }
        public List<ObjectSingle> ChildList { get; set; } = new List<ObjectSingle>();
        public Controller Inspector;
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
        public ObjectSingle(ObjType objectType, float? layer = null, float? positionX = null, float? positionY = null, float? sizeX = null, float? sizeY = null, ConsoleColor? color = null, params ObjectSingle[] childObject )
        {
            Inspector = new Controller(this);
            Position = new Vector(null, null, Inspector, Task.positionX,Task.positionY);
            Size = new Vector(null, null, Inspector, Task.sizeX, Task.sizeY);
            switch (objectType)
            {   //характеристики объектов по умолчанию
                case ObjType.Car:      { Position.X = positionX ??  40; Position.Y = positionY ??  20; Size.X = sizeX ??  12; Size.Y = sizeY ??  08; Color = color ?? ConsoleColor.Blue; } break;
                case ObjType.Wheel:    { Position.X = positionX ??  39; Position.Y = positionY ??  21; Size.X = sizeX ??  01; Size.Y = sizeY ??  01; Color = color ?? ConsoleColor.DarkGray; } break;
                case ObjType.Line:     { Position.X = positionX ??  20; Position.Y = positionY ??  10; Size.X = sizeX ??  02; Size.Y = sizeY ??  05; Color = color ?? ConsoleColor.Gray; } break;
                case ObjType.House:    { Position.X = positionX ??  00; Position.Y = positionY ??  00; Size.X = sizeX ?? 120; Size.Y = sizeY ??  80; Color = color ?? ConsoleColor.DarkYellow; } break;
                case ObjType.Tree:     { Position.X = positionX ??  40; Position.Y = positionY ??  10; Size.X = sizeX ??  08; Size.Y = sizeY ??  08; Color = color ?? ConsoleColor.Green; } break;
                default: break;
            }
            Layer = layer ?? 0 ;
            for (int i = 0; i < childObject.Length; i++)
                ChildList.Add(childObject[i]);
            //SceneList.Add(new Controller((ObjectGroup)this));
            if (GetType() != typeof(ObjectGroup))
            {
                Inspector.SetArrayFull();
            }
        }

        //========
    }
}
