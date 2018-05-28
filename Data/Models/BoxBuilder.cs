using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALB
{
    class BoxBuilder : Model
    {
        public List<int[]> BoxList { get; } = new List<int[]>();
        //---
        int[] firstBox = new int[5];
        int[] secondBox = new int[5];
        int[] currentBox;
        ConsoleColor prevColor;
        bool? isFirst = null;
        bool isRow;
        bool isCol;
        bool isInside;
        bool isSame;
        //------
        public List<int[]> LastPixel()
        {
            BoxList.Clear();
            if (isFirst != null)
            {
                if (firstBox[3] == secondBox[3] || isFirst == true)
                {
                    MergeBoxes();
                    BoxToList();
                }
                else
                {
                    BoxToList(true);
                }
                isFirst = null;
            }
            return BoxList.Count > 0 ? BoxList : null;
        }

        public List<int[]> AddPixel(ConsoleColor color, int x, int y)
        {
            BoxList.Clear();
            currentBox = isFirst != false ? firstBox : secondBox;
            prevColor = isFirst == null ? color : (ConsoleColor)firstBox[0];
            isRow = isFirst == null ? true : x == currentBox[1] + currentBox[3] && y == currentBox[2] + currentBox[4] - 1;
            isCol = isFirst == null ? true : y == currentBox[2] + currentBox[4] && x == currentBox[1];
            isInside = isFirst != false ? true : x >= firstBox[1] && x < (firstBox[1] + firstBox[3]);
            isSame = firstBox[3] == secondBox[3];

            if (isFirst == null)
            {
                isFirst = true;
            }
            if (isInside && prevColor == color)
            {
                if (isRow)
                {
                    PixelToBox(currentBox);
                }
                else if (isCol)
                {
                    if (isFirst == true)
                    {
                        PixelToBox(secondBox);
                        isFirst = false;
                    }
                    else
                    {
                        if (!isSame)
                        {
                            BoxToList();
                        }
                        MergeBoxes();
                        PixelToBox(secondBox);
                    }
                }
                else
                {
                    End();
                }
            }
            else
            {
                //prevColor = color;
                End();
            }
            return BoxList.Count > 0 ? BoxList : null;
            //----
            void End()
            {
                if (isSame || isFirst == true)
                {
                    MergeBoxes();
                    BoxToList();
                }
                else
                {
                    BoxToList(true);
                }
                PixelToBox(firstBox);
                isFirst = true;
            }
            //----
            void PixelToBox(int[] box)
            {
                if (box[3] == 0)
                {
                    box[0] = (int)color;
                    box[1] = x;
                    box[2] = y;
                    box[4] = 1;
                }
                box[3]++;
            }
            //----
        }
        //----
        void MergeBoxes()
        {
            if (firstBox[3] == 0)
            {
                secondBox.CopyTo(firstBox, 0);
            }
            else
            {
                firstBox[4] += secondBox[4];
            }
            ClearBox(secondBox);
        }
        //----
        void BoxToList(bool addBoth = false)
        {
            BoxList.Add((int[])firstBox.Clone());
            ClearBox(firstBox);

            if (addBoth)
            {
                BoxList.Add((int[])secondBox.Clone());
                ClearBox(secondBox);
            }
        }
        //----
        void ClearBox(int[] box)
        {
            for (int i = 0; i < box.Length; i++)
            {
                box[i] = 0;
            }
        }
        //----
    }
}
