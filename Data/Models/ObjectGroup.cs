using System;
using System.Threading;
using System.Collections.Generic;

namespace ALB
{
    /// <summary>
    /// game object group class (класс группы игровых объектов)
    /// </summary>
    class ObjectGroup : ObjectSingle
    {
        /// <summary>
        /// X/Y-axis interval (интервал по осям X/Y)
        /// </summary>
        public Vector Gap;

        /// <summary>
        /// X/Y-axis quantity (количество по осям X/Y)
        /// </summary>
        public Vector Quant;

        //========
        /// <summary>
        /// constructs game object group class(создает класс группы игровых объектов)
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
        /// <param name="gapX">X-axis interval (интервал по оси X)</param>
        /// <param name="gapY">Y-axis interval (интервал по оси X)</param>
        /// <param name="quantX">X-axis quantity (количество по оси X)</param>
        /// <param name="quantY">Y-axis quantity (количество по оси X)</param>
        public ObjectGroup(Preset? preset = null, ConsoleColor? color = null, string tag = null, float? layer = null, float? positionX = null, float? positionY = null, float? sizeX = null, float? sizeY = null, float? gapX = null, float? gapY = null, float? quantX = null, float? quantY = null, params ObjectSingle[] childObject)
            :base(preset, color, tag, layer, positionX, positionY, sizeX, sizeY, childObject)
        {
            Inspection.IsParentGroup = true;
            Gap = new Vector(null, null, this, Param.gapX, Param.gapY);
            Quant = new Vector(null, null, this, Param.quantX, Param.quantY);
            //характеристики объектов по умолчанию
            Gap.X = gapX ?? 1.GridX();
            Gap.Y = gapY ?? 1.GridY();
            Quant.X = quantX ?? 2;
            Quant.Y = quantY ?? 2;
            //---
            Inspection.SetArrayFull();
        }
        //========
    }
}
