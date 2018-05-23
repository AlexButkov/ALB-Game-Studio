using System;

namespace ALB
{
    enum ObjType : int
    {
        Car,
        Wheel,
        Line,
        House,
        Tree
    }
    enum SideX : int
    {
        Left=-1,
        Middle,
        Right
    }
    enum SideY : int
    {
        Up=-1,
        Middle,
        Down
    }
    enum Task : int
    {
        isDestroyed,
        layer,
        positionX,
        positionY,
        sizeX,
        sizeY,
        color,
        gapX,
        gapY,
        quantX,
        quantY,
        max //max value variable (переменная с максимальным значением)
    }
    enum Draw : int
    { 
        some,
        destroy,
        vector,
        layer,
        color,
        max //max value variable (переменная с максимальным значением)
    }
}
