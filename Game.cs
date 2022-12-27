using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Змейка
{
    class Game
    {
        public static bool isGameStart = false;
        public static bool isDie = false;
        private static int score = 0;
        private static int hightScore = 0;
        private static int widthWall = 50;
        private static int heightWall = 22;

        private static List<Point> snake;
        private static Point apple;
        
        public static KeyPress.Direction snakeDirection = KeyPress.Direction.right;
        public static void StartGame()
        {
            BeginSettingsGame();
            BeginGame();
        }
        private static void BeginSettingsGame()
        {
            Console.Clear();
            score = 0;
            isGameStart = false;
            CreateWall();
            CreateSnake();
            PrintSnake();
            CreateApple();
            PrinApple();
            snakeDirection = KeyPress.Direction.right;
        }
        private static void CreateWall()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < heightWall; i++)
            {
                Console.CursorTop = i;
                for (int j = 0; j < widthWall; j++)
                {
                    Console.CursorLeft = j;
                    if (i == 0)
                        Console.Write("♦");
                    else if (i < heightWall - 1)
                    {
                        if (j == 0)
                            Console.Write("♦");
                        if (j == widthWall - 1)
                            Console.Write("♦");
                    }
                    else
                        Console.Write("♦");
                }
            }
        }
        private static void CreateSnake()
        {
            Point headPoint = new Point(7, 7);
            Point bodyPoint = new Point(6, 7);
            Point tailPoint = new Point(5, 7);
            snake = new List<Point>();
            snake.Add(headPoint);
            snake.Add(bodyPoint);
            snake.Add(tailPoint);
        }
        private static void PrintSnake()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (Point point in snake)
            {
                Console.SetCursorPosition(point.PosX, point.PosY);
                Console.Write("♦");
            }
        }
        private static void CreateApple()
        {
            Random rand = new Random();
            bool isCheckApple = false;
            do
            {
                Point applePoint = new Point(rand.Next(1, widthWall - 1), rand.Next(1, heightWall - 1));
                apple = applePoint;
            } while (isCheckApple);
        }
        private static void PrinApple()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(apple.PosX, apple.PosY);
            Console.Write("♦");
        }

        private static void BeginGame()
        {
            isGameStart = true;
            isDie = false;
            KeyPress.StartThreadGame1();
            while (!isDie)
                while (isGameStart)
                {
                    PrinApple();
                    MoveSnake();
                    PrintSnake();
                    PrintMenu();
                    Thread.Sleep(100);
                }
        }
        private static void MoveSnake()
        {
            int prHeadX = snake[0].PosX;
            int prHeadY = snake[0].PosY;
            switch (snakeDirection)
            {
                case KeyPress.Direction.up:
                    snake[0].PosY--;
                    break;
                case KeyPress.Direction.down:
                    snake[0].PosY++;
                    break;
                case KeyPress.Direction.left:
                    snake[0].PosX--;
                    break;
                case KeyPress.Direction.right:
                    snake[0].PosX++;
                    break;
            }
            if (snake[0].PosX == 0 || snake[0].PosY == 0 || snake[0].PosX == widthWall - 1 || snake[0].PosY == heightWall - 1)
            {
                isGameStart = false;
                isDie = true;
            }
            foreach (Point point in snake)
                if (snake[0] != point && snake[0].PosX == point.PosX && snake[0].PosY == point.PosY)
                {
                    isGameStart = false;
                    isDie = true;
                }
            if (snake[0].PosX == apple.PosX && snake[0].PosY == apple.PosY)
            {
                snake.Add(new Point(apple.PosX, apple.PosY));
                CreateApple();
                score += 10;
                if (score > hightScore)
                    hightScore = score;
            }
            for (int i = snake.Count - 1; i > 0; i--)
            {
                if (i == snake.Count - 1)
                {
                    Console.SetCursorPosition(snake[i].PosX, snake[i].PosY);
                    Console.Write(" ");
                }

                if (i - 1 > 0)
                {
                    snake[i].PosX = snake[i - 1].PosX;
                    snake[i].PosY = snake[i - 1].PosY;
                }
                else
                {
                    snake[i].PosX = prHeadX;
                    snake[i].PosY = prHeadY;
                }
            }
        }
        private static void PrintMenu()
        {
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.SetCursorPosition(widthWall + 3, 1);
            Console.Write("Счет: " + score);
            Console.SetCursorPosition(widthWall + 3, 2);
            Console.Write("Лучший счет: " + hightScore);

            Console.SetCursorPosition(5, heightWall);
            Console.Write("Для управления нажимайте клавиши: ↑ ↓ → ← ");
            Console.SetCursorPosition(5, heightWall + 1);
            Console.Write("Для паузы нажмите \"Пробел\"");

            if (isDie)
            {
                Console.SetCursorPosition(widthWall + 3, 5);
                Console.Write("Увы, Вы проиграли!");
                Console.SetCursorPosition(widthWall + 3, 7);
                Console.Write("Пробел для рестарта!");
            }
        }
    }
}