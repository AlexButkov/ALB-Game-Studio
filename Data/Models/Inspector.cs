using System;
using System.Threading;
using System.Collections.Generic;

namespace ALB
{
    /// <summary>
    /// translates changed variables info into rendering class <see cref="View"/> 
    /// (передает информацию об измененных переменных в класс для рендеринга <see cref="View"/>)
    /// </summary>
    class Inspector : Model
    {
        /// <summary>(очередь событий для инспектора)</summary>
        public Queue<Param> QueueList = new Queue<Param>();
        View view = new View();
        /// <summary>(объект для поочередного доступа к очереди)</summary>
        public object QueueBlocker = new object();
        public ObjectSingle ParentObject { private get; set; }
        public bool IsParentGroup;
        /// <summary>(массив переменных для инспектора)</summary>
        protected List<dynamic>[] tempArray = new List<dynamic>[(int)Param.max];
        protected bool[] actionToggle = new bool[(int)Draw.max];
        protected Param tempType;
        protected bool toDequeue;
        protected bool isStarted;
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
        public void AddTask(Param taskType)
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
            ParentObject = parentObject;
            StartThread(Monitoring);
        }

        protected void Monitoring()
        {
            while (true)
            {
                if (toDequeue && ParentObject != null)
                {
                    while (QueueList.Count > 0)
                    {
                        lock (QueueBlocker)
                        {
                            tempType = QueueList.Dequeue();
                        }
                        if (tempType == Param.copyObject && ParentObject.CopyObject != null)
                        {
                            for (int i = 0; i < tempArray.Length; i++)
                            {
                                ParentObject.Value((Param)i) = ParentObject.CopyObject.Value((Param)i);
                            }
                            SetArrayFull();
                            actionToggle[(int)Draw.some] = actionToggle[(int)Draw.color] = actionToggle[(int)Draw.vector] = true;
                        }
                        else
                        {
                        SetArray(tempType);
                        }
                        switch (tempType)
                        {
                            case Param.isDrawn:
                                actionToggle[(int)Draw.destroy] = true; goto case Param.max;
                            case Param.layer:
                                goto case Param.color;
                            case Param.color:
                                actionToggle[(int)Draw.color] = true; goto case Param.max;
                            case Param.max:
                                actionToggle[(int)Draw.some] = true; break;
                            default:
                                actionToggle[(int)Draw.vector] = true; goto case Param.max;
                        }
                    }
                    if (actionToggle[(int)Draw.some])
                    {
                        View.DrawObject(tempArray, actionToggle, ParentObject, IsParentGroup);
                        if (!isStarted)
                        {
                            ParentObject.IsDrawn = true;
                            isStarted = true;
                        }
                        RemoveOldValue();
                    }
                }
            }
        }

        public void SetArrayFull()
        {
            for (int i = 0; i < tempArray.Length; i++)
            {
                SetArray((Param)i);
            }
            toDequeue = true;
        }

        protected void SetArray(Param varType)
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
