using System;
using System.Threading;

class Program
{
    static void Main()
    {
        int width = 10;
        int height = 20;

        GameField field = new GameField(width, height);

        int[][,] figures = new int[][,]
        {
            new int[,] { {1,1,1,1} }, // I
            new int[,] { {1,1}, {1,1} }, // O
            new int[,] { {0,1,0}, {1,1,1} }, // T
            new int[,] { {1,0}, {1,0}, {1,1} }, // L
            new int[,] { {0,1}, {0,1}, {1,1} }, // J
            new int[,] { {0,1,1}, {1,1,0} }, // S
            new int[,] { {1,1,0}, {0,1,1} }  // Z
        };

        Random rnd = new Random();

        Figure figure = CreateNewFigure();

        bool gameOver = false;

        Console.CursorVisible = false;

        while (!gameOver)
        {
            Console.Clear();
            field.Draw();
            figure.Draw(field);

            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        if (field.CanMove(figure, figure.X - 1, figure.Y))
                            figure.X--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (field.CanMove(figure, figure.X + 1, figure.Y))
                            figure.X++;
                        break;
                    case ConsoleKey.UpArrow:
                        Figure rotated = figure.GetRotated();
                        if (field.CanMove(rotated, figure.X, figure.Y))
                            figure = rotated;
                        break;
                    case ConsoleKey.DownArrow:
                        if (field.CanMove(figure, figure.X, figure.Y + 1))
                            figure.Y++;
                        break;
                }
            }

            if (field.CanMove(figure, figure.X, figure.Y + 1))
            {
                figure.Y++;
            }
            else
            {
                field.PlaceFigure(figure);
                field.ClearFullLines();

                figure = CreateNewFigure();

                if (!field.CanMove(figure, figure.X, figure.Y))
                    gameOver = true;
            }

            Thread.Sleep(300);
        }

        Console.Clear();
        Console.WriteLine("Игра окончена! Спасибо за игру.");

        Figure CreateNewFigure()
        {
            int index = rnd.Next(figures.Length);
            Figure f = new Figure(figures[index]);
            f.X = width / 2 - f.Shape.GetLength(1) / 2;
            f.Y = 0;
            return f;
        }
    }
}
