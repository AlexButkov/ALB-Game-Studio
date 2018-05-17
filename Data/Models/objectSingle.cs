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
        public ConsoleColor Color { get; set; }
        public float Layer { get; set;}
        List<ObjectSingle> ChildList { get; set; } = new List<ObjectSingle>();
        public Controller Inspector { get; set; }

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
        public ObjectSingle(ObjType objectType, float? layer = null, Vector position = null , Vector size = null, ConsoleColor color = 0, params ObjectSingle[] childObject )
        {
            Inspector = new Controller(this);
            Position = new Vector(Inspector,VarType.positionX,VarType.positionY);
            Size = new Vector(Inspector, VarType.sizeX, VarType.sizeY);
            switch (objectType)
            {   //характеристики объектов по умолчанию
                case ObjType.Car:      { Position.SetX( position != null ? position.GetX : 40); Position.SetY( position != null ? position.GetY : 20); Size.SetX( size != null ? size.GetX : 12); Size.SetY( size != null ? size.GetY : 08); Color = color != 0 ? color : ConsoleColor.Blue; } break;
                case ObjType.Wheel:    { Position.SetX( position != null ? position.GetX : 39); Position.SetY( position != null ? position.GetY : 21); Size.SetX( size != null ? size.GetX : 01); Size.SetY( size != null ? size.GetY : 01); Color = color != 0 ? color : ConsoleColor.DarkGray; } break;
                case ObjType.Line:     { Position.SetX( position != null ? position.GetX : 20); Position.SetY( position != null ? position.GetY : 10); Size.SetX( size != null ? size.GetX : 02); Size.SetY( size != null ? size.GetY : 05); Color = color != 0 ? color : ConsoleColor.Gray; } break;
                case ObjType.House:    { Position.SetX( position != null ? position.GetX : 05); Position.SetY( position != null ? position.GetY : 20); Size.SetX( size != null ? size.GetX : 15); Size.SetY( size != null ? size.GetY : 15); Color = color != 0 ? color : ConsoleColor.DarkYellow; } break;
                case ObjType.Tree:     { Position.SetX( position != null ? position.GetX : 40); Position.SetY( position != null ? position.GetY : 10); Size.SetX( size != null ? size.GetX : 08); Size.SetY( size != null ? size.GetY : 08); Color = color != 0 ? color : ConsoleColor.Green; } break;
                default: break;
            }
            Layer = layer ?? 0 ;
            for (int i = 0; i < childObject.Length; i++)
                ChildList.Add(childObject[i]);
            //SceneList.Add(new Controller((ObjectGroup)this));
            if (GetType() != typeof(ObjectGroup))
                Inspector.SetArrayFull();
        }

        //========
    }
}
