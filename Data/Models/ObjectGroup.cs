using System;

namespace ALB
{
    /// <summary>
    /// Класс для группы объектов в игре
    /// </summary>
    class ObjectGroup : ObjectSingle
    {
        public Vector Gap { get; set; }
        public Vector Quant { get; set; }

        //========
        /// <summary>
        /// конструктор экземпляра класса для группы объектов в игре (в первом параметре указывается объект перечисляемого типа "ObjectType", иначе создается объект с нулевыми значениями)
        /// </summary>
        //public ObjectGroup() { } // : base() { }
        /// <summary>
        /// конструктор экземпляра класса для группы объектов (в первом параметре указывается объект перечисляемого типа "ObjectType", иначе создается объект с нулевыми значениями)
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
        /// <param name="gapX">расстояние между объектами в группе по оси X</param>
        /// <param name="gapY">расстояние между объектами в группе по оси Y</param>
        /// <param name="countX">количество объектов в группе по оси X</param>
        /// <param name="countY">количество объектов в группе по оси Y</param>
        public ObjectGroup(ObjType objectType, int renderLayer = 0, float? positionX = null, float? positionY = null, float? sizeX = null, float? sizeY = null, ConsoleColor? color = null, float? gapX = null, float? gapY = null, float? countX = null, float? countY = null, params ObjectSingle[] childObject)
            :base(objectType, renderLayer, positionX, positionY, sizeX, sizeY, color, childObject)
        {
            Inspection.ParentGroup = this;
            Gap = new Vector(null, null, Inspection, Task.gapX, Task.gapY);
            Quant = new Vector(null, null, Inspection, Task.quantX, Task.quantY);
            switch (objectType)
            {   //характеристики объектов по умолчанию
                case ObjType.Car:     { Gap.X = gapX ??  05; Gap.Y = gapY ??  05; Quant.X = countX ??  05; Quant.Y = countY ??  05; } break;
                case ObjType.Wheel:   { Gap.X = gapX ??  05; Gap.Y = gapY ??  05; Quant.X = countX ??  05; Quant.Y = countY ??  05; } break;
                case ObjType.Line:    { Gap.X = gapX ??  05; Gap.Y = gapY ??  05; Quant.X = countX ??  05; Quant.Y = countY ??  05; } break;
                case ObjType.House:   { Gap.X = gapX ??  05; Gap.Y = gapY ??  05; Quant.X = countX ??  05; Quant.Y = countY ??  05; } break;
                case ObjType.Tree:    { Gap.X = gapX ??  05; Gap.Y = gapY ??  05; Quant.X = countX ??  05; Quant.Y = countY ??  05; } break;
                default: break;
            }
            Inspection.SetArrayFull();
        }
        //========
    }
}
