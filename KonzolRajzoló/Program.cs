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
        }
    }
}