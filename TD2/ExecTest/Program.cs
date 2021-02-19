using System;

namespace ExeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("<h2>Exécutable lancé grâce à l'URL</h2>");
            if (args.Length == 1)
                Console.WriteLine(args[0]);
            else
            {
                foreach (string s in args)
                    Console.WriteLine("<p>" + s + "</p>");
            }
        }
    }
}
