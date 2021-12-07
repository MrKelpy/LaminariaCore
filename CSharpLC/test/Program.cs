/*
 * This class is distributed as part of the Laminaria Core (CSharp Version).
 * Get the Source Code in GitHub:
 * https://github.com/MrKelpy/LaminariaCore
 *
 * The LaminariaCore is Open Source and distributed under the
 * MIT License
 */
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
