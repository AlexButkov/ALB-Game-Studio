using System;
using System.Collections.Generic;
using System.Threading;
using System.Reflection;


namespace ALB
{
    /// <summary> 
    /// sample game logic (образец игровой логики)
    /// </summary>
    class SampleGame : ALBGame
    {
        //==== initialization ====
        //background
        ObjectSingle plane;
        ObjectSingle border;
        ObjectSingle road;
        ObjectGroup firstLine;
        ObjectGroup secondLine;
        int linesQuantY = 3;
        float linesSizeY;
        float startSpeed = 5f;
        float currentSpeed;
        static float windowMult = WindowSize.Y / 80;
        //text
        float textSpeed = 50f;
        float mphMult = 2f / windowMult;
        ObjectSingle textA;
        ObjectSingle textB;
        int charCounter;
        string messageA;
        string messageB;
        string messageC;
        bool messageOn = true;
        Vector textPoint;
        //cars
        ObjectSingle car;
        float carPosX;
        ObjectGroup windows;
        ObjectGroup wheels;
        ObjectSingle heroCar;
        ObjectSingle restartCar;
        float heroStep;
        List<ObjectSingle> enemies = new List<ObjectSingle>();
        List<ObjectSingle> other;
        string enemyTag = "enemy";
        bool isEnd;
        //environment
        ObjectSingle house;
        ObjectGroup houseB;
        ObjectSingle houseC;
        ObjectSingle tree;
        ObjectSingle treeB;
        ObjectSingle treeC;
        List<ObjectSingle> environs = new List<ObjectSingle>();
        float environsPosY;

