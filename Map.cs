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
      
       int level = 0;        //현재 단계 ->모양크기 || 내려오는 속도 조절
       int score = 0;        //사라진 line의 수
      
      // int curx = 0;
      // int cury = 5;
      // bool mainCheck = true;
      //
      // public bool LeftMove = false;
      // public bool RightMove = false;
      // public bool Rotate = false;
      //
      // int[,] map = new int[25, 18];
      // Shape shape=new Shape();
       int[,] nextShape=new int[6,6];
      // int[,] mainShape=new int[4,2];
      // string[] shapeN;
      public void ScoreUp()
        {
            score++;
        }

        

       public void WriteHelp()
        {
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
      //
       public void ShowNextShape(int[,] next)
       {
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
                nextShape[next[i, 0], next[i, 1]-6] = 1;         
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
        // 

        public void SetMainShape(int[,] mainShape, int[,] map)
        {
            for (int i = 0; i < mainShape.GetLength(0); i++)
            {
                map[mainShape[i, 0], mainShape[i, 1]] = 2;
            }
        }


        //
        // public bool CheckDown()
        // {
        //     bool check = true;
        //     for (int i = 0; i < mainShape.GetLength(0); i++) {
        //         int x = mainShape[i, 0];
        //         int y = mainShape[i, 1];
        //
        //         if (map[x + 1, y]==1)
        //         {
        //             check = false;
        //             break;
        //         }
        //     }
        //     return check;
        // }//-----------------------------------------------------------------> 판정기준 필요
        //
        // public bool CheckLeft()
        //  {
        //      bool check = true;
        //      for (int i = 0; i < mainShape.GetLength(0); i++)
        //      {
        //          int x = mainShape[i, 0];
        //          int y = mainShape[i, 1];
        //
        //          if (map[x, y-1] == 1)
        //          {
        //              check = false;
        //              break;
        //          }
        //      }
        //      return check;
        //  }
        //
        // public bool CheckRight()
        //  {
        //      bool check = true;
        //      for (int i = 0; i < mainShape.GetLength(0); i++)
        //      {
        //          int x = mainShape[i, 0];
        //          int y = mainShape[i, 1];
        //
        //          if (map[x, y + 1] == 1)
        //          {
        //              check = false;
        //              break;
        //          }
        //      }
        //      return check;
        //  }
        //
        // public void MainShapeMove()
        // {
        //    
        //     if (CheckDown())//----------------->CheckDown()
        //     {
        //         for (int i = 0; i < mainShape.GetLength(0); i++)
        //         {
        //             int x = mainShape[i, 0];
        //             int y = mainShape[i, 1];
        //
        //             map[x, y] = 0;
        //
        //             mainShape[i, 0]++;
        //         }
        //
        //         for (int i = 0; i < mainShape.GetLength(0); i++)
        //         {
        //             int x = mainShape[i, 0];
        //             int y = mainShape[i, 1];
        //
        //             map[x, y] = 2;
        //         }
        //
        //         DrawBaseMap();
        //     }//아래 움직임이 막히지 않았을 경우 아래로 이동
        //
        //     else
        //     {
        //         for (int i = 0; i < mainShape.GetLength(0); i++)
        //         {
        //             int x = mainShape[i, 0];
        //             int y = mainShape[i, 1];
        //             map[x, y] = 1;
        //         }
        //         SetMainShape();
        //         ShowNextShape();
        //         DrawBaseMap();
        //     }
        //
        //
        //     if (LeftMove)
        //     {
        //         if (CheckLeft())
        //         {
        //             for (int i = 0; i < mainShape.GetLength(0); i++)
        //             {
        //                 int x = mainShape[i, 0];
        //                 int y = mainShape[i, 1];
        //
        //                 map[x, y] = 0;
        //
        //                 mainShape[i, 1]--;
        //             }
        //
        //             for (int i = 0; i < mainShape.GetLength(0); i++)
        //             {
        //                 int x = mainShape[i, 0];
        //                 int y = mainShape[i, 1];
        //
        //                 map[x, y] = 2;
        //             }
        //
        //             DrawBaseMap();
        //             LeftMove = false;
        //         }
        //     }
        //
        //     if (RightMove)
        //     {
        //         if (CheckRight())
        //         {
        //             for (int i = 0; i < mainShape.GetLength(0); i++)
        //             {
        //                 int x = mainShape[i, 0];
        //                 int y = mainShape[i, 1];
        //
        //                 map[x, y] = 0;
        //
        //                 mainShape[i, 1]++;
        //             }
        //
        //             for (int i = 0; i < mainShape.GetLength(0); i++)
        //             {
        //                 int x = mainShape[i, 0];
        //                 int y = mainShape[i, 1];
        //
        //                 map[x, y] = 2;
        //             }
        //
        //             DrawBaseMap();
        //             RightMove = false;
        //         }
        //     }
        //
        //
        //     
        // }
        // 
        // public void RotateShape()
        // {
        //     Rotate = true;
        //     shape.RotateShape(mainShape,map);
        //     DrawBaseMap();
        //     Rotate = false;
        // }
        // 
        // 
        public void DrawBaseMap(int[,] map, ConsoleColor color)
      {
            ConsoleColor originalColor = ConsoleColor.White;
            Console.SetWindowSize(width, height);
          int row=map.GetLength(0);
          int col=map.GetLength(1);
            level = score / 10;
            WriteHelp();
   
          for(int i=0; i<map.GetLength(0) ; i++)
          {
              Console.SetCursorPosition(mapRow, mapCol + i);
              for (int j=0; j<map.GetLength(1) ; j++)
              {
                  if (map[i, j] == 1 )
                  {
                      Console.Write("■");
                  }

                  else if (map[i, j] == 2)
                    {
                        Console.ForegroundColor = color;
                        Console.Write("■");
                        Console.ForegroundColor = originalColor;
                    }
     
                  else
                      Console.Write("  ");
              }
              Console.WriteLine();
          }
      }//DrawBaseMap
    }
}
