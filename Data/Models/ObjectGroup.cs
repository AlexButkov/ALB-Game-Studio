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
        /// <param name="position">координаты позиций по осям X/Y</param>
        /// <param name="size">размеры по осям X/Y</param>
        /// <param name="color">цвет объекта</param>
        /// <param name="childObject">список объектов, позиции которых будут связаны с текущим объектом</param>
        /// <param name="gap">расстояние между объектами в группе по осям X/Y</param>
        /// <param name="count">количество объектов в группе по осям X/Y</param>
        /// <param name="СountGetY"></param>
        public ObjectGroup(ObjType objectType, int renderLayer = 0, Vector position = null, Vector size = null, ConsoleColor color = 0, Vector gap = null, Vector count = null, params ObjectSingle[] childObject)
            :base(objectType, renderLayer, position, size, color, childObject)
        {
            Gap = new Vector(Inspector, Task.gapX, Task.gapY);
            Quant = new Vector(Inspector, Task.quantX, Task.quantY);
            switch (objectType)
            {   //характеристики объектов по умолчанию
                case ObjType.Car:     { Gap.SetX(gap != null ? gap.GetX : 05); Gap.SetY(gap != null ? gap.GetY : 05); Quant.SetX(count != null ? count.GetX : 05); Quant.SetY(count != null ? count.GetY : 05); } break;
                case ObjType.Wheel:   { Gap.SetX(gap != null ? gap.GetX : 05); Gap.SetY(gap != null ? gap.GetY : 05); Quant.SetX(count != null ? count.GetX : 05); Quant.SetY(count != null ? count.GetY : 05); } break;
                case ObjType.Line:    { Gap.SetX(gap != null ? gap.GetX : 05); Gap.SetY(gap != null ? gap.GetY : 05); Quant.SetX(count != null ? count.GetX : 05); Quant.SetY(count != null ? count.GetY : 05); } break;
                case ObjType.House:   { Gap.SetX(gap != null ? gap.GetX : 05); Gap.SetY(gap != null ? gap.GetY : 05); Quant.SetX(count != null ? count.GetX : 05); Quant.SetY(count != null ? count.GetY : 05); } break;
                case ObjType.Tree:    { Gap.SetX(gap != null ? gap.GetX : 05); Gap.SetY(gap != null ? gap.GetY : 05); Quant.SetX(count != null ? count.GetX : 05); Quant.SetY(count != null ? count.GetY : 05); } break;
                default: break;
            }
            Inspector.SetArrayFull();
        }
        //========
    }
}