        /// <summary> gets called on startup only (выполняется только при запуске) </summary>
        public override void Start()
        {
            //==== instantiating ====
            Console.Title = "Straight Racing";
            //background
            plane = new ObjectSingle(Preset.plane, ConsoleColor.DarkGreen);
            road = new ObjectSingle(Preset.plane, ConsoleColor.DarkGray, null, 0.04f, null, null, (int)(plane.Size.X / 2));
            border = new ObjectSingle(Preset.plane, ConsoleColor.DarkYellow, null, 0.02f, null, null, road.Size.X + 4);
            linesSizeY = (int)(road.Size.Y / ZeroCheck(linesQuantY * 2));
            firstLine = new ObjectGroup(null, ConsoleColor.Gray, null, 0.06f, road.Position.X, road.Position.Y - road.Size.Y, Math.Max((int)(linesSizeY / 5), 1), linesSizeY, (int)(road.Size.X / 3), linesSizeY , 2, linesQuantY);
            secondLine = new ObjectGroup(null, firstLine.Color, null, 0.06f, firstLine.Position.X, road.Position.Y, firstLine.Size.X , firstLine.Size.Y , firstLine.Gap.X, firstLine.Gap.Y, firstLine.Quant.X, firstLine.Quant.Y);
            secondLine.Position.MaxY = firstLine.Position.MaxY = road.Position.Y + road.Size.Y;
            //text
            textA = new ObjectSingle(Preset.boxXL, ConsoleColor.White, null, 10f, null, null, null, 7);
            textB = new ObjectSingle(Preset.boxXL, ConsoleColor.Black, null, textA.Layer - 0.01f, textA.Position.X + 1, textA.Position.Y + 1, null, textA.Size.Y);
            textA.ChildList.Add(textB);
            textPoint = new Vector(0, plane.Position.Y + (int)(textA.Size.Y / 1.5f) - (int)(plane.Size.Y / 2));
            //cars
            carPosX = new Random().Next(0,2) > 0 ? road.Position.X - (int)(road.Size.X / 3) : road.Position.X + (int)(road.Size.X / 3);
            car = new ObjectSingle(Preset.boxL, ConsoleColor.Blue, null, 1f, carPosX, road.Position.Y - (int)(road.Size.Y * 0.75f), (int)(road.Size.X / 6), (int)(road.Size.Y / 4));
            windows = new ObjectGroup(null, ConsoleColor.Cyan, null, car.Layer + 0.5f, car.Position.X, car.Position.Y + 1, car.Size.X - 2 , Math.Max((int)(car.Size.Y / 8),1), null, (int)(car.Size.Y / 4) + 1, 1, 2);
            wheels = new ObjectGroup(null, ConsoleColor.Black, null, car.Layer - 0.5f, car.Position.X, car.Position.Y, 1, (int)(car.Size.Y / 4), car.Size.X, (int)(car.Size.Y / 3), 2, 2);
            car.ChildList.Add(windows);
            car.ChildList.Add(wheels);
            heroCar = car.CopyThis();
            heroCar.Position.MinX = road.Position.X + (int)(heroCar.Size.X / 2) - (int)(road.Size.X / 2);
            heroCar.Position.MinY = road.Position.Y + (int)(heroCar.Size.Y / 2) - (int)(road.Size.Y / 2);
            heroCar.Position.MaxX = road.Position.X - (int)(heroCar.Size.X / 2) + (int)(road.Size.X / 2);
            heroCar.Position.MaxY = road.Position.Y - (int)(heroCar.Size.Y / 2) + (int)(road.Size.Y / 2);
            heroCar.AlignWithSide(SideX.middle, SideY.down);
            heroCar.Color = ConsoleColor.Red;
            heroCar.Layer = car.Layer + 1;
            heroCar.ChildList[0].Layer = heroCar.Layer + 0.5f;
            heroStep = (heroCar.Size.X * 2.1f).XToGrid();
            AddEnemy();
            //environment
            house = new ObjectSingle(Preset.boxL, ConsoleColor.Gray, "house", 3f, plane.Position.X - (int)(plane.Size.X / 2), plane.Position.Y , (int)(plane.Size.X / 3), (int)(plane.Size.Y / 2));
            houseB = new ObjectGroup(Preset.boxL, windows.Color, null, house.Layer+0.5f, house.Position.X, house.Position.Y, (int)(house.Size.X / 1.2f), (int)(house.Size.Y / 4), null, (int)(house.Size.Y / 6),1,2);
            houseC = new ObjectSingle(Preset.boxL, ConsoleColor.Black, null, house.Layer+1.0f, house.Position.X, house.Position.Y, (int)(house.Size.X / 2f), (int)(house.Size.Y * 1.2f));
            house.Position.MaxY = road.Position.Y + (int)(houseC.Size.Y / 2) +(int)(plane.Size.Y / 2);
            house.ChildList.Add(houseB);
            house.ChildList.Add(houseC);
            tree = new ObjectSingle(Preset.boxL, ConsoleColor.DarkYellow, "tree", 4f, house.Position.X, house.Position.Y , (int)(plane.Size.X / 2.5f), (int)(plane.Size.Y / 20));
            treeB = new ObjectGroup(Preset.boxL, ConsoleColor.Green, null, tree.Layer+0.5f, tree.Position.X, tree.Position.Y, (int)(tree.Size.X / 4), tree.Size.Y * 5, (int)(tree.Size.X / 8), null ,2,1);
            treeC = new ObjectSingle(Preset.boxL, treeB.Color, null, tree.Layer+1.0f, tree.Position.X, tree.Position.Y, (int)(tree.Size.X / 1.3f), tree.Size.Y * 3);
            tree.Position.MaxY = road.Position.Y + (int)(treeB.Size.Y / 2) + (int)(plane.Size.Y / 2);
            tree.ChildList.Add(treeB);
            tree.ChildList.Add(treeC);
            tree.Position.X = plane.Position.X + plane.Size.X / 2;
            environs.Add(tree);
            environs.Add(house);
            environsPosY = road.Position.Y - (int)(houseC.Size.Y / 2) -(int)(plane.Size.Y / 2);
        }

