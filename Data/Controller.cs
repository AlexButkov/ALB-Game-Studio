using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace ALB
{
    class Controller: Model
    {
        /// <summary>(очередь событий для контроллера)</summary>
        public Queue<VarType> QueueList = new Queue<VarType>();
        /// <summary>(массив переменных для контроллера)</summary>
        public List<Object>[] ValueArray = new List<Object>[(int)VarType.def];
        public ObjectSingle ParentObject { get; set; }
        public ObjectGroup ParentGroup { get; set; }
        protected bool[] ActionToggle { get; set; } = new bool[3];
        
        public Controller(Object parentObject)
        {
            ParentObject = (ObjectSingle)parentObject;
            ParentGroup = parentObject.GetType() == typeof(ObjectGroup) ? (ObjectGroup)parentObject : null ;

            for (int i = 0; i < ValueArray.Length; i++)
            {
                ValueArray[i] = new List<Object>();
            }
        }

        public void SetArrayFull()
        {
            for (int i = 0; i < ValueArray.Length; i++)
            {
                SetArray((VarType)i);
            }
        }
        protected void SetArray(VarType varType)
        {
            switch (varType)
            {
                case VarType.layer:
                    ValueArray[(int)VarType.layer].Add(ParentObject.Layer); break;
                case VarType.positionX:
                    ValueArray[(int)VarType.positionX].Add(ParentObject.Position.GetX); break;
                case VarType.positionY:
                    ValueArray[(int)VarType.positionY].Add(ParentObject.Position.GetY); break;
                case VarType.sizeX:
                    ValueArray[(int)VarType.sizeX].Add(ParentObject.Size.GetX); break;
                case VarType.sizeY:
                    ValueArray[(int)VarType.sizeY].Add(ParentObject.Size.GetY); break;
                case VarType.color:
                    ValueArray[(int)VarType.color].Add(ParentObject.Color); break;
                case VarType.gapX:
                    ValueArray[(int)VarType.gapX].Add(ParentGroup?.Gap.GetX); break;
                case VarType.gapY:
                    ValueArray[(int)VarType.gapY].Add(ParentGroup?.Gap.GetY); break;
                case VarType.quantX:
                    ValueArray[(int)VarType.quantX].Add(ParentGroup?.Quant.GetX); break;
                case VarType.quantY:
                    ValueArray[(int)VarType.quantY].Add(ParentGroup?.Quant.GetY); break;
                case VarType.def:
                    goto default;
                default:
                    break;
            }
        }
        protected void RemoveOldValue()
        {
            for (int i = 0; i < ValueArray.Length; i++)
            {
                while (ValueArray[i].Count > 1)
                {
                    ValueArray[i].RemoveAt(0);
                }
            }
        }
    }
}
