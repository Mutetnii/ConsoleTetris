using System;

class Figure
{
    public int[,] Shape { get; private set; }
    public int X;
    public int Y;

    public Figure(int[,] shape)
    {
        Shape = shape;
    }

    public Figure GetRotated()
    {
        int rows = Shape.GetLength(0);
        int cols = Shape.GetLength(1);
        int[,] rotated = new int[cols, rows];
        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
                rotated[c, rows - 1 - r] = Shape[r, c];
        return new Figure(rotated) { X = this.X, Y = this.Y };
    }

    public void Draw(GameField field)
    {
        for (int r = 0; r < Shape.GetLength(0); r++)
        {
            for (int c = 0; c < Shape.GetLength(1); c++)
            {
                if (Shape[r, c] == 1)
                {
                    int drawX = X + c;
                    int drawY = Y + r;
                    if (drawY >= 0 && drawY < field.Height && drawX >= 0 && drawX < field.Width)
                    {
                        Console.SetCursorPosition(drawX, drawY);
                        Console.Write("#");
                    }
                }
            }
        }
    }
}
