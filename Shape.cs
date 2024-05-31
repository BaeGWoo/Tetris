using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class Shape
    {
        string[,] ShapeList = new string[5, 4]
        {
            {"11","12","13","14" },
            {"12","22","23","24" },
            {"12","13","22","23" },
            {"12","13","23","24" },
            {"22","23","24","13" }
        };

        public string[] RotateShape(string[] shape)
        {
            string[] result = shape;
            int pivot = int.Parse(shape[0]);
            int minrow = pivot / 10;
            int mincol = pivot % 10;

            for(int i = 0; i < shape.Length; i++)
            {
                int temppivot = int.Parse(shape[i]);
                int tempminrow = pivot / 10;
                int tempmincol = pivot % 10;

                if (tempmincol <= mincol)
                {
                    if (tempminrow <= minrow)
                    {
                        mincol = tempmincol;
                        minrow= tempminrow;
                    }
                }
            }


            for (int i = 0; i < shape.Length; i++)
            {
                int temppivot = int.Parse(shape[i]);
                int temprow = pivot / 10;
                int tempcol = pivot % 10;

                int margin = tempcol + mincol;

                string temp = "";
                tempcol = margin - tempcol;
                temp = temp + tempcol + temprow;
                shape[i] = temp;
            }



            return result;
        }


        public string[] SetNextShape(int number)
        {
            string[] result=new string[4];
           for(int i = 0; i < 4; i++)
            {
                result[i] = ShapeList[number,i];
            }

            return result;
        }
    }
}
