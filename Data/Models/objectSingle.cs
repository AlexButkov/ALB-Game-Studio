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
        public Vector Position { get { return position; } set { position = value; } }
        public Vector Size { get { return size;  } set { size = value; } }
        public ConsoleColor Color { get { return color; } set { color = value; } }
        public int RenderLayer { get; set;}
        List<ObjectSingle> ChildList { get; set; } = new List<ObjectSingle>();
        //----
        protected Vector position = new Vector(0,0);
        protected Vector size = new Vector(0,0);
        protected int renderLayer;
        protected ConsoleColor color;
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
        /// <param name="position">координаты позиций по осям X/Y</param>
        /// <param name="size">размеры по осям X/Y</param>
        /// <param name="color">цвет объекта</param>
        /// <param name="childObject">список объектов, позиции которых будут связаны с текущим объектом</param>
        public ObjectSingle(ObjectType objectType, int renderLayer = 0, Vector position = null , Vector size = null, ConsoleColor color = 0, params ObjectSingle[] childObject )
        {
            switch (objectType)
            {   //характеристики объектов по умолчанию
                case ObjectType.Car:      { this.position.SetX( position != null ? position.GetX : 40); this.position.SetY( position != null ? position.GetY : 20); this.size.SetX( size != null ? size.GetX : 12); this.size.SetY( size != null ? size.GetY : 08); this.color = color != 0 ? color : ConsoleColor.Blue; } break;
                case ObjectType.Wheel:    { this.position.SetX( position != null ? position.GetX : 39); this.position.SetY( position != null ? position.GetY : 21); this.size.SetX( size != null ? size.GetX : 01); this.size.SetY( size != null ? size.GetY : 01); this.color = color != 0 ? color : ConsoleColor.DarkGray; } break;
                case ObjectType.Line:     { this.position.SetX( position != null ? position.GetX : 20); this.position.SetY( position != null ? position.GetY : 10); this.size.SetX( size != null ? size.GetX : 02); this.size.SetY( size != null ? size.GetY : 05); this.color = color != 0 ? color : ConsoleColor.Gray; } break;
                case ObjectType.House:    { this.position.SetX( position != null ? position.GetX : 05); this.position.SetY( position != null ? position.GetY : 20); this.size.SetX( size != null ? size.GetX : 15); this.size.SetY( size != null ? size.GetY : 15); this.color = color != 0 ? color : ConsoleColor.DarkYellow; } break;
                case ObjectType.Tree:     { this.position.SetX( position != null ? position.GetX : 40); this.position.SetY( position != null ? position.GetY : 10); this.size.SetX( size != null ? size.GetX : 08); this.size.SetY( size != null ? size.GetY : 08); this.color = color != 0 ? color : ConsoleColor.Green; } break;
                default: break;
            }
            this.renderLayer = renderLayer;
            for (int i = 0; i < childObject.Length; i++)
                ChildList.Add(childObject[i]);
        }

        //========
    }
}
