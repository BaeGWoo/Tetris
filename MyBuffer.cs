using System;
using System.Text;

public class MyBuffer
{
    private string[,] buffer;
    private string[,] previousBuffer;
    private int[,] Curbuffer;
    private int width;
    private int height;
    
    public int Width { get { return width; } }
    public int Height { get { return height; } }

    public MyBuffer(int height, int width)
    {
        this.width = width;
        this.height = height;
        buffer = new string[height, width];
        previousBuffer = new string[height, width ];
        Curbuffer = new int[height, width ];
        Clear();
    }

    public void Clear()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                buffer[y, x] = "  ";
            }
        }
    }

    
    public void Draw(int x, int y, string c)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            buffer[y, x] = c;
        }
    }

    
    public void Render(ConsoleColor cur,ConsoleColor prev, int[,] map,bool keyCheck)
    {
        if (!keyCheck)
            return;
        Console.ForegroundColor = cur;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
             if (map[y, x] != Curbuffer[y, x])
                {
                    Console.SetCursorPosition(34 + x * 2, 5 + y);
                    if (map[y, x] == 1)
                    {
                        Console.ForegroundColor = prev;
                        Console.Write("■");
                    }
                    else if (map[y, x] == 2)
                    {
                        Console.Write("■");
                    }
                    else if (map[y, x] == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("■");
                        Console.ForegroundColor = cur;
                    }

                    else if (map[y, x] == 4)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("■");
                        Console.ForegroundColor = cur;
                    }

                    else
                    {
                        Console.Write("  ");
                    }                   
                    Curbuffer[y, x] = map[y, x];
                }
                
            }
        }
        Console.ForegroundColor = ConsoleColor.White;
    }




    



}
