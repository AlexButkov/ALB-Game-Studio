using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace ALB
{
    class Inspector : Model
    {
        /// <summary>(очередь событий для инспектора)</summary>
        public Queue<Task> QueueList = new Queue<Task>();
        View view = new View();
        /// <summary>(объект для поочередного доступа к очереди)</summary>
        public object QueueBlocker = new object();
        public ObjectSingle ParentObject { private get; set; }
        public bool IsParentGroup;
        /// <summary>(массив переменных для инспектора)</summary>
        protected List<dynamic>[] tempArray = new List<dynamic>[(int)Task.max];
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
            IsParentGroup = true;
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
            for (int i = 0; i < tempArray.Length; i++)
            {
                tempArray[i] = new List<Object>();
            }
            this.ParentObject = parentObject;
            //SetArrayFull();
            StartThread(Monitoring);
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
                            if (tempType == Task.copyObject && ParentObject.CopyObject != null)
                            {
                                for (int i = 0; i < tempArray.Length; i++)
                                {
                                    ParentObject.Value((Task)i) = ParentObject.CopyObject.Value((Task)i);
                                }
                                SetArrayFull();
                                actionToggle[(int)Draw.some] = actionToggle[(int)Draw.layer] = actionToggle[(int)Draw.color] = actionToggle[(int)Draw.vector] = true;
                            }
                            else
                            {
                            SetArray(tempType);
                            }
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
                                        Child.Position.X += (int)tempArray[(int)Task.positionX][tempArray[(int)Task.positionX].Count - 1] - (int)tempArray[(int)Task.positionX][0];
                                    }
                                    goto default;
                               case Task.positionY:
                                    foreach (ObjectSingle Child in ParentObject.ChildList)
                                    {
                                        Child.Position.Y += (int)tempArray[(int)Task.positionY][tempArray[(int)Task.positionY].Count - 1] - (int)tempArray[(int)Task.positionY][0];
                                    }
                                    goto default;
                                default:
                                    actionToggle[(int)Draw.vector] = true; goto case Task.max;
                            }
                        }
                    }
                    if (actionToggle[(int)Draw.some])
                    {
                        view.DrawObject(tempArray, actionToggle, ParentObject, IsParentGroup);
                        RemoveOldValue();
                    }
                }
                Thread.Sleep(DeltaTimeMs);
            }
        }

        public void SetArrayFull()
        {
            for (int i = 0; i < tempArray.Length; i++)
            {
                SetArray((Task)i);
            }
            toDequeue = true;
        }

        protected void SetArray(Task varType)
        {
            tempArray[(int)varType].Add(ParentObject.Value(varType));
        }
        protected void RemoveOldValue()
        {
            for (int i = 0; i < tempArray.Length; i++)
            {
                while (tempArray[i].Count > 1)
                {
                    tempArray[i].RemoveAt(0);
                }
            }
        }
    }
}
