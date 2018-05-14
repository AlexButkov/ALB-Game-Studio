using System;
using System.Threading;
using System.Collections;

namespace ALB
{
    public delegate void MDelegate();

    class View : Model
    {


        //========
        public View()
        {
            Blocker = new object();
            Starter = null;
            Starter += FirstMet;
            Starter += CreateThread;
            Starter();
        }
        //========
        void FirstMet()
        {
            Console.SetWindowSize(MaxWidth, MaxHeight);
            Console.CursorVisible = false;
            Console.Title = "ALB-1.0";
        }
        //----
        void CreateThread()
        {
            Thread thread = new Thread(Create);
            thread.Start();
        }
        //----
        void Create()
        {
            ObjectSingle obj = new ObjectSingle(Scene.Car, null ,PositionX.Right,PositionY.Down);
            new Pos(obj.X, obj.Y);
            ChangeView(obj);
        }
        //========
        /// <summary>
        /// Создание (отрисовка) объекта либо его удаление.
        /// </summary>
        /// <param name="sceneObject">Принимаемый объект</param>
        /// <param name="toDestroy">Удалить объект если "true". (Создать - "false", по умолчанию) </param>
        public static void ChangeView(ObjectSingle sceneObject, bool toDestroy = false)
        {
            ObjectSingle obj = sceneObject;
            int wdt = obj.Width;
            int hgt = obj.Height;
            int x = obj.X - wdt / 2 ;
            int y = obj.Y - hgt / 2 ;
            lock (Blocker)
            {
                Console.BackgroundColor = toDestroy ? DefaultColor : obj.Color;
                for (int j = 0; j < hgt; j++)
                {
                    for (int i = 0; i < wdt; i++)
                    {
                        Console.SetCursorPosition(x + i, y + j);
                        Console.Write(DefaultSymbol);
                    }
                }
                Console.SetCursorPosition(x, y);
                Console.BackgroundColor = DefaultColor;
            }
        }
        //----
        /// <summary>
        /// Создание (отрисовка) группы объектов либо ее удаление.
        /// </summary>
        /// <param name="SceneGroup">Принимаемая группа объектов</param>
        /// <param name="toDestroy">Удалить объект если "true". (Создать - "false", по умолчанию) </param>
        public static void ChangeView(ObjectGroup SceneGroup, bool toDestroy = false)
        {
            ObjectGroup obj = SceneGroup;
            int wdt = obj.Width;
            int hgt = obj.Height;
            int x = obj.X - wdt / 2;
            int y = obj.Y - hgt / 2;
            lock (Blocker)
            {
                Console.BackgroundColor = toDestroy ? DefaultColor : obj.Color;
                for (int l = 0; l < obj.CountY; l++)
                {
                    for (int k = 0; k < obj.CountX; k++)
                    {
                        for (int j = 0; j < hgt; j++)
                        {
                            for (int i = 0; i < wdt; i++)
                            {
                                Console.SetCursorPosition(x + i + (wdt + obj.GapX) * k, y + j + (hgt + obj.GapY) * l);
                                Console.Write(DefaultSymbol);
                            }
                        }
                        Console.SetCursorPosition(x, y);
                    }
                }
                Console.BackgroundColor = DefaultColor;
            }
        }
    }
}
