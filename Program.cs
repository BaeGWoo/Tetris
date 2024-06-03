using System;
using System.Timers;
namespace Tetris
{
    internal class Program
    {
        static Map map = new Map();
        static void MoveDown(Map map)
        {
            if (!map.Rotate)
                map.MainShapeMove();
        }


        private static void CountDown(object sender, ElapsedEventArgs e)
        {
            MoveDown(map);
        }

        static void Main(string[] args)
        {
            bool rotate = false;

            map.ShowNextShape();
            map.SetMainShape();
            map.ShowNextShape();

            map.DrawBaseMap();
            Console.Write("");
            DateTime nowDate = DateTime.Now;
            int pivot = 0;
          


            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 500;
            timer.Elapsed += new ElapsedEventHandler(CountDown);
            timer.Start();


            while (true) {


                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {

                    case ConsoleKey.UpArrow:
                        map.RotateShape();
                        break;

                    case ConsoleKey.LeftArrow:
                        map.LeftMove = true;
                        break;

                    case ConsoleKey.RightArrow:
                        map.RightMove = true;
                        break;
                    case ConsoleKey.DownArrow:
                        map.MainShapeMove();
                        break;
                }
               
                          }

     
        }
    }
}
