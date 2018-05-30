using System;
using System.Collections.Generic;

namespace ALB
{
    class NewGame : ALBGame
    {
        ObjectSingle plane;
        public override void Start()
        {
            plane = new ObjectSingle(ObjType.Tree, 0.1f, null, null, WindowSize.X, WindowSize.Y, ConsoleColor.Gray);

        }

        public override void Update()
        {
            plane.MoveAside(1,SideX.Left, SideY.Middle);
        }
    }
}
