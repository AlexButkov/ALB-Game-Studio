using System;

namespace ALB
{
    abstract class ALBGame : Model
    {
        /// <summary> gets called on startup only (выполняется только при запуске) </summary>
        abstract public void Start();
        /// <summary> gets called every frame (выполняется для каждого кадра) </summary>
        abstract public void Update();
    }

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
        copyObject,
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
