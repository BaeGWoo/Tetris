using System;
using System.Security.Cryptography.X509Certificates;
using System.Timers;
using System.Drawing;
using System.Diagnostics;

namespace Tetris
{
    

    internal class Program
    {
       
       static MyBuffer gameBoard=new MyBuffer(25,18);

        static Map tetris = new Map();
        static int[,] nextShape = new int[4, 2];
        static int[,] mainShape = new int[4, 2];
        static int[] mainPivot = new int[2];
        static int nextnumber;
        static int[] nextPivot = new int[2];
        static int[,] map = new int[25, 18];
        static Random random = new Random();
        static List<Shape> shapeList = new List<Shape>();
        static bool gameOver = false;
        static ConsoleColor[] colors = {
            ConsoleColor.Green,
            ConsoleColor.Blue,
            ConsoleColor.Yellow,
            ConsoleColor.Cyan,
            ConsoleColor.DarkYellow
        };

        private static void RenderGame()
        {
            gameBoard.Clear();
            tetris.DrawBaseMap(map, colors[nextnumber], gameBoard);
            tetris.ShowNextShape(nextShape, gameBoard);
            gameBoard.Render();
        }


        private static void CountDown(object sender, ElapsedEventArgs e)
        {
            if (!gameOver)
            {
                //Console.Clear();
                MoveDown();
                Crush();
                //tetris.DrawBaseMap(map, colors[nextnumber]);
                //tetris.ShowNextShape(nextShape);
                RenderGame();
            }

            else
            {
                Console.Clear();
                string message = "GAME OVER";

                // Calculate the position to center the message
                int messageX = (tetris.getWindowWidth() - message.Length) / 2;
                int messageY = tetris.getWindowHeight() / 2;


                Console.SetCursorPosition(messageX, messageY);
                Console.WriteLine("GAME OVER");
                for(int i = 0; i < 5; i++)
                {
                    Console.WriteLine("\n");
                }
                //tetris.DisplayGameOverMessage();
                Environment.Exit(0);
            }
        }

        public static void SetMap(int[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                map[i, 0] = 3;
                map[i, map.GetLength(1) - 1] = 3;
            }

            for (int i = 0; i < map.GetLength(1); i++)
            {
                map[map.GetLength(0) - 1, i] = 3;
            }
        }

        public static bool CheckMove(int[,] temp, int[,] map)
        {
            for(int i = 0; i < temp.GetLength(0); i++)
            {
                if (temp[i, 0] < 0 || temp[i, 0] >= map.GetLength(0) - 1 || temp[i, 1] <= 0 || temp[i, 1] >= map.GetLength(1) - 1)
                {                   
                    return false;
                }

                if (map[temp[i,0], temp[i, 1]] == 1)
                {                 
                    return false;
                }
            }         
            return true;
        }

        public static void MoveLeft()
        {
            int[,] temp =new int[4,2];
            for (int i = 0; i < mainShape.GetLength(0); i++)
            {
                temp[i,0]= mainShape[i,0];
                temp[i,1] = mainShape[i,1]-1;
            }

            if (CheckMove(temp, map))
            {

                for (int i = 0; i < mainShape.GetLength(0); i++)
                {
                    map[mainShape[i, 0], mainShape[i, 1]] = 0;
                }

                for (int i = 0; i < mainShape.GetLength(0); i++)
                {
                    map[mainShape[i, 0], mainShape[i, 1] - 1] = 2;
                    mainShape[i, 1]--;
                }
                mainPivot[1]--;
            }

        }

        public static void MoveDown()
        {
            int[,] temp = new int[4, 2];
            for (int i = 0; i < mainShape.GetLength(0); i++)
            {
                temp[i, 0] = mainShape[i, 0]+1;
                temp[i, 1] = mainShape[i, 1];
            }

            if (CheckMove(temp, map))
            {

                for (int i = 0; i < mainShape.GetLength(0); i++)
                {
                    map[mainShape[i, 0], mainShape[i, 1]] = 0;
                }

                for (int i = 0; i < mainShape.GetLength(0); i++)
                {
                    map[mainShape[i, 0] + 1, mainShape[i, 1]] = 2;
                    mainShape[i, 0]++;
                }
                mainPivot[0]++;
            }

            else
            {
                
                for(int i=0;i<mainShape.GetLength(0); i++)
                {
                    map[mainShape[i, 0], mainShape[i, 1]] = 1;
                    if (mainShape[i, 0] <= 4)
                        gameOver = true;
                }

               

                for (int i = 0; i < mainShape.GetLength(0); i++)
                {
                    mainShape[i, 0] = nextShape[i, 0];
                    mainShape[i, 1] = nextShape[i, 1];
                }
                mainPivot[0] = nextPivot[0];
                mainPivot[1]= nextPivot[1];

                tetris.SetMainShape(mainShape, map);

                int[,] tempNext = new int[4, 2];
                int[] tempPivot = new int[2];
               
                int tempnumber= random.Next(0, 10);

                nextnumber = (tempnumber + nextnumber) % shapeList.Count;
               

                tempNext = shapeList[nextnumber].GetShapeType();
                tempPivot = shapeList[nextnumber].GetPivot();

                for(int i=0;i<nextShape.GetLength(0); i++)
                {
                    nextShape[i, 0] = tempNext[i, 0];
                    nextShape[i, 1] = tempNext[i, 1];
                }
                nextPivot[0] = tempPivot[0];
                nextPivot[1] = tempPivot[1];
            }         
        }

