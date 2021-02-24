using System;
using System.Diagnostics;
using System.IO;

namespace Exo2
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpRequest.Main(args[0]);
            Console.ReadLine();
        }
    }
}