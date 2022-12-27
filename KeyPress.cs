using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Змейка
{
    class KeyPress
    {
        public enum Direction
        {
            up,
            down,
            left,
            right
        }

        private static Thread keyPressThread;
        private static Thread keyPressInGameThread;
        public static void StartThreadMenu()
        {
            keyPressThread = new Thread(ReadKeyConsole);
            keyPressThread.Start();
        }
        public static void StartThreadGame1()
        {
            keyPressInGameThread = new Thread(ReadKeyGame1Console);
            keyPressInGameThread.Start();
        }
        private static void ReadKeyConsole()
        {
            while (Program.isInMenu)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (!Program.isInSubMenu)
                {
                    switch (key.Key.ToString())
                    {
                        case "UpArrow":
                        case "DownArrow":
                            if (Program.menu == 1)
                                Program.menu = 0;
                            else if (Program.menu == 0)
                                Program.menu = 1;
                            Program.CreateMenu();
                            break;
                        case "Spacebar":
                        case "Enter":
                            if (Program.menu == 0)
                            {
                                Program.subMenu = 0;
                                Program.isInSubMenu = true;
                                Program.CreateSubMenu();
                            }
                            else if (Program.menu == 1)
                                return;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (key.Key.ToString())
                    {
                        case "UpArrow":
                            if (Program.subMenu > 0)
                                Program.subMenu--;
                            else
                                Program.subMenu = 3;
                            Program.CreateSubMenu();
                            break;
                        case "DownArrow":
                            if (Program.subMenu < 3)
                                Program.subMenu++;
                            else
                                Program.subMenu = 0;
                            Program.CreateSubMenu();
                            break;
                        case "Spacebar":
                        case "Enter":
                            if (Program.subMenu == 0)
                                Program.StartGame(1);
                            if (Program.subMenu == 1)
                                Program.StartGame(2);
                            else if (Program.subMenu == 3)
                                return;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private static void ReadKeyGame1Console()
        {
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key.ToString())
                {
                    case "UpArrow":
                        if (Game.snakeDirection != Direction.down)
                            Game.snakeDirection = Direction.up;
                        break;
                    case "DownArrow":
                        if (Game.snakeDirection != Direction.up)
                            Game.snakeDirection = Direction.down;
                        break;
                    case "LeftArrow":
                        if (Game.snakeDirection != Direction.right)
                            Game.snakeDirection = Direction.left;
                        break;
                    case "RightArrow":
                        if (Game.snakeDirection != Direction.left)
                            Game.snakeDirection = Direction.right;
                        break;
                    case "Spacebar":
                        if (!Game.isDie)
                            Game.isGameStart = !Game.isGameStart;
                        else
                            Game.StartGame();
                        break;
                    default:
                        break;
                }
            }
        }

        public static void EndThread()
        {
            if (keyPressThread != null)
                if (keyPressThread.IsAlive)
                    keyPressThread.Abort();
            if (keyPressInGameThread != null)
                if (keyPressInGameThread.IsAlive)
                    keyPressInGameThread.Abort();
        }
    }
}