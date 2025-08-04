using System;

class GameField
{
    public int Width { get; }
    public int Height { get; }

    private int[,] field;

    public GameField(int width, int height)
    {
        Width = width;
        Height = height;
        field = new int[height, width];
    }

    public bool CanMove(Figure figure, int x, int y)
    {
        int rows = figure.Shape.GetLength(0);
        int cols = figure.Shape.GetLength(1);

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (figure.Shape[r, c] == 1)
                {
                    int newX = x + c;
                    int newY = y + r;

                    if (newX < 0 || newX >= Width || newY < 0 || newY >= Height)
                        return false;

                    if (field[newY, newX] == 1)
                        return false;
                }
            }
        }
        return true;
    }

    public void PlaceFigure(Figure figure)
    {
        int rows = figure.Shape.GetLength(0);
        int cols = figure.Shape.GetLength(1);

        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
                if (figure.Shape[r, c] == 1)
                    field[figure.Y + r, figure.X + c] = 1;
    }

    public void ClearFullLines()
    {
        for (int r = Height - 1; r >= 0; r--)
        {
            bool fullLine = true;
            for (int c = 0; c < Width; c++)
            {
                if (field[r, c] == 0)
                {
                    fullLine = false;
                    break;
                }
            }

            if (fullLine)
            {
                for (int row = r; row > 0; row--)
                    for (int col = 0; col < Width; col++)
                        field[row, col] = field[row - 1, col];

                for (int col = 0; col < Width; col++)
                    field[0, col] = 0;

                r++;
            }
        }
    }

    public void Draw()
    {
        for (int r = 0; r < Height; r++)
        {
            for (int c = 0; c < Width; c++)
            {
                Console.SetCursorPosition(c, r);
                Console.Write(field[r, c] == 1 ? "#" : ".");
            }
        }
    }
}
