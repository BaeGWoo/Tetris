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
      
       int level = 1;        //현재 단계 ->모양크기 || 내려오는 속도 조절
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

        public int GetLevel()
        {
            return level;
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
            Console.SetCursorPosition(padding, 4);
            Console.WriteLine("Level " + level);
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


      //  public void ShowNextShape(int[,] nextShape, MyBuffer buffer)
      //  {
      //      for (int i = 0; i < nextShape.GetLength(0); i++)
      //      {
      //          buffer.Draw(nextShape[i, 1] + 10, nextShape[i, 0] + 3, "□");
      //      }
      //  }

        public void DrawBaseMap(int[,] map, ConsoleColor color, MyBuffer buffer)
        {
            //ConsoleColor originalColor = ConsoleColor.White;
            //Console.ForegroundColor = color;
            level = score >= 10 ? score / 10 : 1;
           

            for (int i = 0; i < map.GetLength(0); i++)
            {
                //Console.SetCursorPosition(mapRow, mapCol + i);
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 1)
                    {                   
                        buffer.Draw(j, i, "■");                 
                    }
                    else if (map[i, j] == 2)
                    {
                        buffer.Draw(j, i, "■");
                    }
                    else if (map[i, j] == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        buffer.Draw(j, i, "■");
                        Console.ForegroundColor = color;
                    }

                    else
                    {
                        buffer.Draw(j, i, "  ");
                    }
                }
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
               nextShape[next[i, 0]-2, next[i, 1]-6] = 1;         
           }
    
           for (int i = 0; i < nextShape.GetLength(0); i++)
           {
               Console.SetCursorPosition(89, 5 + i);
               for (int j = 0; j < nextShape.GetLength(1); j++)
               {
                   if (nextShape[i,j] == 1)
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
  

      


     
      public void DrawBaseMap(int[,] map, ConsoleColor color)
    {
          ConsoleColor originalColor = ConsoleColor.White;
          Console.SetWindowSize(width, height);
        int row=map.GetLength(0);
        int col=map.GetLength(1);
          level = score>=10?score / 10:1;
          WriteHelp();
    
          for (int i = 0; i < map.GetLength(0); i++)
          {
              Console.SetCursorPosition(mapRow, mapCol + i);
              if (i == 2)
              {
                  for (int j = 0; j < map.GetLength(1); j++)
                  {
                      Console.ForegroundColor = ConsoleColor.Red;
                      Console.Write("■");
                  }
                  Console.ForegroundColor = originalColor;
              }
    
              else
              {
                  for (int j = 0; j < map.GetLength(1); j++)
                  {
    
                      if (map[i, j] == 1)
                      {
                          Console.ForegroundColor = ConsoleColor.Magenta;
                          Console.Write("■");
                          Console.ForegroundColor = originalColor;
    
                          
                      }
    
                      else if (map[i, j] == 2)
                      {
                           Console.ForegroundColor = color;
                           Console.Write("■");
                           Console.ForegroundColor = originalColor;
                         
                      }
    
                      else if (map[i, j] == 3)
                      {
                          Console.ForegroundColor = originalColor;
                          Console.Write("■");
    
                         
                      }
    
                      else
                          Console.Write("  ");
                  }
              }
            Console.WriteLine();
        }
    }//DrawBaseMap
    



      


    }
}
