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
            Starter += Create; // Thread;
            Starter();
        }
        //========
        void FirstMet()
        {
            Console.SetWindowSize(WindowSize.GetX, WindowSize.GetY);
            Console.SetBufferSize(WindowSize.GetX+1, WindowSize.GetY);//+1: last pixel buffer(буффер для последнего пикселя)
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
            ObjectSingle obj = new ObjectSingle(ObjType.Car, 1);
            bool sw = false;
            while (true)
            {
                obj.Position.SetX(obj.Position.GetX+1);
                ChangeView(obj, sw);
                sw = !sw;
                //Console.WriteLine(SceneList[0].State);
            }
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
            int wdt = obj.Size.GetX;
            int hgt = obj.Size.GetY;
            int x = WindowSize.GetX / 2 + obj.Position.GetX - wdt / 2 ;
            int y = WindowSize.GetY / 2 + obj.Position.GetY - hgt / 2 ;
            lock (Blocker)
            {
                Console.BackgroundColor = toDestroy ? DefaultColor : obj.Color;
                for (int j = 0; j < hgt; j++)
                {
                    for (int i = 0; i < wdt; i++)
                    {
                        Console.SetCursorPosition(CheckSizeX(x + i), CheckSizeY(y + j));
                        Console.Write(DefaultSymbol);
                    }
                }
                Console.SetCursorPosition(CheckSizeX(x), CheckSizeY(y));
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
            int wdt = obj.Size.GetX;
            int hgt = obj.Size.GetY;
            int x = WindowSize.GetX / 2 + obj.Position.GetX - wdt / 2;
            int y = WindowSize.GetY / 2 + obj.Position.GetY - hgt / 2;
            int w = wdt + obj.Gap.GetX;
            int h = hgt + obj.Gap.GetY;
            lock (Blocker)
            {
                Console.BackgroundColor = toDestroy ? DefaultColor : obj.Color;
                for (int l = 0; l < obj.Quant.GetY; l++)
                {
                    for (int k = 0; k < obj.Quant.GetX; k++)
                    {
                        for (int j = 0; j < hgt; j++)
                        {
                            for (int i = 0; i < wdt; i++)
                            {
                                Console.SetCursorPosition(CheckSizeX(x + i + w * k), CheckSizeY(y + j + h * l));
                                Console.Write(DefaultSymbol);
                            }
                        }
                        Console.SetCursorPosition(CheckSizeX(x), CheckSizeY(y));
                    }
                }
                Console.BackgroundColor = DefaultColor;
            }
        }
    }
}
