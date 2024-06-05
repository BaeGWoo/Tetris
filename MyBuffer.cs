using System;
using System.Text;

public class MyBuffer
{
    private string[,] buffer;
    private string[,] previousBuffer;
    private int width;
    private int height;
    
    public int Width { get { return width; } }
    public int Height { get { return height; } }

    public MyBuffer(int height, int width)
    {
        this.width = width;
        this.height = height;
        buffer = new string[height*2, width*2];
        previousBuffer = new string[height * 2, width * 2];
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

    public void Render()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (buffer[y, x] != previousBuffer[y, x])
                {
                    Console.SetCursorPosition(35+x*2,5+ y);
                    Console.Write(buffer[y, x]);
                    previousBuffer[y, x] = buffer[y, x];
                }
            }
        }
    }
}
