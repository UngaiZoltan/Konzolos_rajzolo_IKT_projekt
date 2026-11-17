using System;

namespace KonzolosRajzolo
{
    class Program
    {
        // rajzlap méretei

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

            // TeljesKepernyoRajzolasa();

            bool fut = true;

            Console.WriteLine(kivalasztottMenuIdx);

            while (fut)
            {

                ConsoleKeyInfo bekertBillentyu = Console.ReadKey(true);

                switch (bekertBillentyu.Key)
                {
                    case ConsoleKey.UpArrow:
                        kivalasztottMenuIdx--;

                        if (kivalasztottMenuIdx < 0)
                        {
                            kivalasztottMenuIdx = menuPontok.Length;
                        }

                        Console.WriteLine(kivalasztottMenuIdx);

                        break;

                    case ConsoleKey.DownArrow:
                        kivalasztottMenuIdx++;

                        if (kivalasztottMenuIdx > menuPontok.Length)
                        {
                            kivalasztottMenuIdx = kivalasztottMenuIdx = 0;
                        }

                        Console.WriteLine(kivalasztottMenuIdx);

                        break;

                    case ConsoleKey.Enter:
                        if (kivalasztottMenuIdx == 0)
                        {
                            //KarakterValasztasa();
                            Console.WriteLine("karakter választása függvény");
                        }
                        if (kivalasztottMenuIdx == 1)
                        {
                            //SzinValasztasa();
                            Console.WriteLine("szín választása függvény");
                        }
                        if (kivalasztottMenuIdx == 2)
                        {
                            //MintaBetoltese();
                            Console.WriteLine("minta betöltése függvény");
                        }
                        if (kivalasztottMenuIdx == 3)
                        {
                            //PLACEHOLDER();
                            Console.WriteLine("egyéni funkció 1");
                        }
                        if (kivalasztottMenuIdx == 4)
                        {
                            //PLACEHOLDER();
                            Console.WriteLine("egyéni funkció 2");
                        }
                        if (kivalasztottMenuIdx == 5)
                        {
                            fut = false;
                        }
                        break;
                }
            }
        }
    }
}