using System;
using System.Collections.Generic;
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


        int[,] map = new int[25, 18];
        Shape shape=new Shape();
        int[,] nextShape;
        int[,] mainShape=new int[4,2];
        string[] shapeN;
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


        public void ShowNextShape()
        {
            Console.SetCursorPosition(90, 3);
            Console.WriteLine("[  Next  ]");

            nextShape = new int[5, 5];
            Random rand = new Random();
            shapeN = shape.SetNextShape(rand.Next(0,5));
            //string[] shapeN = shape.SetNextShape(4);
            for (int i=0;i<shapeN.Length;i++)
            {
                int Num = int.Parse(shapeN[i]);
                int x = Num / 10;
                int y=Num % 10;

                nextShape[x, y] = 1;
            }
          
            for (int i = 0; i <= shapeN.Length; i++)
            {
                Console.SetCursorPosition(89, 5 + i);
                for (int j = 0; j <= shapeN.Length; j++)
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
        
        public void SetMainShape()
        {
            string[] temp = shapeN;
            for(int i=0; i<temp.Length; i++)
            {
                int cur = int.Parse(temp[i]);
                int x=cur / 10;
                int y=cur % 10;

                temp[i] = "" + x + (y + 5);
            }


            for (int i = 0; i < temp.Length; i++)
            {
                int cur = int.Parse(temp[i]);
                int x = cur / 10;
                int y = cur % 10;

                map[x, y+1] = 1;
                mainShape[i, 0] = x;
                mainShape[i, 1] = y + 1;
            }
        }
        
        public bool CheckDown()
        {
            bool check = true;
            for(int i=0;i<mainShape.GetLength(0);i++) {
                int x = mainShape[i, 0];
                int y = mainShape[i, 1];

                if (map[x+1,y]!=0)
                {
                    check = false;
                    break;
                }
            }
            return check;
        }//-----------------------------------------------------------------> 판정기준 필요

        public void MainShapeMove()
        {


            if (true)//----------------->CheckDown()
            {
                for (int i = 0; i < mainShape.GetLength(0); i++)
                {
                    int x = mainShape[i, 0];
                    int y = mainShape[i, 1];

                    map[x, y] = 0;

                    mainShape[i, 0]++;
                }

                for (int i = 0; i < mainShape.GetLength(0); i++)
                {
                    int x = mainShape[i, 0];
                    int y = mainShape[i, 1];

                    map[x, y] = 1;
                }

                DrawBaseMap();
            }
        }
        
        
        public void DrawBaseMap()
        {
            Console.SetWindowSize(width, height);
            int row=map.GetLength(0);
            int col=map.GetLength(1);


            WriteHelp();
           //ShowNextShape();----------------------------------->CheckDown()이 false가 될 경우 실행
            //SetMainShape();


            for (int i=0;i<map.GetLength(0); i++)
            {
                map[i, 0] = 1;
                map[i,map.GetLength(1)-1] = 1;
            }

            for(int i=0;i<map.GetLength(1) ; i++)
            {
                map[map.GetLength(0)-1, i] = 1;
            }


            for(int i=0; i<map.GetLength(0) ; i++)
            {
                Console.SetCursorPosition(mapRow, mapCol + i);
                for (int j=0; j<map.GetLength(1) ; j++)
                {
                    if (map[i, j] == 1)
                    {
                        Console.Write("■");
                    }

                    else
                        Console.Write("  ");
                }
                Console.WriteLine();
            }
        }//DrawBaseMap
    }
}
