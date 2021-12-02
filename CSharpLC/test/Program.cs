using System;
using System.IO;
using LaminariaCore.LaminariaCore;

namespace LaminariaCore {
    class Program {
        static void Main(string[] args) {

            Console.WriteLine("Hello World!");
            String a = Path.Join(PathUtils.getCwd(), "potato");
            Console.WriteLine(a);
            Console.ReadKey();
        }
    }
}
