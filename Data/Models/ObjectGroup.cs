using System;

namespace ALB
{
    /// <summary>
    /// Класс для группы объектов в игре
    /// </summary>
    class ObjectGroup : ObjectSingle
    {
        public Vector Gap { get { return gap; } set { gap = value; } }
        public Vector Count { get { return count; } set { count = value; } }

        protected Vector gap = new Vector(0,0);
        protected Vector count = new Vector(0,0);

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
        public ObjectGroup(ObjectType objectType, int renderLayer = 0, Vector position = null, Vector size = null, ConsoleColor color = 0, Vector gap = null, Vector count = null, params ObjectSingle[] childObject)
            :base(objectType, renderLayer, position, size, color, childObject)
        {
            switch (objectType)
            {   //характеристики объектов по умолчанию
                case ObjectType.Car:     { this.gap.SetX(gap != null ? gap.GetX : 05); this.gap.SetY(gap != null ? gap.GetY : 05); this.count.SetX(count != null ? count.GetX : 05); this.count.SetY(count != null ? count.GetY : 05); } break;
                case ObjectType.Wheel:   { this.gap.SetX(gap != null ? gap.GetX : 05); this.gap.SetY(gap != null ? gap.GetY : 05); this.count.SetX(count != null ? count.GetX : 05); this.count.SetY(count != null ? count.GetY : 05); } break;
                case ObjectType.Line:    { this.gap.SetX(gap != null ? gap.GetX : 05); this.gap.SetY(gap != null ? gap.GetY : 05); this.count.SetX(count != null ? count.GetX : 05); this.count.SetY(count != null ? count.GetY : 05); } break;
                case ObjectType.House:   { this.gap.SetX(gap != null ? gap.GetX : 05); this.gap.SetY(gap != null ? gap.GetY : 05); this.count.SetX(count != null ? count.GetX : 05); this.count.SetY(count != null ? count.GetY : 05); } break;
                case ObjectType.Tree:    { this.gap.SetX(gap != null ? gap.GetX : 05); this.gap.SetY(gap != null ? gap.GetY : 05); this.count.SetX(count != null ? count.GetX : 05); this.count.SetY(count != null ? count.GetY : 05); } break;
                default: break;
            }
        }
        //========
    }
}
