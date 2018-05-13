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
            SceneObject obj = new SceneObject(Scene.Car, null ,PositionX.Middle,PositionY.Middle);
            ChangeView(obj);
        }
        //========
        /// <summary>
        /// Создание (отрисовка) объекта либо его удаление.
        /// </summary>
        /// <param name="sceneObject">Принимаемый объект</param>
        /// <param name="toDestroy">Удалить объект если "true". (Создать - "false", по умолчанию) </param>
        public static void ChangeView(SceneObject sceneObject, bool toDestroy = false)
        {
            SceneObject obj = sceneObject;
            int x = obj.X;
            int y = obj.Y;
            int wdt = obj.Width;
            int hgt = obj.Height;
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
        public static void ChangeView(SceneGroup SceneGroup, bool toDestroy = false)
        {
            SceneGroup obj = SceneGroup;
            int x = obj.X;
            int y = obj.Y;
            int wdt = obj.Width;
            int hgt = obj.Height;
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
