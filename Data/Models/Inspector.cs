using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace ALB
{
    class Inspector : Model
    {
        /// <summary>(очередь событий для инспектора)</summary>
        public Queue<Task> QueueList { get; set; } = new Queue<Task>();
        /// <summary>(объект для поочередного доступа к очереди)</summary>
        public object QueueBlocker { get; set; } = new object();
        protected ObjectSingle ParentObject { get; set; }
        public ObjectGroup ParentGroup { get; set; }
        /// <summary>(массив переменных для инспектора)</summary>
        protected List<Object>[] valueArray = new List<Object>[(int)Task.max];
        protected bool[] actionToggle = new bool[(int)Draw.max];
        protected Task tempType;
        protected bool toDequeue;
        //=========
        public Inspector(ObjectSingle parentObject)
        {
            Initialize(parentObject);
        }
        public Inspector(ObjectGroup parentObject)
        {
            ParentGroup = parentObject;
            Initialize(parentObject);
        }
        //=========
        public void AddTask(Task taskType)
        {
            lock (QueueBlocker)
            {
                if (!QueueList.Contains(taskType))
                {
                    QueueList.Enqueue(taskType);
                }
            }
        }
        //=========
        protected void Initialize(ObjectSingle parentObject)
        {
            for (int i = 0; i < valueArray.Length; i++)
            {
                valueArray[i] = new List<Object>();
            }
            this.ParentObject = parentObject;
            //SetArrayFull();
            View.StartThread(Monitoring);//Monitoring();
        }

        protected void Monitoring()
        {
            while (true)
            {
                if (toDequeue && ParentObject != null)
                {
                    lock (QueueBlocker)
                    {
                        while (QueueList.Count > 0)
                        {
                            tempType = QueueList.Dequeue();
                            SetArray(tempType);
                            switch (tempType)
                            {
                                case Task.isDestroyed:
                                    actionToggle[(int)Draw.destroy] = true; goto case Task.max;
                                case Task.layer:
                                    actionToggle[(int)Draw.layer] = true; goto case Task.max;
                                case Task.color:
                                    actionToggle[(int)Draw.color] = true; goto case Task.max;
                                case Task.max:
                                    actionToggle[(int)Draw.some] = true; break;
                                case Task.positionX:
                                    foreach (ObjectSingle Child in ParentObject.ChildList)
                                    {
                                        Child.Position.X += (int)valueArray[(int)Task.positionX][valueArray[(int)Task.positionX].Count - 1] - (int)valueArray[(int)Task.positionX][0];
                                    }
                                    goto default;
                               case Task.positionY:
                                    foreach (ObjectSingle Child in ParentObject.ChildList)
                                    {
                                        Child.Position.Y += (int)valueArray[(int)Task.positionY][valueArray[(int)Task.positionY].Count - 1] - (int)valueArray[(int)Task.positionY][0];
                                    }
                                    goto default;
                                default:
                                    actionToggle[(int)Draw.vector] = true; goto case Task.max;
                            }
                        }
                    }
                    if (actionToggle[(int)Draw.some])
                    {
                        View.DrawObject(valueArray, actionToggle, ParentObject, ParentGroup);
                        RemoveOldValue();
                    }
                }
                Thread.Sleep(DeltaTimeMs);
            }
        }

        public void SetArrayFull()
        {
            for (int i = 0; i < valueArray.Length; i++)
            {
                SetArray((Task)i);
            }
            toDequeue = true;
        }

        protected void SetArray(Task varType)
        {
            switch (varType)
            {
                case Task.isDestroyed:
                    valueArray[(int)Task.isDestroyed].Add(ParentObject.IsDestroyed); break;
                case Task.layer:
                    valueArray[(int)Task.layer].Add(ParentObject.Layer); break;
                case Task.positionX:
                    valueArray[(int)Task.positionX].Add((int)ParentObject.Position.X); break;
                case Task.positionY:
                    valueArray[(int)Task.positionY].Add((int)ParentObject.Position.Y); break;
                case Task.sizeX:
                    valueArray[(int)Task.sizeX].Add((int)ParentObject.Size.X); break;
                case Task.sizeY:
                    valueArray[(int)Task.sizeY].Add((int)ParentObject.Size.Y); break;
                case Task.color:
                    valueArray[(int)Task.color].Add(ParentObject.Color); break;
                case Task.gapX:
                    valueArray[(int)Task.gapX].Add((int)(ParentGroup?.Gap.X ?? 0)); break;
                case Task.gapY:
                    valueArray[(int)Task.gapY].Add((int)(ParentGroup?.Gap.Y ?? 0)); break;
                case Task.quantX:
                    valueArray[(int)Task.quantX].Add((int)(ParentGroup?.Quant.X ?? 1)); break;
                case Task.quantY:
                    valueArray[(int)Task.quantY].Add((int)(ParentGroup?.Quant.Y ?? 1)); break;
                case Task.max:
                    goto default;
                default:
                    break;
            }
        }
        protected void RemoveOldValue()
        {
            for (int i = 0; i < valueArray.Length; i++)
            {
                while (valueArray[i].Count > 1)
                {
                    valueArray[i].RemoveAt(0);
                }
            }
        }
    }
}
