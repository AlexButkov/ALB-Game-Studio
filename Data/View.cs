using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace ALB
{
    class View : Model
    {
        //========
        public View()
        {
            Console.SetWindowSize((int)WindowSize.X, (int)WindowSize.Y);
            Console.SetBufferSize((int)WindowSize.X + 1, (int)WindowSize.Y);//+1: last pixel buffer(буффер для последнего пикселя)
            Console.CursorVisible = false;
            Console.Title = "ALB-1.0";

            for (int j = 0; j < WindowSize.Y; j++)
            {
                for (int i = 0; i < WindowSize.X; i++)
                {
                    WindowArray[i,j] = new List<ObjectSingle>();
                }
            }
        }
        //========
        public static void DrawObject(List<dynamic>[] valueArray, bool[] actionToggle, ObjectSingle parentObject, bool isParentGroup)
        {
            //current fixed values(текущие фиксированные значения)
            ConsoleColor col = (ConsoleColor)valueArray[(int)Task.color][valueArray[(int)Task.color].Count - 1];
            float lay = (float)valueArray[(int)Task.layer][valueArray[(int)Task.layer].Count - 1];
            int sx = (int)valueArray[(int)Task.sizeX][valueArray[(int)Task.sizeX].Count - 1];
            int sy = (int)valueArray[(int)Task.sizeY][valueArray[(int)Task.sizeY].Count - 1];
            int px = (int)valueArray[(int)Task.positionX][valueArray[(int)Task.positionX].Count - 1] + (int)WindowSize.X / 2 - sx / 2;
            int py = (int)valueArray[(int)Task.positionY][valueArray[(int)Task.positionY].Count - 1] + (int)WindowSize.Y / 2 - sy / 2;
            int sumX;
            int sumY;
            //previous values(предыдущие значения)
            int _sx = (int)valueArray[(int)Task.sizeX][0];
            int _sy = (int)valueArray[(int)Task.sizeY][0];
            int _px = (int)valueArray[(int)Task.positionX][0] + (int)WindowSize.X / 2 - _sx / 2;
            int _py = (int)valueArray[(int)Task.positionY][0] + (int)WindowSize.Y / 2 - _sy / 2;
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
                        if (!actionToggle[(int)Draw.layer] && !actionToggle[(int)Draw.color])
                        {
                            RedrawGroup(true, false);
                        }
                    }
                    if (actionToggle[(int)Draw.layer] || actionToggle[(int)Draw.color])
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
                        if (!actionToggle[(int)Draw.layer] && !actionToggle[(int)Draw.color])
                        {
                            RedrawSingle(true, false);
                        }
                    }
                    if (actionToggle[(int)Draw.layer] || actionToggle[(int)Draw.color])
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
            /// <param name="toDestroy">Удалить объект если "true". (Создать - "false", по умолчанию) </param>
            void RedrawSingle(bool isPartial, bool isRemove)
            {
                if(isRemove)
                {
                    for (int j = 0; j < _sy; j++)
                    {
                        for (int i = 0; i < _sx; i++)
                        {
                            _sumX = _px + i;
                            _sumY = _py + j;
                            sumX = px;
                            sumY = py;
                            Remove(_sumX, _sumY, sumX, sumY, isPartial);
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < sy; j++)
                    {
                        for (int i = 0; i < sx; i++)
                        {
                            sumX = px + i;
                            sumY = py + j;
                            _sumX = _px;
                            _sumY = _py;
                            Redraw(sumX, sumY, _sumX, _sumY, isPartial);
                        }
                    }
                }
                //Console.BackgroundColor = DefaultColor;
            }
            //----
            /// <summary>
            /// Создание (отрисовка) группы объектов либо ее удаление.
            /// </summary>
            /// <param name="SceneGroup">Принимаемая группа объектов</param>
            /// <param name="toDestroy">Удалить объект если "true". (Создать - "false", по умолчанию) </param>
            void RedrawGroup(bool isPartial, bool isRemove)
            {
                //current fixed values(текущие фиксированные значения)
                int gx = (int)valueArray[(int)Task.gapX][valueArray[(int)Task.gapX].Count - 1] + sx;
                int gy = (int)valueArray[(int)Task.gapY][valueArray[(int)Task.gapY].Count - 1] + sy;
                int qx = (int)valueArray[(int)Task.quantX][valueArray[(int)Task.quantX].Count - 1];
                int qy = (int)valueArray[(int)Task.quantY][valueArray[(int)Task.quantY].Count - 1];
                px = (int)valueArray[(int)Task.positionX][valueArray[(int)Task.positionX].Count - 1] + (int)WindowSize.X / 2 - (sx + gx * (qx - 1)) / 2;
                py = (int)valueArray[(int)Task.positionY][valueArray[(int)Task.positionY].Count - 1] + (int)WindowSize.Y / 2 - (sy + gy * (qy - 1)) / 2;
                //previous values(предыдущие значения)
                int _gx = (int)valueArray[(int)Task.gapX][0] + _sx;
                int _gy = (int)valueArray[(int)Task.gapY][0] + _sy;
                int _qx = (int)valueArray[(int)Task.quantX][0];
                int _qy = (int)valueArray[(int)Task.quantY][0];
                int _hx = (_sx + _gx * (_qx - 1)) / 2;
                int _hy = (_sy + _gy * (_qy - 1)) / 2;
                _px = (int)valueArray[(int)Task.positionX][0] + (int)WindowSize.X / 2 - _hx;
                _py = (int)valueArray[(int)Task.positionY][0] + (int)WindowSize.Y / 2 - _hy;

                if (isRemove)
                {
                    for (int l = 0; l < _qy; l++)
                    {
                        for (int k = 0; k < _qx; k++)
                        {
                            for (int j = 0; j < _sy; j++)
                            {
                                for (int i = 0; i < _sx; i++)
                                {
                                    _sumX = _px + i + _gx * k;
                                    _sumY = _py + j + _gy * l;
                                    sumX = px + gx * k;
                                    sumY = py + gy * l;
                                    Remove(_sumX, _sumY, sumX, sumY, isPartial);
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int l = 0; l < qy; l++)
                    {
                        for (int k = 0; k < qx; k++)
                        {
                            for (int j = 0; j < sy; j++)
                            {
                                for (int i = 0; i < sx; i++)
                                {
                                    sumX = px + i + gx * k;
                                    sumY = py + j + gy * l;
                                    _sumX = _px + _gx * k;
                                    _sumY = _py + _gy * l;
                                    Redraw(sumX, sumY, _sumX, _sumY, isPartial);
                                }
                            }
                        }
                    }
                }

            }
            //----
            void Redraw(int x, int y, int _x, int _y, bool isPartial)
            {
                if (CheckPrint(x, y))
                {
                    if (!isPartial || !((x >= _x && x < _x + _sx) && (y >= _y && y < _y + _sy)))
                    {
                        lock (DrawBlocker)
                        {
                            if (WindowArray[x, y].Count == 0 || lay >= WindowArray[x, y][0].Layer)
                            {
                                    Console.BackgroundColor = col;
                                    Console.SetCursorPosition(x, y);
                                    Console.Write(DefaultSymbol);
                            }
                            if (actionToggle[(int)Draw.layer])
                            {
                                WindowArray[x, y].RemoveAll(obj => obj.Equals(parentObject));
                            }
                            if (!WindowArray[x, y].Contains(parentObject))
                            {
                                if (WindowArray[x, y].Count == 0)
                                {
                                    WindowArray[x, y].Add(parentObject);
                                }
                                else
                                {
                                    for (int n = 0; n < WindowArray[x, y].Count; n++)
                                    {
                                        if (lay >= WindowArray[x, y][n].Layer)
                                        {
                                            WindowArray[x, y].Insert(n, parentObject);
                                            break;
                                        }
                                        else if (n == WindowArray[x, y].Count - 1)
                                        {
                                            WindowArray[x, y].Add(parentObject);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //----
            void Remove(int _x, int _y, int x, int y, bool isPartial)
            {
                if (CheckPrint(_x, _y))
                {
                    if (!isPartial || !((_x >= x && _x < x + sx) && (_y >= y && _y < y + sy)))
                    {
                        lock (DrawBlocker)
                        {
                            WindowArray[_x, _y].RemoveAll(obj => obj.Equals(parentObject));

                            Console.BackgroundColor = WindowArray[_x, _y].Count == 0 ? DefaultColor : WindowArray[_x, _y][0].Color;
                            Console.SetCursorPosition(_x, _y);
                            Console.Write(DefaultSymbol);
                        }
                    }
                }
            }
            //----
        }
    }
}