        /// <summary> gets called every frame (выполняется для каждого кадра) </summary>
        public override void Update()
        {
            //==== game loop ====
            if (!isEnd)
            {
                currentSpeed = windowMult * (float)Math.Sqrt(Math.Pow(startSpeed,2) + 0.02f * MainTimer.ElapsedMilliseconds);
                //background
                firstLine.MoveAside(currentSpeed, SideX.middle, SideY.down);
                secondLine.MoveAside(currentSpeed, SideX.middle, SideY.down);
                if (firstLine.Position.Y >= firstLine.Position.MaxY)
                {
                    firstLine.Position.Y = secondLine.Position.Y - road.Size.Y;
                }
                if (secondLine.Position.Y >= secondLine.Position.MaxY)
                {
                    secondLine.Position.Y = firstLine.Position.Y - road.Size.Y;
                }
                //text
                if (messageOn)
                {
                    messageA = "Press arrow keys or WASD to move.";
                    //message.WriteTo(textA);
                    if (Controller.LastKey != null)
                    {
                        //textA.IsDrawn = false;
                        messageOn = false;
                    }
                }
                else
                {
                    charCounter = (int)Math.Max(Math.Floor(currentSpeed * mphMult/10)-5,0);
                    messageA = String.Format("Current speed: {0} mph {1}", (int)(currentSpeed * mphMult), new string('!', charCounter));
                    textA.MoveTowards(textSpeed * 4, textPoint);
                }
                messageA.WriteTo(textA);
                //cars
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].MoveAside(currentSpeed - startSpeed*2, SideX.middle, SideY.down);
                    int prev = i - 1 < 0 ? enemies.Count - 1 : i - 1;
                    if (enemies[i].Position.Y >= enemies[i].Position.MaxY)
                    {
                        enemies[i].Position.X = new Random().Next((int)heroCar.Position.MinX, (int)heroCar.Position.MaxX);
                        enemies[i].Position.Y = Math.Min(car.Position.Y - new Random().Next(0, (int)currentSpeed.GridToY()), enemies[prev].Position.Y - road.Size.Y / ZeroCheck(enemies.Count));
                        enemies[i].Color = RandomColor(ConsoleColor.Black, ConsoleColor.DarkRed, windows.Color, heroCar.Color, road.Color, firstLine.Color);
                        if (!enemies[i].IsDrawn)
                        {
                            enemies[i].IsDrawn = true;
                        }
                    }
                }
                
                heroCar.MoveByWASD(currentSpeed, heroStep);
                if (heroCar.TriggerEnter(out other, enemyTag))
                {
                    restartCar = other[0];
                    heroCar.ChildList[0].Color = ConsoleColor.DarkRed;
                    restartCar.ChildList[0].Color = ConsoleColor.DarkRed;
                    Console.Beep();
                    isEnd = true;
                }
                //environment
                for (int i = 0; i < environs.Count; i++)
                {
                    environs[i].MoveAside(currentSpeed, SideX.middle, SideY.down);
                    int prev = i - 1 < 0 ? environs.Count - 1 : i - 1;
                    if (environs[i].Position.Y >= environs[i].Position.MaxY)
                    {
                        environs[i].Position.X = new Random().Next(0, 2) > 0 ? plane.Position.X - plane.Size.X / 2 : plane.Position.X + plane.Size.X / 2;
                        environs[i].Position.Y = Math.Min(environsPosY - new Random().Next(0, (int)currentSpeed.GridToY()), environs[prev].Position.Y - houseC.Size.Y * 1.5f);
                        if (environs[i].Tag == house.Tag)
                        {
                            environs[i].Color = RandomColor(ConsoleColor.Black, ConsoleColor.Red ,ConsoleColor.DarkRed , windows.Color, heroCar.Color, road.Color, plane.Color);
                        }
                    }
                }
            }
            else
            {
                //text
                messageA = currentSpeed > 30 ? String.Format(" You drove at a speed of {0} mph!", (int)(currentSpeed * mphMult)): "";
                messageB = String.Format("Game over!{0}", messageA);
                messageC = "Press Enter to restart.";
                messageB.WriteTo(textA,0,-1);
                messageC.WriteTo(textA,0,1);
                textA.MoveTowards(textSpeed, heroCar);
                if (Controller.LastKey == ConsoleKey.Enter)
                {
                    //System.Diagnostics.Process.Start(Assembly.GetExecutingAssembly().Location);
                    //Environment.Exit(0);
                    currentSpeed = startSpeed;
                    heroCar.AlignWithSide(SideX.middle, SideY.down);
                    heroCar.ChildList[0].Color = windows.Color;
                    restartCar.ChildList[0].Color = windows.Color;
                    restartCar.Position.Y = (int)restartCar.Position.MaxY;
                    MainTimer.Restart();
                    //SleepTime = FixedTimeMs;
                    isEnd = false;
                }
            }
        }

        /// <summary> Adds given enemy quantity. 1 by default. (Добавляет данное количество врагов. Одного по умолчанию.)</summary>
        void AddEnemy(int quantity = 1 )
        {
            for (int i = 0; i < quantity; i++)
            {
                enemies.Add(car?.CopyThis());
                enemies[enemies.Count - 1].Tag = enemyTag;
                enemies[enemies.Count - 1].Position.MaxY = heroCar.Position.MaxY + heroCar.Size.Y;
            }
        }
    }
}
