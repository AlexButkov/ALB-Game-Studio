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
    enum PosTypeX : int
    {
        Left=-1,
        Middle,
        Right
    }
    enum PosTypeY : int
    {
        Up=-1,
        Middle,
        Down
    }
    enum VarType : int
    { 
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
        def //max value variable for ValueArray (переменная с максимальным значением для ValueArray)
    }
}
