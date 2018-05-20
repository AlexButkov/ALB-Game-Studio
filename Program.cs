using System;
using System.Threading;
using System.Collections;

namespace ALB
{
    class Program
    {

        static void Main()
        {
            View view = new View();
            view.Create();
            //View.StartThread(view.Create);
            /// <summary>
            /// основной цикл
            /// </summary>
            /*void Update()
            {
                while (true)
                {


                }
            }*/

            Console.ReadKey();
        }
    }
}
