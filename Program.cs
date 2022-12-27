using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Змейка;

namespace Змейка
{
    class Program
    {
        public static int menu = 0;
        public static bool isInMenu = true;
        public static int subMenu = 0;
        public static bool isInSubMenu = false;
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            Console.Title = "Змейка :3";
            StartMenu();
            KeyPress.StartThreadMenu();
        }
        static void StartMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string[] str = new string[]{"", "", "",
                                        "\t##############################################################",
                                        "\t##      $$$$$  $$       $$ $$$$ $ $$ $ $  $       $         ##",
                                        "\t##     $     $ $ $     $ $ $    $   $$ $  $      $ $        ##",
                                        "\t##          $$ $  $   $  $ $$$$ $  $ $ $ $      $   $       ##",
                                        "\t##         $$  $   $ $   $ $    $ $  $ $$      $$$$$$$      ##",
                                        "\t##     $     $ $    $    $ $    $$   $ $ $    $       $     ##",
                                        "\t##      $$$$$  $         $ $$$$ $    $ $  $  $         $    ##",
                                        "\t##############################################################"};
            for (int i = 10; i < str.Length; i++)
            {
                Console.Clear();
                int k = i;
                for (int j = str.Length - 1; j >= str.Length - i - 1; j--)
                {
                    Console.SetCursorPosition(0, k);
                    Console.Write(str[j]);
                    k--;
                }
                Thread.Sleep(500);
            }
            CreateMenu();
        }
        public static void CreateMenu()
        {
            if (menu == 0)
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(30, 15);
            Console.Write("СТАРТ");
            if (menu == 1)
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(30, 17);
            Console.Write("ВЫХОД");
        }
        public static void CreateSubMenu()
        {
            string text = "Начать игру?";
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(text);
            if (subMenu == 0)
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(30, 12);
            Console.Write("Начать");
            if (subMenu == 1)
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(30, 14);
            Console.Write("Выход");
        }

        public static void StartGame(int level = 1)
        {
            Console.Title = "Змейка :3 - Режим " + level;
            if (level == 1)
                Game.StartGame();
            isInMenu = false;
            isInSubMenu = false;
        }

        static void CurrentDomain_ProcessExit(Object sender, EventArgs e)
        {
            KeyPress.EndThread();
        }
    }
}