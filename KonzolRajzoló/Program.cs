using System;
using System.IO;

namespace KonzolosRajzolo
{
    class Program
    {
        // rajzlap méretei
        const int RajzlapSzelesseg = 80;
        const int RajzlapMagassag = 25;

        // vizuális szélei a rajzlapnak x,y tengelyen
        const int RajzlapKezdoX = 28;
        const int RajzlapKezdoY = 2;

        static string[] menuPontok = {
            "Karakter választása",
            "Szín választása",
            "Minta betöltése fájlból",
            "Szabadkézi rajzolás",
            "Kilépés"
        };

        static bool rainbowMode = false;
        static int szinIdx = 0;
        static ConsoleColor[] szivarvanySzinek = {
            ConsoleColor.Red,
            ConsoleColor.Yellow,
            ConsoleColor.Green,
            ConsoleColor.Cyan,
            ConsoleColor.Blue,
            ConsoleColor.Magenta
        };

        // aktuális rajzkarakter és szín
        static char aktualisKarakter = '#';
        static ConsoleColor aktualisSzin = ConsoleColor.White;

        static int kivalasztottMenuIdx = 0;

        static void Main()
        {
            try
            {
                if (Console.WindowWidth < 92)
                {
                    Console.WindowWidth = 92;
                }

                if (Console.WindowHeight < 30)
                {
                    Console.WindowHeight = 30;
                }
            }
            catch
            {
            }

            Console.CursorVisible = false;

            TeljesKepernyoRajzolasa();

            bool fut = true;

            while (fut)
            {
                Console.SetCursorPosition(0, 10);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($"Karakter: {aktualisKarakter}   ");
                Console.SetCursorPosition(0, 11);
                Console.Write($"Szín: {aktualisSzin}      ");

                MenuKirajzolasa();

                ConsoleKeyInfo bekertBillentyu = Console.ReadKey(true);

                switch (bekertBillentyu.Key)
                {
                    case ConsoleKey.UpArrow:
                        kivalasztottMenuIdx--;
                        if (kivalasztottMenuIdx < 0)
                        {
                            kivalasztottMenuIdx = menuPontok.Length - 1;
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        kivalasztottMenuIdx++;
                        if (kivalasztottMenuIdx >= menuPontok.Length)
                        {
                            kivalasztottMenuIdx = 0;
                        }
                        break;

                    case ConsoleKey.Enter:
                        if (kivalasztottMenuIdx == 0)
                        {
                            Console.SetCursorPosition(0, 16);
                            aktualisKarakter = KarakterValasztasa();
                        }
                        else if (kivalasztottMenuIdx == 1)
                        {
                            Console.SetCursorPosition(0, 16);
                            aktualisSzin = SzinValasztasa();
                        }
                        else if (kivalasztottMenuIdx == 2)
                        {
                            Console.SetCursorPosition(0, 16);
                            MintaBetoltese(RajzlapKezdoX, RajzlapKezdoY);
                        }
                        else if (kivalasztottMenuIdx == 3)
                        {
                            Console.SetCursorPosition(0, 16);
                            SzabadkeziRajzolas();
                        }
                        else if (kivalasztottMenuIdx == menuPontok.Length - 1)
                        {
                            fut = false;
                        }
                        break;
                }
            }

            Console.ResetColor();
            Console.Clear();
        }

        static void TeljesKepernyoRajzolasa()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 0);
            Console.Write("KONZOLOS RAJZOLÓ");
            Console.SetCursorPosition(RajzlapKezdoX, 0);
            Console.Write("RAJZLAP");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(RajzlapKezdoX - 1, RajzlapKezdoY - 1);
            Console.Write("+" + new string('-', RajzlapSzelesseg) + "+");

            for (int y = 0; y < RajzlapMagassag; y++)
            {
                Console.SetCursorPosition(RajzlapKezdoX - 1, RajzlapKezdoY + y);
                Console.Write("|");
                Console.SetCursorPosition(RajzlapKezdoX + RajzlapSzelesseg, RajzlapKezdoY + y);
                Console.Write("|");
            }

            Console.SetCursorPosition(RajzlapKezdoX - 1, RajzlapKezdoY + RajzlapMagassag);
            Console.Write("+" + new string('-', RajzlapSzelesseg) + "+");

