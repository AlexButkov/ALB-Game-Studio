using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace ALB
{
    class Controller: Model
    {
        /// <summary>(очередь событий для контроллера)</summary>
        public Queue<Task> QueueList { get; set; } = new Queue<Task>();
        /// <summary>(объект для поочередного доступа к очереди)</summary>
        public object QueueBlocker { get; set; } = new object();
        protected ObjectSingle parentObject;
        protected ObjectGroup parentGroup;
        /// <summary>(массив переменных для контроллера)</summary>
        protected List<Object>[] valueArray = new List<Object>[(int)Task.max];
        protected bool[] actionToggle = new bool[(int)Draw.max];
        protected Task tempType;
        protected bool toLaunch;
        //=========
        public Controller(ObjectSingle parentObject)
        {
            Initialize(parentObject);
        }
        public Controller(ObjectGroup parentObject)
        {
            parentGroup = parentObject;
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
            this.parentObject = parentObject;
            //SetArrayFull();
            View.StartThread(Monitoring);//Monitoring();
        }

        protected void Monitoring()
        {
            while (true)
            {
                if (toLaunch && parentObject != null)
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
                                case Task.gapX:
                                    actionToggle[(int)Draw.group] = true; goto default;
                                case Task.gapY:
                                    goto case Task.gapX;
                                case Task.quantX:
                                    goto case Task.gapX;
                                case Task.quantY:
                                    goto case Task.gapX;
                                case Task.max:
                                    actionToggle[(int)Draw.some] = true; break;
                                default:
                                    actionToggle[(int)Draw.vector] = true; goto case Task.max;
                            }
                        }
                    }
                    if (actionToggle[(int)Draw.some])
                    {
                        View.DrawObject(valueArray, actionToggle, parentObject);
                        RemoveOldValue();
                    }
                }
            }
        }

        public void SetArrayFull()
        {
            for (int i = 0; i < valueArray.Length; i++)
            {
                SetArray((Task)i);
            }
            toLaunch = true;
        }

        protected void SetArray(Task varType)
        {
            switch (varType)
            {
                case Task.isDestroyed:
                    valueArray[(int)Task.isDestroyed].Add(parentObject.IsDestroyed); break;
                case Task.layer:
                    valueArray[(int)Task.layer].Add(parentObject.Layer); break;
                case Task.positionX:
                    valueArray[(int)Task.positionX].Add(parentObject.Position.GetX); break;
                case Task.positionY:
                    valueArray[(int)Task.positionY].Add(parentObject.Position.GetY); break;
                case Task.sizeX:
                    valueArray[(int)Task.sizeX].Add(parentObject.Size.GetX); break;
                case Task.sizeY:
                    valueArray[(int)Task.sizeY].Add(parentObject.Size.GetY); break;
                case Task.color:
                    valueArray[(int)Task.color].Add(parentObject.Color); break;
                case Task.gapX:
                    valueArray[(int)Task.gapX].Add(parentGroup?.Gap.GetX); break;
                case Task.gapY:
                    valueArray[(int)Task.gapY].Add(parentGroup?.Gap.GetY); break;
                case Task.quantX:
                    valueArray[(int)Task.quantX].Add(parentGroup?.Quant.GetX); break;
                case Task.quantY:
                    valueArray[(int)Task.quantY].Add(parentGroup?.Quant.GetY); break;
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
