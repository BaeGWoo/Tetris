using System;
using System.Timers;
namespace Tetris
{
    internal class Program
    {
        static Map map = new Map();
        static void MoveDown(Map map)
        {
            map.MainShapeMove();
        }


        private static void CountDown(object sender, ElapsedEventArgs e)
        {
            MoveDown(map);
        }

        static void Main(string[] args)
        {
            map.ShowNextShape();
            map.SetMainShape();
            map.DrawBaseMap();
            Console.Write("");
            DateTime nowDate = DateTime.Now;
            int pivot = 0;
          


            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += new ElapsedEventHandler(CountDown);
            timer.Start();


            while (true) {
               
                          }

     
        }
    }
}
