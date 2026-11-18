using System;

namespace KonzolosRajzolo
{
    class Program
    {
        // rajzlap méretei
        const int RajzlapSzelesseg = 80;
        const int RajzlapSzelesseg = 60;
        const int RajzlapMagassag = 25;

        // vizuális szélei a rajzlapnak x,y tengelyen
        const int RajzlapKezdoX = 28;
        const int RajzlapKezdoY = 2;

        static string[] menuPontok = {
            "Karakter választása",
            "Szín választása",
            "Minta betöltése fájlból",
            "PLACEHOLDER 1",
            "PLACEHOLDER 2",
            "Kilépés"
        };

        // aktuális rajzkarakter
        static char aktualisKarakter = '#';

        static int kivalasztottMenuIdx = 0;

        static void Main()
        {
            // megnézi hogy lehet-e ekkorára állítani
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
                            Console.SetCursorPosition(0, 16);
                            aktualisKarakter = KarakterValasztasa();
                            Console.WriteLine("karakter választása függvény");
                        }
                        else if (kivalasztottMenuIdx == 1)
                        {
                            Console.SetCursorPosition(0, 16);
                            Console.Write("szín választása függvény");
                        }
                        else if (kivalasztottMenuIdx == 2)
                        {
                            Console.SetCursorPosition(0, 16);
                            Console.Write("minta betöltése függvény");
                        }
                        else if (kivalasztottMenuIdx == 3)
                        {
                            Console.SetCursorPosition(0, 16);
                            Console.Write("egyéni funkció 1");
                        }
                        else if (kivalasztottMenuIdx == 4)
                        {
                            Console.SetCursorPosition(0, 16);
                            Console.Write("egyéni funkció 2");
                        }
                        else if (kivalasztottMenuIdx == 5)
                        {
                            fut = false;
                        }
                        break;

                //gombnyomások után frissíti a megjelenést
                MenuKirajzolasa();
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

            Console.SetCursorPosition(0, 12);
            Console.Write("Fel/Le: választás");
            Console.SetCursorPosition(0, 13);
            Console.Write("Enter: megnyitás");

            MenuKirajzolasa();
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
            Console.Write("Add meg az új rajzkaraktert: ");

            Console.CursorVisible = true;
            ConsoleKeyInfo betuInfo = Console.ReadKey(false);
            Console.CursorVisible = false;

            if (!char.IsControl(betuInfo.KeyChar))
            {
                Console.SetCursorPosition(0, 16);
                Console.Write(new string(' ', 40));
                Console.SetCursorPosition(0, 16);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"Karakter frissítve: {betuInfo.KeyChar}");

                // Visszaadjuk a megadott karaktert a Main-nek, hogy elmenthesse
                return betuInfo.KeyChar;
            }
            else
            {
                Console.Beep(); // Hiba jelzése hanggal
                Console.SetCursorPosition(0, 16);
                Console.Write(new string(' ', 40));
                Console.SetCursorPosition(0, 16);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Érvénytelen karakter!");

                // Ha hibás volt, vissadjuk a jelenlegi karaktert, hogy ne változzon meg
                return aktualisKarakter;
                }
            }
        }
    }
}