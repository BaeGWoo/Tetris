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

        public void SetMap(int x,int y)
        {
            map[x, y] = 1;
            DrawBaseMap();
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

        public void ShowNextShape()
        {
            Console.SetCursorPosition(90, 3);
            Console.WriteLine("[ Next ]");
        }
        public void DrawBaseMap()
        {
            Console.SetWindowSize(width, height);
            int row=map.GetLength(0);
            int col=map.GetLength(1);
            WriteHelp();
           ShowNextShape();

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
