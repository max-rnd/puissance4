using System;
using System.Threading;

namespace puissance4
{
    class Program
    {
        static void Main(string[] args)
        {
            const int X = 18;
            const int Y = 2;

            int x = X;
            int y = Y;
            int l = 4;
            int h = 2;
            int posiV, posiH;
            int fmove;
            bool anim;
            char joueur = 'R';
            bool gagnee;
            bool jouer = true;
            String rejouer;

            while (jouer)
            {

                tableP4(x - 2, y + 3);

                char[,] tableau = new char[,] {
                { '0', '0', '0', '0','0', '0','0' },
                { ' ', ' ', ' ', ' ',' ', ' ',' ' },
                { ' ', ' ', ' ', ' ',' ', ' ',' ' },
                { ' ', ' ', ' ', ' ',' ', ' ',' ' },
                { ' ', ' ', ' ', ' ',' ', ' ',' ' },
                { ' ', ' ', ' ', ' ',' ', ' ',' ' },
                { ' ', ' ', ' ', ' ',' ', ' ',' ' },
                { '1', '1', '1', '1','1', '1','1' }
            };

                bool tour = true;
                while (tour)
                {
                    x = X;
                    y = Y;
                    posiH = 0;
                    posiV = 0;
                    fmove = 1;
                    anim = true;
                    if (joueur == 'R')
                    {
                        joueur = 'Y';
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                    }
                    else
                    {
                        joueur = 'R';
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                    }
                    Console.SetCursorPosition(x, y);
                    Console.Write("    ");
                    Console.SetCursorPosition(x, y + 1);
                    Console.Write("    ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(0, 0);
                    bool choix = true;
                    while (choix)
                    {
                        ConsoleKeyInfo info = Console.ReadKey(true);
                        switch (info.Key)
                        {
                            case ConsoleKey.LeftArrow:
                                if (x > X)
                                {
                                    Console.MoveBufferArea(x, y, l, h, x - 6, y);
                                    x = x - 6;
                                    posiH--;
                                }
                                break;
                            case ConsoleKey.RightArrow:
                                if (x < X + 38 - l)
                                {
                                    Console.MoveBufferArea(x, y, l, h, x + 6, y);
                                    x = x + 6;
                                    posiH++;
                                }
                                break;
                            case ConsoleKey.Escape:
                                Console.Clear();
                                joueur = 'N';
                                choix = false;
                                tour = false;
                                break;
                            case ConsoleKey.Enter:
                                while (anim)
                                {
                                    if (tableau[posiV + 1, posiH] == ' ')
                                    {
                                        Console.MoveBufferArea(x, y, l, h, x, y + 3 + fmove);
                                        y = y + 3 + fmove;
                                        if (fmove == 1)
                                            fmove--;
                                        Thread.Sleep(200);
                                        posiV++;
                                    }
                                    else
                                        anim = false;
                                }
                                tableau[posiV, posiH] = joueur;
                                choix = false;
                                break;
                        }
                        Win(tableau, joueur, out gagnee);
                        if (gagnee)
                            tour = false;
                    }
                }
                switch (joueur)
                {
                    case 'R':
                        Console.WriteLine("Le joueur rouge a gagnée.");
                        break;
                    case 'Y':
                        Console.WriteLine("Le joueur jaune a gagnée.");
                        break;
                }
                Console.Write("Voulez-vous rejouer ? (Y/N) ");
                rejouer = Console.ReadLine().ToUpper();
                switch (rejouer)
                {
                    case "Y":
                        Console.Clear();
                        break;
                    case "N":
                        Console.Clear();
                        jouer = false;
                        break;
                }
            }
        }
        static void tableP4(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write("                                            ");
            for (int lg = 1; lg < 17; lg = lg + 3)
            {
                for (int cl = 0; cl < 43; cl = cl + 6)
                {
                    Console.SetCursorPosition(x + cl, y + lg);
                    Console.Write("  ");
                    Console.SetCursorPosition(x + cl, y + lg + 1);
                    Console.Write("  ");
                }
                Console.SetCursorPosition(x, y + lg + 2);
                Console.Write("                                            ");
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
        static void Win(char[,] tableau, char joueur, out bool gagnee)
        {
            gagnee = false;
            bool[] step = new bool[3];
            int x;
            int y;
            int count;
            // HORIZONTAL
            y = 0;
            count = 0;
            step[0] = true;
            while (step[0])
            {
                x = 0;
                step[1] = true;
                while (step[1])
                {
                    step[2] = true;
                    while (step[2])
                    {
                        if (tableau[y, x] == joueur)
                        {
                            count++;
                            x++;
                            if (count == 4)
                            {
                                gagnee = true;
                                step[2] = false;
                                step[1] = false;
                                step[0] = false;
                            }
                        }
                        else
                        {
                            step[2] = false;
                            count = 0;
                        }
                        if (x > 6)
                            step[2] = false;
                    }
                    count = 0;
                    x++;
                    if (x > 6)
                        step[1] = false;
                }
                y++;
                if (y > 6)
                    step[0] = false;
            }
            // VERTICAL
            x = 0;
            count = 0;
            if (!gagnee)
                step[0] = true;
            while (step[0])
            {
                y = 0;
                step[1] = true;
                while (step[1])
                {
                    step[2] = true;
                    while (step[2])
                    {
                        if (tableau[y, x] == joueur)
                        {
                            count++;
                            y++;
                            if (count == 4)
                            {
                                gagnee = true;
                                step[2] = false;
                                step[1] = false;
                                step[0] = false;
                            }
                        }
                        else
                        {
                            step[2] = false;
                            count = 0;
                        }
                        if (y < 0)
                            step[2] = false;
                    }
                    y++;
                    if (y > 6)
                        step[1] = false;
                }
                x++;
                if (x > 6)
                    step[0] = false;
            }
            // DIAGONALE AUGM
            x = 0;
            count = 0;
            if (!gagnee)
                step[0] = true;
            while (step[0])
            {
                y = 0;
                step[1] = true;
                while (step[1])
                {
                    step[2] = true;
                    while (step[2])
                    {
                        if (tableau[y, x] == joueur)
                        {
                            count++;
                            x++;
                            y--;
                            if (count == 4)
                            {
                                gagnee = true;
                                step[2] = false;
                                step[1] = false;
                                step[0] = false;
                            }
                        }
                        else
                        {
                            step[2] = false;
                            x = x - count;
                            y = y + count;
                        }
                        if (y < 0 || x > 6)
                            step[2] = false;
                    }
                    count = 0;
                    y++;
                    if (y > 6)
                        step[1] = false;
                }
                x++;
                if (x > 6)
                    step[0] = false;
            }
            // DIAGONALE DIMIN
            x = 0;
            count = 0;
            if (!gagnee)
                step[0] = true;
            while (step[0])
            {
                y = 0;
                step[1] = true;
                while (step[1])
                {
                    step[2] = true;
                    while (step[2])
                    {
                        if (tableau[y, x] == joueur)
                        {
                            count++;
                            x++;
                            y++;
                            if (count == 4)
                            {
                                gagnee = true;
                                step[2] = false;
                                step[1] = false;
                                step[0] = false;
                            }
                        }
                        else
                        {
                            step[2] = false;
                            x = x - count;
                            y = y - count;
                        }
                        if (y < 0 || x > 6)
                            step[2] = false;
                    }
                    count = 0;
                    y++;
                    if (y > 6)
                        step[1] = false;
                }
                x++;
                if (x > 6)
                    step[0] = false;
            }
        }
    }
}
