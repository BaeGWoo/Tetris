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
        int[,] shapeType = new int[4, 2];
        int[] shapePivot = new int[2];
        public Shape(int[,] shapeType, int[] shapePivot)
        {
            this.shapeType = shapeType;
            this.shapePivot = shapePivot;
        }

        public int[] GetPivot()
        {
            return shapePivot;
        }

        public int[,] GetShapeType()
        {
            return shapeType;
        }

       
    }
}
