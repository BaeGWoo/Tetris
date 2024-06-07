using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class Map
    {
      #region 화면 설정
      int height =40;        //화면높이
      int width=120;         //화면 너비
      #endregion
     
      #region 테트리스 맵 위치설정
      int mapRow = 35;      //map초기화 위치 너비(x)
      int mapCol = 5;       //map 초기화 위치 높이(y)
      #endregion
            
       int score = 0;        //사라진 line의 수
      
       int[,] nextShape=new int[6,6];
     
      public void ScoreUp()
        {
            score++;
        }
        public int GetScore()
        {
            return score;
        }

       

        public int getWindowHeight()
        {
            return height;
        }

        public int getWindowWidth()
        {
            return width;
        }

        public void WriteHelp()
        {
            Console.ForegroundColor = ConsoleColor.White;
            int padding = 3;          
            Console.SetCursorPosition(padding, 6);
            Console.WriteLine("Clear Line Number : " + score);
      
      
            int margin = 12;
            Console.SetCursorPosition(padding, margin);
            Console.WriteLine("↑ : 모양 회전");
            Console.SetCursorPosition(padding, margin + 2);
            Console.WriteLine("← : 왼쪽 이동");
            Console.SetCursorPosition(padding, margin + 4);
            Console.WriteLine("→ : 오른쪽 이동");
            Console.SetCursorPosition(padding, margin + 6);
            Console.WriteLine("↓ : 빠른 낙하");
        }// 도움말

        public void SetMainShape(int[,] mainShape, int[,] map)
        {
            for (int i = 0; i < mainShape.GetLength(0); i++)
            {
                map[mainShape[i, 0], mainShape[i, 1]] = 2;
            }
        }

        public void ShowNextShape(int[,] next)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(90, 3);
            Console.WriteLine("[  Next  ]");
            for (int i = 0; i < nextShape.GetLength(0); i++)
            {
                for (int j = 0; j < nextShape.GetLength(1); j++)
                {
                    nextShape[i, j] = 0;
                }
            }


            for (int i = 0; i < next.GetLength(0); i++)
            {
                nextShape[next[i, 0] - 2, next[i, 1] - 6] = 1;
            }

            for (int i = 0; i < nextShape.GetLength(0); i++)
            {
                Console.SetCursorPosition(89, 5 + i);
                for (int j = 0; j < nextShape.GetLength(1); j++)
                {
                    if (nextShape[i, j] == 1)
                    {

                        Console.Write("■");
                    }
                    else
                    {
                        Console.Write("  ");
                    }

                }

            }


        }
  

      





    }
}
