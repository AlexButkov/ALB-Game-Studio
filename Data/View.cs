﻿using System;
using System.Threading;
using System.Collections.Generic;
using System.Reflection;

namespace ALB
{
    /// <summary>
    /// provides game objects rendering (обеспечивает рендеринг игровых объектов)
    /// </summary>
    class View : Model
    {
        /// <summary>
        /// (делегат для методов Start из всех наследников класса <see cref="ALBGame"/>)
        /// </summary>
        public Action StartSum;
        /// <summary>
        /// (делегат для методов Update из всех наследников класса <see cref="ALBGame"/>)
        /// </summary>
        public Action UpdateSum;
        //---
        static Type GameLogic = typeof(ALBGame);
        static Assembly assembly = GameLogic.Assembly;
        static Type[] allTypes = assembly.GetTypes();
        //========
        /// <summary>
        /// (основная инициализация)
        /// </summary>
        public void MainInitialize()
        {
            Console.SetWindowSize((int)WindowSize.X, (int)WindowSize.Y);
            Console.SetBufferSize((int)WindowSize.X + 1, (int)WindowSize.Y);//+1: last pixel buffer(буффер для последнего пикселя)
            Console.CursorVisible = false;
            Console.Title = "ALB-1.0";
            
            for (int j = 0; j < WindowSize.Y; j++)
            {
                for (int i = 0; i < WindowSize.X; i++)
                {
                    WindowArray[i, j] = new List<float[]>();
                }
            }
            CheckAllTypes();
        }
        /// <summary>
        /// <see cref="ALBGame"/> class implementation check (проверка наличия подходящей реализации класса <see cref="ALBGame"/>)
        /// </summary>
        void CheckAllTypes()
        {
            for (int i = 0; i < allTypes.Length; i++)
            {
                Type t = allTypes[i];
                if (t.IsSubclassOf(GameLogic) && !t.IsAbstract)
                {
                    MethodInfo start = t.GetMethod("Start");
                    MethodInfo update = t.GetMethod("Update");
                    Object Instance = Activator.CreateInstance(t);
                    if (start != null)
                    {
                        StartSum += (Action)Delegate.CreateDelegate(typeof(Action), Instance, start);
                    }
                    if (update != null)
                    {
                        UpdateSum += (Action)Delegate.CreateDelegate(typeof(Action), Instance, update);
                    }
                }
            }
            if (StartSum == null || UpdateSum == null)
            {
                Exception newEx = new Exception("There is no appropriate ALBGame class implementation.");
                throw newEx;
            }
        }
        //========
        /// <summary>
        /// provides game object rendering (Oбеспечивает рендеринг игрового объекта. Вызывается автоматически при изменении соответствующих параметров объекта.)
        /// </summary>
        public static void DrawObject(List<dynamic>[] tempArray, bool[] actionToggle, ObjectSingle parentObject, bool isParentGroup)
        {
            BoxBuilder builder = new BoxBuilder();
            List<int[]> boxes;
            bool isLast;
            ConsoleColor locol;
            ConsoleColor col = (ConsoleColor)tempArray[(int)Param.color][tempArray[(int)Param.color].Count - 1];
            float lay = (float)tempArray[(int)Param.layer][tempArray[(int)Param.layer].Count - 1];
            //current fixed values(текущие фиксированные значения)
            int sx = (int)tempArray[(int)Param.sizeX][tempArray[(int)Param.sizeX].Count - 1];
            int sy = (int)tempArray[(int)Param.sizeY][tempArray[(int)Param.sizeY].Count - 1];
            int px = (int)tempArray[(int)Param.positionX][tempArray[(int)Param.positionX].Count - 1] + (int)WindowSize.X / 2 - sx / 2;
            int py = (int)tempArray[(int)Param.positionY][tempArray[(int)Param.positionY].Count - 1] + (int)WindowSize.Y / 2 - sy / 2;
            int sumX;
            int sumY;
            //previous values(предыдущие значения)
            int _sx = (int)tempArray[(int)Param.sizeX][0];
            int _sy = (int)tempArray[(int)Param.sizeY][0];
            int _px = (int)tempArray[(int)Param.positionX][0] + (int)WindowSize.X / 2 - _sx / 2;
            int _py = (int)tempArray[(int)Param.positionY][0] + (int)WindowSize.Y / 2 - _sy / 2;
            int _sumX;
            int _sumY;
            //---
            if (isParentGroup)
            {
                if (actionToggle[(int)Draw.destroy])
                {
                    RedrawGroup(false, true);
                }
                else
                {
                    if (actionToggle[(int)Draw.vector])
                    {
                        RedrawGroup(true, true);
                        if (!actionToggle[(int)Draw.color])
                        {
                            RedrawGroup(true, false);
                        }
                    }
                    if (actionToggle[(int)Draw.color])
                    {
                        RedrawGroup(false, false);
                    }
                }
                for (int i = 0; i < actionToggle.Length; i++)
                {
                    actionToggle[i] = false;
                }
            }
            else
            {
                if (actionToggle[(int)Draw.destroy])
                {
                    RedrawSingle(false, true);
                }
                else
                {
                    if (actionToggle[(int)Draw.vector])
                    {
                        RedrawSingle(true, true);
                        if (!actionToggle[(int)Draw.color])
                        {
                            RedrawSingle(true, false);
                        }
                    }
                    if (actionToggle[(int)Draw.color])
                    {
                        RedrawSingle(false, false);
                    }
                }
                for (int i = 0; i < actionToggle.Length; i++)
                {
                    actionToggle[i] = false;
                }
            }

            //----
            /// <summary>
            /// Создание (отрисовка) объекта либо его удаление.
            /// </summary>
            void RedrawSingle(bool isPartial, bool isRemove)
            {
                
                if (isRemove)
                {
                    ArrayBlocker.WaitOne();
                    for (int j = 0; j < _sy; j++)
                    {
                        for (int i = 0; i < _sx; i++)
                        {
                            isLast = (j + 1 == _sy && i + 1 == _sx) ? true : false;
                            _sumX = _px + i;
                            _sumY = _py + j;
                            sumX = px;
                            sumY = py;
                            Remove(_sumX, _sumY, sumX, sumY, isPartial, isLast);
                        }
                    }
                    ArrayBlocker.ReleaseMutex();
                }
                else
                {
                    ArrayBlocker.WaitOne();
                    for (int j = 0; j < sy; j++)
                    {
                        for (int i = 0; i < sx; i++)
                        {
                            isLast = (j + 1 == sy && i + 1 == sx) ? true : false;
                            sumX = px + i;
                            sumY = py + j;
                            _sumX = _px;
                            _sumY = _py;
                            Redraw(sumX, sumY, _sumX, _sumY, isPartial, isLast);
                        }
                    }
                    ArrayBlocker.ReleaseMutex();
                }
                
            }
            //----
            /// <summary>
            /// Создание (отрисовка) группы объектов либо ее удаление.
            /// </summary>
            void RedrawGroup(bool isPartial, bool isRemove)
            {
                //current fixed values(текущие фиксированные значения)
                int gx = (int)tempArray[(int)Param.gapX][tempArray[(int)Param.gapX].Count - 1] + sx;
                int gy = (int)tempArray[(int)Param.gapY][tempArray[(int)Param.gapY].Count - 1] + sy;
                int qx = (int)tempArray[(int)Param.quantX][tempArray[(int)Param.quantX].Count - 1];
                int qy = (int)tempArray[(int)Param.quantY][tempArray[(int)Param.quantY].Count - 1];
                int hx = (sx + gx * (qx - 1)) / 2;
                int hy = (sy + gy * (qy - 1)) / 2;
                px = (int)tempArray[(int)Param.positionX][tempArray[(int)Param.positionX].Count - 1] + (int)WindowSize.X / 2 - hx;
                py = (int)tempArray[(int)Param.positionY][tempArray[(int)Param.positionY].Count - 1] + (int)WindowSize.Y / 2 - hy;
                //previous values(предыдущие значения)
                int _gx = (int)tempArray[(int)Param.gapX][0] + _sx;
                int _gy = (int)tempArray[(int)Param.gapY][0] + _sy;
                int _qx = (int)tempArray[(int)Param.quantX][0];
                int _qy = (int)tempArray[(int)Param.quantY][0];
                int _hx = (_sx + _gx * (_qx - 1)) / 2;
                int _hy = (_sy + _gy * (_qy - 1)) / 2;
                _px = (int)tempArray[(int)Param.positionX][0] + (int)WindowSize.X / 2 - _hx;
                _py = (int)tempArray[(int)Param.positionY][0] + (int)WindowSize.Y / 2 - _hy;

                
                if (isRemove)
                {
                    ArrayBlocker.WaitOne();
                    for (int l = 0; l < _qy; l++)
                    {
                        for (int k = 0; k < _qx; k++)
                        {
                            for (int j = 0; j < _sy; j++)
                            {
                                for (int i = 0; i < _sx; i++)
                                {
                                    isLast = (l + 1 == _qy && k + 1 == _qx && j + 1 == _sy && i + 1 == _sx) ? true : false;
                                    _sumX = _px + i + _gx * k;
                                    _sumY = _py + j + _gy * l;
                                    sumX = px + gx * k;
                                    sumY = py + gy * l;
                                    Remove(_sumX, _sumY, sumX, sumY, isPartial, isLast);
                                }
                            }
                        }
                    }
                    ArrayBlocker.ReleaseMutex();
                }
                else
                {
                    ArrayBlocker.WaitOne();
                    for (int l = 0; l < qy; l++)
                    {
                        for (int k = 0; k < qx; k++)
                        {
                            for (int j = 0; j < sy; j++)
                            {
                                for (int i = 0; i < sx; i++)
                                {
                                    isLast = (l + 1 == qy && k + 1 == qx && j + 1 == sy && i + 1 == sx) ? true : false;
                                    sumX = px + i + gx * k;
                                    sumY = py + j + gy * l;
                                    _sumX = _px + _gx * k;
                                    _sumY = _py + _gy * l;
                                    Redraw(sumX, sumY, _sumX, _sumY, isPartial, isLast);
                                }
                            }
                        }
                    }
                    ArrayBlocker.ReleaseMutex();
                }
                
            }
            //----
            void Redraw(int x, int y, int _x, int _y, bool isPartial, bool isItLast)
            {
                if (CheckPrint(x, y))
                {
                    if (!isPartial || !((x >= _x && x < _x + _sx) && (y >= _y && y < _y + _sy)))
                    {
                        if (WindowArray[x, y].Count == 0 || lay >= WindowArray[x, y][0][1])
                        {
                            DrawBox(col, x, y);
                        }
                        if (actionToggle[(int)Draw.color])
                        {
                            WindowArray[x, y].RemoveAll(obj => obj[2].Equals(parentObject.GetHashCode()));
                        }
                        if (!WindowArray[x, y].Exists(obj => obj[2].Equals(parentObject.GetHashCode())))
                        {
                        if (WindowArray[x, y].Count == 0)
                        {
                            WindowArray[x, y].Add(new float[] { (int)col, lay, parentObject.GetHashCode() });
                            }
                            else
                            {
                                for (int n = 0; n < WindowArray[x, y].Count; n++)
                                {
                                    if (lay >= WindowArray[x, y][n][1])
                                    {
                                        WindowArray[x, y].Insert(n, new float[] { (int)col, lay, parentObject.GetHashCode() });
                                        break;
                                    }
                                    else if (n == WindowArray[x, y].Count - 1)
                                    {
                                        WindowArray[x, y].Add(new float[] { (int)col, lay, parentObject.GetHashCode() });
                                    }
                                }
                            }
                        }
                    }
                }
                if (isItLast)
                {
                    DrawBoxLast();
                }
            }
            //----
            void Remove(int _x, int _y, int x, int y, bool isPartial, bool isItLast)
            {
                if (CheckPrint(_x, _y))
                {
                    if (!isPartial || !((_x >= x && _x < x + sx) && (_y >= y && _y < y + sy)))
                    {
                        WindowArray[_x, _y].RemoveAll(obj => obj[2].Equals(parentObject.GetHashCode()));
                        locol = WindowArray[_x, _y].Count == 0 ? DefaultColor : (ConsoleColor)(int)WindowArray[_x, _y][0][0];
                        DrawBox(locol, _x, _y);
                    }
                }
                if (isItLast)
                {
                    DrawBoxLast();
                }

            }
            //----
            /// <summary>
            /// draws box at last point (отрисовывает квадратный элемент в последней точке) 
            /// </summary>
            void DrawBoxLast()
            {
                boxes = builder.LastPixel();
                if (boxes != null)
                {
                    for (int i = 0; i < boxes.Count; i++)
                    {
                        DrawPixel((ConsoleColor)boxes[i][0], boxes[i][1], boxes[i][2], boxes[i][3], boxes[i][4]);
                    }
                }
            }
            //----
            /// <summary>
            /// draws box at point (отрисовывает квадратный элемент в точке) 
            /// </summary>
            /// <param name="color">color to draw (цвет для отрисовки)</param>
            /// <param name="x">point X coordinate (координата точки по оси X)</param>
            /// <param name="y">point Y coordinate (координата точки по оси Y)</param>
            void DrawBox(ConsoleColor color, int x, int y)
            {
                boxes = builder.AddPixel(color, x, y);
                if (boxes != null)
                {
                    for (int i = 0; i < boxes.Count; i++)
                    {
                        DrawPixel((ConsoleColor)boxes[i][0], boxes[i][1], boxes[i][2], boxes[i][3], boxes[i][4]); 
                    }
                }
            }
        }
        //----
        /// <summary>
        /// draws pixel at point (отрисовывает пиксель в точке) 
        /// </summary>
        /// <param name="color">color to draw (цвет для отрисовки)</param>
        /// <param name="x">point X coordinate (координата точки по оси X)</param>
        /// <param name="y">point Y coordinate (координата точки по оси Y)</param>
        /// <param name="sizeX">X-axis size (размер по оси X)</param>
        /// <param name="sizeX">Y-axis size (размер по оси Y)</param>
        static void DrawPixel(ConsoleColor color, int x, int y, int sizeX = 0, int sizeY = 0)
        {
            lock (ConsoleBlocker)
            {
                Console.MoveBufferArea(Math.Max(0, x), Math.Max(0, y), Math.Max(1, sizeX), Math.Max(1, sizeY), (int)WindowSize.X, (int)WindowSize.Y - 1, DefaultSymbol, ConsoleColor.Black, color);
            }
        }
    }
}
