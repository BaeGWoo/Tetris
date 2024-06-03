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

        public void RotateShape(int[,] shape, int[,] map)
        {
            
            int Bmaxrow = shape[0, 0];
            int Bmincol = shape[0,1];

            for (int i = 0; i < shape.GetLength(0); i++)
            {

                map[shape[i, 0], shape[i, 1]] = 0;
            }


            for (int i = 0; i < shape.GetLength(0); i++)
            {

                if (shape[i,1]<= Bmincol)
                    Bmincol = shape[i,1];

                if (shape[i,0]>= Bmaxrow)
                    Bmaxrow = shape[i,0];

             
            }


           

            for (int i = 0; i < shape.GetLength(0); i++)
            {

                int x = shape[i, 0];
                int y = shape[i, 1];
                int temp;
                temp = x;
                shape[i, 0] = -1 * shape[i, 1];
                shape[i, 1] = temp;
             
            }


            int Amaxrow = shape[0, 0];
            int Amincol = shape[0, 1];

            for (int i = 0; i < shape.GetLength(0); i++)
            {

                if (shape[i, 1] <= Amincol)
                    Amincol = shape[i, 1];

                if (shape[i, 0] >= Amaxrow)
                    Amaxrow = shape[i, 0];
            }

            int rowmargin=Math.Abs(Amaxrow-Bmaxrow);
            int colmargin=Math.Abs(Amincol-Bmincol);

            for (int i = 0; i < shape.GetLength(0); i++)
            {
                shape[i, 0] += rowmargin;
                shape[i, 1] += colmargin;
            }


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
