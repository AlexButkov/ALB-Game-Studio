using System;
using System.Threading;
using System.Collections.Generic;

namespace ALB
{
    /// <summary> abstract game logic class (абстрактный класс игровой логики)</summary>
    abstract class ALBGame : Model
    {
        /// <summary> gets called on startup only (выполняется только при запуске) </summary>
        abstract public void Start();
        /// <summary> gets called every frame (выполняется для каждого кадра) </summary>
        abstract public void Update();
    }

    /// <summary>
    /// preset object (преднастроенный объект)
    /// </summary>
    enum Preset : int
    {
        boxS,
        boxM,
        boxL,
        boxXL,
        boxXXL,
        plane
    }
    /// <summary>
    /// X-axis side (сторона по оси X)
    /// </summary>
    enum SideX : int
    {
        left=-1,
        middle,
        right
    }
    /// <summary>
    /// Y-axis side (сторона по оси Y)
    /// </summary>
    enum SideY : int
    {
        up=-1,
        middle,
        down
    }
    /// <summary>rendering variables names (названия влияющих на рендеринг переменных)</summary>
    enum Param : int
    {
        isDrawn,
        color,
        layer,
        positionX,
        positionY,
        sizeX,
        sizeY,
        gapX,
        gapY,
        quantX,
        quantY,
        copyObject,
        /// <summary>max value variable (переменная с максимальным значением)</summary>
        max
    }
    /// <summary>rendering task names (названия задач для рендеринга)</summary>
    enum Draw : int
    { 
        some,
        destroy,
        vector,
        color,
        /// <summary>max value variable (переменная с максимальным значением)</summary>
        max
    }
}
