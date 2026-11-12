namespace KonzolRajzoló
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(1920, 1080);


            char kurzorJel = '+';
            char rajzJel = '■';
            bool rajzol = false;
            string szin = "red";

            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Cső");

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            //switch (keyInfo.Key)
            //{
            //    case ConsoleKey.Spacebar:


            //    default:
            //}



        }
    }
}
