using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DodgingRocks
{
    class Coordinate
    {
        public int X, Y;

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

    }

    public class Program
    {

        private static int width = 50, height = 250, xPos, difficulty, counter;
        private static Coordinate currentPos, exitPoint;
        private static bool alive, drawingLine, finished;
        private static char[,] level;

        public static void Main(string[] args)
        {
            Console.WindowHeight = 50;
            Console.WindowWidth = width;
            Console.CursorVisible = false;
            Console.SetBufferSize(width + 10, height + 10);


            while (true)
            {
                alive = true;
                drawingLine = false;
                counter = 1;

                Console.Clear();
                Console.WriteLine("Press i for instructions or press s to start.");
                string input = Console.ReadLine();

                try
                {
                    switch (input.ToLower())
                    {
                        case "i":
                            Console.Clear();
                            Console.WriteLine(string.Format("Direct the space craft {0} through the asteroids *.", '\u25BC'));
                            Console.WriteLine("Use the left and right arrow keys to move side");
                            Console.WriteLine("to side.");
                            Console.WriteLine("The difficulty changes the number of asteroids.");
                            Console.WriteLine("Press any key to return to main menu.");
                            Console.ReadLine();
                            break;
                        case "s":
                            StartGame();
                            break;
                        default:
                            Console.WriteLine("Input not recognised. Please try again.");
                            Thread.Sleep(5000);
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("Input not recognised. Please try again.");
                    Thread.Sleep(5000);
                    break;
                }
            }
        }

        public static void StartGame()
        {
            Console.WriteLine("Please enter a difficulty between 1 and 20");
            Console.WriteLine("then press enter to start game.");
            string input = Console.ReadLine();

            if (int.TryParse(input, out difficulty))
            {
                if (difficulty > 0 && difficulty <= 20)
                {
                    Console.Clear();
                    level = GenerateMap();

                    for (int h = 0; h < Console.WindowHeight; h++)
                    {
                        for (int w = 0; w < width; w++)
                        {
                            Console.Write(string.Format("{0}", level[h, w]));
                        }
                        Console.WriteLine();
                    }

                    new Thread(ProgressGame).Start();

                    while (alive && ! finished)
                    {
                        GetInput();
                    }

                    if (alive)
                    {
                        Console.Clear();
                        Console.WriteLine("Congratulations! You have won!");
                    }
                    else
                    {
                        Thread.Sleep(2000);
                        Console.Clear();
                        Console.WriteLine("I'm sorry, this time you have lost!");
                    }
                    Thread.Sleep(2000);
                }
                else
                {
                    Console.WriteLine(string.Format("I'm sorry, your input of \"{0}\"", input));
                    Console.WriteLine("is not recognised as a number between 1 and 20.");
                    Thread.Sleep(2000);
                }
            }
            else
            {
                Console.WriteLine(string.Format("I'm sorry, your input of \"{0}\"", input));
                Console.WriteLine("is not recognised as a number between 1 and 20.");
                Thread.Sleep(5000);
            }
        }

        public static char[,] GenerateMap()
        {
            var map = new char[height, width];

            //Add Corners
            map[0, 0] = '\u250C';
            map[0, width - 1] = '\u2510';
            map[height - 1, 0] = '\u2514';
            map[height - 1, width - 1] = '\u2518';

            //Add Sides
            for (int h = 1; h < height - 1; h++)
            {
                map[h, 0] = '\u2502';
                map[h, width - 1] = '\u2502';
            }
            //Add Top and bottom
            for (int w = 1; w < width - 1; w++)
            {
                map[0, w] = '\u2500';
                map[height - 1, w] = '\u2500';
            }

            //Add Rocks at random to map - don't add to top or bottom 5 rows
            //Probability of adding rock is based on difficulty selected
            var rnd = new Random();
            for (int h = 1; h < height - 1; h++)
            {
                for (int w = 1; w < width - 1; w++)
                {
                    if (h > 6 && h < height - 6)
                    {
                        if (rnd.Next(100) < difficulty)
                        {
                            map[h, w] = '*';
                        }
                        else
                        {
                            map[h, w] = ' ';
                        }
                    }
                    else
                    {
                        map[h, w] = ' ';
                    }
                }
            }

            //Add Start position as down arrow - middle top
            if (width % 2 == 0)
            {
                xPos = width / 2;
            }
            else
            {
                xPos = (width + 1) / 2;
            }

            currentPos = new Coordinate(1, xPos);

            map[1, xPos] = '\u25BC';

            //Add Exit at random point along bottom
            exitPoint = new Coordinate(rnd.Next(2, width - 2), height - 1);
            map[exitPoint.Y, exitPoint.X] = ' ';

            return map;
        }

        static void GetInput()
        {
            ConsoleKeyInfo key;

            while (Console.KeyAvailable == false || drawingLine == true)
            {
                Thread.Sleep(100);
            }

            key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    xPos--;
                    MovePointer();
                    break;
                case ConsoleKey.RightArrow:
                    xPos++;
                    MovePointer();
                    break;
            }
        }

        static void ProgressGame()
        {
            while (counter < height - 1 && alive == true)
            {
                Thread.Sleep(500);
                ScrollMap();
                counter++;
                MovePointer();
            }
        }

        static void MovePointer()
        {
            if (alive && ! finished)
            {
                // clear pointer
                Console.SetCursorPosition(currentPos.X, currentPos.Y);
                Console.Write(' ');

                // add pointer in new position updating current position to reflect change
                currentPos.X = xPos;
                currentPos.Y = counter;
                Console.SetCursorPosition(xPos, counter);

                if (level[counter, xPos] == ' ')
                {
                    Console.Write('\u25BC');
                    if (counter == height - 1)
                    {
                        finished = true;
                    }
                }
                else
                {
                    Console.Write('\u2020');
                    alive = false;
                }
            }
        }

        static void ScrollMap()
        {
            drawingLine = true;
            if (alive && Console.WindowHeight + counter < height)
            {   
                Console.SetCursorPosition(0, Console.WindowHeight + counter - 1);
                for (int w = 0; w < width; w++)
                {
                    Console.Write(string.Format("{0}", level[Console.WindowHeight + counter, w]));
                }
                Console.WriteLine();
            }
            drawingLine = false;
        }
    }

}
