using System;

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
                            MintaBetoltese();
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
                Console.Beep();
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
                    Console.Beep();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Érvénytelen szín választás!");
                    return aktualisSzin;
            }
        }

        static void MintaBetoltese()
        {
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

            if (System.IO.File.Exists(fajlNev))
            {
                try
                {
                    string[] sorok = System.IO.File.ReadAllLines(fajlNev);

                    for (int y = 0; y < sorok.Length && y < RajzlapMagassag; y++)
                    {
                        Console.SetCursorPosition(RajzlapKezdoX, RajzlapKezdoY + y);

                        string sor = sorok[y];
                        if (sor.Length > RajzlapSzelesseg)
                        {
                            sor = sor.Substring(0, RajzlapSzelesseg);
                        }

                        Console.ForegroundColor = aktualisSzin;
                        Console.Write(sor);
                    }
                    Console.SetCursorPosition(0, 16);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"Betöltve: {sorok.Length} sor          ");
                }
                catch (Exception)
                {
                    Console.Beep();
                    Console.SetCursorPosition(0, 16);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Hiba a fájl olvasásakor!  ");
                }
            }
            else
            {
                Console.Beep();
                Console.SetCursorPosition(0, 16);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("A fájl nem található!      ");
            }
        }

        static void SzabadkeziRajzolas()
        {
            int kurzorX = RajzlapKezdoX;
            int kurzorY = RajzlapKezdoY;
            bool ceruzaLent = false;

            Console.SetCursorPosition(0, 16);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Nyilak=Mozg, Space=Fel/Le");
            Console.SetCursorPosition(0, 17);
            Console.Write("R=Rainbow,   Esc=Kilép   ");

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

                Console.SetCursorPosition(kurzorX, kurzorY);
                Console.CursorVisible = true;

                ConsoleKeyInfo gomb = Console.ReadKey(true);
                bool mozgott = false;

                switch (gomb.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (kurzorY > RajzlapKezdoY) { kurzorY--; mozgott = true; }
                        break;
                    case ConsoleKey.DownArrow:
                        if (kurzorY < RajzlapKezdoY + RajzlapMagassag - 1) { kurzorY++; mozgott = true; }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (kurzorX > RajzlapKezdoX) { kurzorX--; mozgott = true; }
                        break;
                    case ConsoleKey.RightArrow:
                        if (kurzorX < RajzlapKezdoX + RajzlapSzelesseg - 1) { kurzorX++; mozgott = true; }
                        break;
                    case ConsoleKey.R:
                        rainbowMode = !rainbowMode;
                        break;
                    case ConsoleKey.Spacebar:
                        ceruzaLent = !ceruzaLent;
                        if (ceruzaLent)
                        {
                            Console.SetCursorPosition(kurzorX, kurzorY);
                            Console.ForegroundColor = RajzSzinLekerdezese();
                            Console.Write(aktualisKarakter);
                        }
                        break;
                    case ConsoleKey.Escape:
                        Console.CursorVisible = false;
                        Console.SetCursorPosition(0, 14);
                        Console.Write("Fel/Le: választás       ");
                        Console.SetCursorPosition(0, 16);
                        Console.Write(new string(' ', 27));
                        Console.SetCursorPosition(0, 17);
                        Console.Write(new string(' ', 27));
                        return;
                }

                if (ceruzaLent && mozgott)
                {
                    Console.SetCursorPosition(kurzorX, kurzorY);
                    Console.ForegroundColor = RajzSzinLekerdezese();
                    Console.Write(aktualisKarakter);
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