        public static void Crush()
        {
            for(int i = 0; i < map.GetLength(0)-1; i++)
            {
                int count = 0;
                for(int j=0; j<map.GetLength(1); j++)
                {
                    if (map[i, j] == 1)
                        count++;
                }
                if (count >= map.GetLength(1)-2)
                {
                    for(int j=1 ; j<map.GetLength(1)-1; j++)
                    {
                        map[i, j] = 0;
                    }
                    MapDown(i-1);
                }

            }

           
        }

        public static void MapDown(int num)
        {
            for(int i = num; i >= 0; i--)
            {
                for(int j=1;j<map.GetLength(1)-1; j++)
                {
                    if (map[i,j]==1)
                    {
                        map[i, j] = 0;
                        map[i + 1, j] = 1;
                    }
                }
            }
            tetris.ScoreUp();
        }

        public static void MoveRight()
        {
            int[,] temp = new int[4, 2];
            for (int i = 0; i < mainShape.GetLength(0); i++)
            {
                temp[i, 0] = mainShape[i, 0];
                temp[i, 1] = mainShape[i, 1] + 1;
            }

            if (CheckMove(temp, map))
            {

                for (int i = 0; i < mainShape.GetLength(0); i++)
                {
                    map[mainShape[i, 0], mainShape[i, 1]] = 0;
                }

                for (int i = 0; i < mainShape.GetLength(0); i++)
                {
                    map[mainShape[i, 0], mainShape[i, 1] + 1] = 2;
                    mainShape[i, 1]++;
                }
                mainPivot[1]++;
            }
        }

        public static void Rotate()
        {
            int[,] Rtemp = new int[4, 2];
            for (int i = 0; i < mainShape.GetLength(0); i++)
            {
                Rtemp[i, 0] = mainShape[i, 0]-mainPivot[0];
                Rtemp[i, 1] = mainShape[i, 1]-mainPivot[1];

                int temp = Rtemp[i, 1];
                Rtemp[i, 1] = -1 * Rtemp[i, 0];
                Rtemp[i, 0] = temp;

                Rtemp[i, 0] += mainPivot[0];
                Rtemp[i, 1] += mainPivot[1];
            }
           

            if (CheckMove(Rtemp, map))
            {
                for (int i = 0; i < mainShape.GetLength(0); i++)
                {
                    map[mainShape[i, 0], mainShape[i, 1]] = 0;
                }

                for (int i = 0; i < mainShape.GetLength(0); i++)
                {
                    mainShape[i, 0] -= mainPivot[0];
                    mainShape[i, 1] -= mainPivot[1];

                    int temp = mainShape[i, 1];
                    mainShape[i, 1] = -1 * mainShape[i, 0];
                    mainShape[i, 0] = temp;

                    mainShape[i, 0] += mainPivot[0];
                    mainShape[i, 1] += mainPivot[1];
                   
                }
            }
            
        } 


        static void Main(string[] args)
        {
            int[,] Type1 = new int[4, 2] { { 2, 9 }, { 3, 8 }, { 3, 9 }, { 3, 10 } };
            int[] Typ1Pivot = new int[2] { 3, 9 };

            int[,] Type2 = new int[4, 2] { { 2, 8 }, { 3, 8 }, { 3, 9 }, { 3, 10 } };
            int[] Typ2Pivot = new int[2] {3, 9 };

            int[,] Type3 = new int[4, 2] { { 2, 8 }, { 2, 9 }, { 3, 9 }, { 3, 10 } };
            int[] Typ3Pivot = new int[2] { 3, 9 };

            int[,] Type4 = new int[4, 2] { { 2, 7 }, { 2, 8 }, { 2, 9 }, { 2, 10 } };
            int[] Typ4Pivot = new int[2] { 2, 9 };

            int[,] Type5 = new int[4, 2] { { 2, 8 }, { 2, 9 }, { 3, 8 }, { 3, 9 } };
            int[] Typ5Pivot = new int[2] { -1, -1 };

            shapeList.Add(new Shape(Type1, Typ1Pivot));
            shapeList.Add(new Shape(Type2, Typ2Pivot));
            shapeList.Add(new Shape(Type3, Typ3Pivot));
            shapeList.Add(new Shape(Type4, Typ4Pivot));
            shapeList.Add(new Shape(Type5, Typ5Pivot));

            SetMap(map); //맵 기본 모양 초기화
            int number = random.Next(0, shapeList.Count);
            mainShape = shapeList[number].GetShapeType(); //mainShape 초기화
            mainPivot = shapeList[number].GetPivot();

            nextnumber = (number+1)% shapeList.Count;
            nextShape = shapeList[nextnumber].GetShapeType(); //nextShape 초기화
            nextPivot= shapeList[nextnumber].GetPivot();

            //초기화 해 놓은 정보들 출력
            //tetris.ShowNextShape(nextShape,gameBoard);
            //tetris.SetMainShape(mainShape, map);
            //tetris.DrawBaseMap(map, colors[nextnumber],gameBoard);
            
            //아래로 떨어지고 움직이는 정보 interval 실행
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 500;
            timer.Elapsed += new ElapsedEventHandler(CountDown);
            timer.Start();
            

            //방향키 입력받기
            while (true)
            {
               
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        Rotate();
                        break;

                    case ConsoleKey.LeftArrow:
                        MoveLeft();
                        break;

                    case ConsoleKey.RightArrow:
                        MoveRight();
                        break;
                    case ConsoleKey.DownArrow:
                        MoveDown();
                        break;
                }
                RenderGame();
            }

        }
    }
}
