using System;
using System.Security.Cryptography.X509Certificates;
using System.Timers;
using System.Drawing;
using System.Diagnostics;
using static System.Formats.Asn1.AsnWriter;

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
        static int colorNumber;
        static bool keyCheck = true;
        static bool gameStart = false;
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

        public static void GameOverImage()
        {
            Console.Clear();
            string[] gameOver = new string[]
            {
            "  ######    ###    ##     ## ########     #######   ##   ##  ######## ######## ",
            " ##    ##  ## ##   ###   ### ##          ##     ##  ##   ##  ##       ##     ##",
            " ##       ##   ##  #### #### ##          ##     ##  ##   ##  ##       ##     ##",
            " ##   ### ##     ## ## ### ## ######      ##     ##  ##  ##  ######   ######## ",
            " ##    ## ######### ##     ## ##          ##     ##  ##  ##  ##       ##   ## ",
            " ##    ## ##     ## ##     ## ##          ##     ##  ## ##   ##       ##    ##",
            "  ###  ##  ##     ## ##     ## ########     #######   ##     ## ####  ##      ##"
            };

            int startX = (Console.WindowWidth - gameOver[0].Length) / 2;
            int startY = Console.WindowHeight / 2 - 4;

            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < gameOver.Length; i++)
            {
                Console.SetCursorPosition(startX, startY + i);
                Console.Write(gameOver[i]);
            }

            string scoreText = $"Score: {tetris.GetScore()}";
            int scoreX = (Console.WindowWidth - scoreText.Length) / 2;
            int scoreY = startY + gameOver.Length + 2;

            Console.SetCursorPosition(scoreX, scoreY);
            Console.Write(scoreText);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\n\n\n\n\n\n");
        }
        public static void GameStartImage()
        {
            Console.Clear();
            string[] gaemStart = new string[]
            {
            "##########   #######  ###########  ##########   #########   #######    ##########",
            "    ##       ##           ##       ##           ##     ##     ##       ##      ",
            "    ##       ##           ##       ##           ##     ##     ##       ##       ",
            "    ##       #######      ##       ########     #########     ##       ########## ",
            "    ##       ##           ##       ##           ##  ##        ##               ## ",
            "    ##       ##           ##       ##           ##    ##      ##       ##      ## ",
            "    ##       #######      ##       ###########  ##     ##   #######    ##########  "
            };

            int startX = (Console.WindowWidth - gaemStart[0].Length) / 2;
            int startY = Console.WindowHeight / 2 - 4;

            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < gaemStart.Length; i++)
            {
                Console.SetCursorPosition(startX, startY + i);
                Console.Write(gaemStart[i]);
            }

            string scoreText = $"Score: {tetris.GetScore()}";
            int scoreX = (Console.WindowWidth - scoreText.Length) / 2;
            int scoreY = startY + gaemStart.Length + 2;

            string instructionText = "Press any key to start";
            int instructionX = (Console.WindowWidth - instructionText.Length) / 2;
            int instructionY = startY + gaemStart.Length + 2;

            Console.SetCursorPosition(instructionX, instructionY);
            Console.Write(instructionText);

            Console.ForegroundColor = ConsoleColor.White;
        }




        private static void RenderGame()
        {
            gameBoard.Clear();
            //tetris.DrawBaseMap(map, colors[nextnumber], gameBoard);
            //tetris.ShowNextShape(nextShape, gameBoard);
            gameBoard.Render(colors[colorNumber%colors.Length],map,keyCheck);
            //gameBoard.CheckMap(map);
            Console.CursorVisible = false;
        }


        private static void CountDown(object sender, ElapsedEventArgs e)
        {
            if (!gameOver)
            {
                //Console.Clear();
                Move(1,0, keyCheck);
                keyCheck = true;
                Crush();
                //tetris.DrawBaseMap(map, colors[nextnumber]);
                //tetris.ShowNextShape(nextShape);
                RenderGame();
               
            }

            else
            {
                GameOverImage();
               //Console.Clear();
               //string message = "GAME OVER";
               //
               //// Calculate the position to center the message
               //int messageX = (tetris.getWindowWidth() - message.Length) / 2;
               //int messageY = tetris.getWindowHeight() / 2;
               //
               //
               //Console.SetCursorPosition(messageX, messageY);
               //Console.WriteLine("GAME OVER\n");
               //Console.SetCursorPosition(messageX-3, messageY+5);
               //Console.WriteLine("당신의 점수는 : "+tetris.GetScore());
               //for(int i = 0; i < 5; i++)
               //{
               //    Console.WriteLine("\n");
               //}
               ////tetris.DisplayGameOverMessage();
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
                map[2, i] = 4;
            }

            
        }

        public static void Move(int x, int y,bool keyCheck)
        {
            if (!keyCheck)
                return;
            int[,] temp = new int[4, 2];
            for (int i = 0; i < mainShape.GetLength(0); i++)
            {
                temp[i, 0] = mainShape[i, 0] + x;
                temp[i, 1] = mainShape[i, 1] + y;
            }

            if (CheckMove(temp, map))
            {
                gameBoard.Render(colors[colorNumber % colors.Length], map,keyCheck);
                for (int i = 0; i < mainShape.GetLength(0); i++)
                {
                    if (map[mainShape[i, 0], mainShape[i, 1]] == 4)
                        continue;
                    map[mainShape[i, 0], mainShape[i, 1]] = 0;
                }

                for (int i = 0; i < mainShape.GetLength(0); i++)
                {
                   
                    map[mainShape[i, 0] + x, (mainShape[i, 1] + y)] = 2;
                    mainShape[i, 0] += x;
                    mainShape[i, 1] += y;
                }
                mainPivot[0] += x;
                mainPivot[1] += y;
            }

            else
            {
               
                if (x == 1)
                {                  
                    for (int i = 0; i < mainShape.GetLength(0); i++)
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
                    mainPivot[1] = nextPivot[1];

                    tetris.SetMainShape(mainShape, map);

                    int[,] tempNext = new int[4, 2];
                    int[] tempPivot = new int[2];

                    int tempnumber = random.Next(0, 5);

                    nextnumber = (tempnumber + nextnumber) % shapeList.Count;


                    tempNext = shapeList[nextnumber].GetShapeType();
                    tempPivot = shapeList[nextnumber].GetPivot();

                    for (int i = 0; i < nextShape.GetLength(0); i++)
                    {
                        nextShape[i, 0] = tempNext[i, 0];
                        nextShape[i, 1] = tempNext[i, 1];
                    }
                    nextPivot[0] = tempPivot[0];
                    nextPivot[1] = tempPivot[1];

                    tetris.ShowNextShape(nextShape);
                    colorNumber = random.Next(0, 10);
                    keyCheck = true;
                }
                
            }
        }

        public static bool CheckMove(int[,] temp, int[,] map)
        {
            for(int i = 0; i < temp.GetLength(0); i++)
            {
                if (temp[i, 0] < 0 || temp[i, 0] >= map.GetLength(0) - 1 || temp[i, 1] <= 0 || temp[i, 1] >= map.GetLength(1) - 1)
                {
                    keyCheck = false;
                    return false;
                }

                if (map[temp[i,0], temp[i, 1]] == 1)
                {
                   keyCheck = false;
                    return false;
                }
            }         
            return true;
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
            tetris.WriteHelp();
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
            int[,] Type1 = new int[4, 2] { { 4, 9 }, { 5, 8 }, { 5, 9 }, { 5, 10 } };
            int[] Typ1Pivot = new int[2] { 5, 9 };

            int[,] Type2 = new int[4, 2] { { 4, 8 }, { 5, 8 }, { 5, 9 }, { 5, 10 } };
            int[] Typ2Pivot = new int[2] {5, 9 };

            int[,] Type3 = new int[4, 2] { { 4, 8 }, { 4, 9 }, { 5, 9 }, { 5, 10 } };
            int[] Typ3Pivot = new int[2] { 5, 9 };

            int[,] Type4 = new int[4, 2] { { 4, 7 }, { 4, 8 }, { 4, 9 }, { 4, 10 } };
            int[] Typ4Pivot = new int[2] { 4, 9 };

            int[,] Type5 = new int[4, 2] { { 4, 8 }, { 4, 9 }, { 5, 8 }, { 5, 9 } };
            int[] Typ5Pivot = new int[2] { -10, -10 };

            shapeList.Add(new Shape(Type1, Typ1Pivot));
            shapeList.Add(new Shape(Type2, Typ2Pivot));
            shapeList.Add(new Shape(Type3, Typ3Pivot));
            shapeList.Add(new Shape(Type4, Typ4Pivot));
            shapeList.Add(new Shape(Type5, Typ5Pivot));

           
                GameStartImage();
                Console.ReadKey();
            Console.Clear();
            
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

            tetris.WriteHelp();
            tetris.ShowNextShape(nextShape);
            RenderGame();
           
            //방향키 입력받기
            while (true)
            {
                keyCheck = true;
              
                ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            Rotate();
                            break;

                        case ConsoleKey.LeftArrow:
                            Move(0, -1, keyCheck);
                            break;

                        case ConsoleKey.RightArrow:
                            Move(0, 1, keyCheck);
                            break;
                        case ConsoleKey.DownArrow:
                            Move(1, 0, keyCheck);
                            break;
                    }
                    RenderGame();
                
              
                    
              
            }

        }
    }
}