            Console.SetCursorPosition(0, 13);
            Console.Write("Fel/Le: választás");
            Console.SetCursorPosition(0, 14);
            Console.Write("Enter: megnyitás");
        }

        static void MenuKirajzolasa()
        {
            for (int i = 0; i < menuPontok.Length; i++)
            {
                Console.SetCursorPosition(0, 2 + i);
                if (i == kivalasztottMenuIdx)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("> " + menuPontok[i]);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("  " + menuPontok[i]);
                }
            }
        }

        static char KarakterValasztasa()
        {
            Console.SetCursorPosition(0, 16);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Új rajzkarakter:           ");
            Console.SetCursorPosition(17, 16);

            Console.CursorVisible = true;
            ConsoleKeyInfo betuInfo = Console.ReadKey(false);
            Console.CursorVisible = false;

            if (!char.IsControl(betuInfo.KeyChar))
            {
                Console.SetCursorPosition(0, 16);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"Karakter: {betuInfo.KeyChar}                ");

                return betuInfo.KeyChar;
            }
            else
            {
                Console.Beep(400, 120);
                Console.SetCursorPosition(0, 16);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Érvénytelen karakter!      ");

                return aktualisKarakter;
            }
        }

        static ConsoleColor SzinValasztasa()
        {
            Console.SetCursorPosition(0, 16);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("1:Feh 2:Pir 3:Zöl        ");
            Console.SetCursorPosition(0, 17);
            Console.Write("4:Kék 5:Sár              ");

            ConsoleKeyInfo gomb = Console.ReadKey(true);

            Console.SetCursorPosition(0, 16);
            Console.Write(new string(' ', 27));
            Console.SetCursorPosition(0, 17);
            Console.Write(new string(' ', 27));
            Console.SetCursorPosition(0, 16);

            switch (gomb.KeyChar)
            {
                case '1':
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Szín: Fehér                ");
                    return ConsoleColor.White;
                case '2':
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Szín: Piros                ");
                    return ConsoleColor.Red;
                case '3':
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Szín: Zöld                 ");
                    return ConsoleColor.Green;
                case '4':
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Szín: Kék                  ");
                    return ConsoleColor.Blue;
                case '5':
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Szín: Sárga                ");
                    return ConsoleColor.Yellow;
                default:
                    Console.Beep(400, 120);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Érvénytelen szín választás!");
                    return aktualisSzin;
            }
        }

        static void MintaBetoltese(int celX, int celY)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 16);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Fájlnév (pl. minta.txt):   ");
            Console.SetCursorPosition(0, 17);

            Console.CursorVisible = true;
            string fajlNev = Console.ReadLine();
            Console.CursorVisible = false;

            Console.SetCursorPosition(0, 16);
            Console.Write(new string(' ', 27));
            Console.SetCursorPosition(0, 17);
            Console.Write(new string(' ', 27));
            Console.SetCursorPosition(0, 16);

            if (!string.IsNullOrWhiteSpace(fajlNev) && File.Exists(fajlNev))
            {
                try
                {
                    string[] sorok = File.ReadAllLines(fajlNev);
                    int maxSor = Math.Min(sorok.Length, RajzlapKezdoY + RajzlapMagassag - celY);

                    for (int y = 0; y < maxSor; y++)
                    {
                        string sor = sorok[y];
                        int maxOszlop = Math.Min(sor.Length, RajzlapKezdoX + RajzlapSzelesseg - celX);

                        for (int x = 0; x < maxOszlop; x++)
                        {
                            char ch = sor[x];
                            if (ch != ' ')
                            {
                                Console.SetCursorPosition(celX + x, celY + y);
                                Console.ForegroundColor = RajzSzinLekerdezese();
                                Console.Write(ch);
                            }
                        }
                    }

                    Console.SetCursorPosition(0, 16);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"Betöltve: {sorok.Length} sor          ");
                }
                catch (Exception)
                {
                    Console.Beep(400, 120);
                    Console.SetCursorPosition(0, 16);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Hiba a fájl olvasásakor!  ");
                }
            }
            else
            {
                Console.Beep(400, 120);
                Console.SetCursorPosition(0, 16);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("A fájl nem található!      ");
            }
        }

        static string[] NyomdaMintaLekerdezese(ConsoleKey gomb)
        {
            switch (gomb)
            {
                case ConsoleKey.F1:
                    return new[]
                    {
                        "#####",
                        "#   #",
                        "#   #",
                        "#   #",
                        "#####"
                    };
                case ConsoleKey.F2:
                    return new[]
                    {
                        "#########",
                        "#       #",
                        "#       #",
                        "#       #",
                        "#########"
                    };
                case ConsoleKey.F3:
                    return new[]
                    {
                        "####",
                        "####",
                        "####",
                        "####"
                    };
                case ConsoleKey.F4:
                    return new[]
                    {
                        "    #    ",
                        "   # #   ",
                        "  #   #  ",
                        " #     # ",
                        "#########"
                    };
                case ConsoleKey.F5:
                    return new[]
                    {
                        "  #  ",
                        " # # ",
                        "#   #",
                        " # # ",
                        "  #  "
                    };
                case ConsoleKey.F6:
                    return new[]
                    {
                        "  #  ",
                        "  #  ",
                        "#####",
                        "  #  ",
                        "  #  "
                    };
                case ConsoleKey.F7:
                    return new[]
                    {
                        "#   #",
                        " # # ",
                        "  #  ",
                        " # # ",
                        "#   #"
                    };
                case ConsoleKey.F8:
                    return new[]
                    {
                        "#    ",
                        "##   ",
                        "###  ",
                        "#### ",
                        "#####"
                    };
                case ConsoleKey.F9:
                    return new[]
                    {
                        "  #  ",
                        "   # ",
                        "#####",
                        "   # ",
                        "  #  "
                    };
                case ConsoleKey.F10:
                    return new[]
                    {
                        " ## ## ",
                        "#######",
                        "#######",
                        " ##### ",
                        "  ###  ",
                        "   #   "
                    };
                case ConsoleKey.F11:
                    return new[]
                    {
                        "  #  ",
                        " #.# ",
                        "#.#.#",
                        " #.# ",
                        "  #  "
                    };
                case ConsoleKey.F12:
                    return new[]
                    {
                        "+-----+",
                        "|     |",
                        "|  *  |",
                        "|     |",
                        "+-----+"
                    };
                default:
                    return null;
            }
        }

        static void BelyegzoKirajzolasa(ConsoleKey gomb, int celX, int celY)
        {
            string[] minta = NyomdaMintaLekerdezese(gomb);
            if (minta == null)
            {
                return;
            }

            int maxSor = Math.Min(minta.Length, RajzlapKezdoY + RajzlapMagassag - celY);

            for (int y = 0; y < maxSor; y++)
            {
                string sor = minta[y];
                int maxOszlop = Math.Min(sor.Length, RajzlapKezdoX + RajzlapSzelesseg - celX);

                for (int x = 0; x < maxOszlop; x++)
                {
                    if (sor[x] != ' ')
                    {
                        Console.SetCursorPosition(celX + x, celY + y);
                        Console.ForegroundColor = RajzSzinLekerdezese();
                        Console.Write(aktualisKarakter);
                    }
                }
            }
        }

        static void SzabadkeziRajzolas()
        {
            int kurzorX = RajzlapKezdoX;
            int kurzorY = RajzlapKezdoY;
            bool ceruzaLent = false;
            bool radirMod = false;

            Console.SetCursorPosition(0, 16);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Nyilak=Mozg, Space=Fel/Le");
            Console.SetCursorPosition(0, 17);
            Console.Write("R=Rain, C=Rad, L=Mint, F1-12");

            while (true)
            {
                int relX = kurzorX - RajzlapKezdoX;
                int relY = kurzorY - RajzlapKezdoY;

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.SetCursorPosition(0, 10);
                Console.Write($"Koordináta: X:{relX,2} Y:{relY,2}");
                Console.SetCursorPosition(0, 13);
                Console.ForegroundColor = ceruzaLent ? ConsoleColor.Green : ConsoleColor.Red;
                Console.Write($"Eszköz:     {(ceruzaLent ? "LETÉVE   " : "FELEMELVE")}");
                Console.SetCursorPosition(0, 14);
                Console.ForegroundColor = rainbowMode ? ConsoleColor.Magenta : ConsoleColor.Gray;
                Console.Write($"Rainbow:    {(rainbowMode ? "BE      " : "KI      ")}");
                Console.SetCursorPosition(0, 15);
                Console.ForegroundColor = radirMod ? ConsoleColor.Yellow : ConsoleColor.Gray;
                Console.Write($"Radír:      {(radirMod ? "BE      " : "KI      ")}");

                Console.SetCursorPosition(kurzorX, kurzorY);
                Console.CursorVisible = true;

                ConsoleKeyInfo gomb = Console.ReadKey(true);
                bool mozgott = false;

                switch (gomb.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (kurzorY > RajzlapKezdoY) { kurzorY--; mozgott = true; }
                        else Console.Beep(400, 120);
                        break;
                    case ConsoleKey.DownArrow:
                        if (kurzorY < RajzlapKezdoY + RajzlapMagassag - 1) { kurzorY++; mozgott = true; }
                        else Console.Beep(400, 120);
                        break;
                    case ConsoleKey.LeftArrow:
                        if (kurzorX > RajzlapKezdoX) { kurzorX--; mozgott = true; }
                        else Console.Beep(400, 120);
                        break;
                    case ConsoleKey.RightArrow:
                        if (kurzorX < RajzlapKezdoX + RajzlapSzelesseg - 1) { kurzorX++; mozgott = true; }
                        else Console.Beep(400, 120);
                        break;
                    case ConsoleKey.R:
                        rainbowMode = !rainbowMode;
                        break;
                    case ConsoleKey.C:
                        radirMod = !radirMod;
                        break;
                    case ConsoleKey.L:
                        MintaBetoltese(kurzorX, kurzorY);
                        Console.SetCursorPosition(0, 16);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Nyilak=Mozg, Space=Fel/Le");
                        Console.SetCursorPosition(0, 17);
                        Console.Write("R=Rain, C=Rad, L=Mint, F1-12");
                        break;
                    case ConsoleKey.F1:
                    case ConsoleKey.F2:
                    case ConsoleKey.F3:
                    case ConsoleKey.F4:
                    case ConsoleKey.F5:
                    case ConsoleKey.F6:
                    case ConsoleKey.F7:
                    case ConsoleKey.F8:
                    case ConsoleKey.F9:
                    case ConsoleKey.F10:
                    case ConsoleKey.F11:
                    case ConsoleKey.F12:
                        BelyegzoKirajzolasa(gomb.Key, kurzorX, kurzorY);
                        break;
                    case ConsoleKey.Spacebar:
                        ceruzaLent = !ceruzaLent;
                        if (ceruzaLent)
                        {
                            Console.SetCursorPosition(kurzorX, kurzorY);
                            if (radirMod)
                            {
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write(' ');
                            }
                            else
                            {
                                Console.ForegroundColor = RajzSzinLekerdezese();
                                Console.Write(aktualisKarakter);
                            }
                        }
                        break;
                    case ConsoleKey.Escape:
                        Console.CursorVisible = false;
                        Console.SetCursorPosition(0, 14);
                        Console.Write("Fel/Le: választás       ");
                        Console.SetCursorPosition(0, 15);
                        Console.Write(new string(' ', 27));
                        Console.SetCursorPosition(0, 16);
                        Console.Write(new string(' ', 27));
                        Console.SetCursorPosition(0, 17);
                        Console.Write(new string(' ', 27));
                        return;
                }

                if (ceruzaLent && mozgott)
                {
                    Console.SetCursorPosition(kurzorX, kurzorY);
                    if (radirMod)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(' ');
                    }
                    else
                    {
                        Console.ForegroundColor = RajzSzinLekerdezese();
                        Console.Write(aktualisKarakter);
                    }
                }
            }
        }

        static ConsoleColor RajzSzinLekerdezese()
        {
            if (!rainbowMode)
            {
                return aktualisSzin;
            }

            ConsoleColor valasztott = szivarvanySzinek[szinIdx];
            szinIdx = (szinIdx + 1) % szivarvanySzinek.Length;
            return valasztott;
        }
    }
}