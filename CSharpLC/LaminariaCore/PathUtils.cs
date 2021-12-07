/*
 * This class is distributed as part of the Laminaria Core (CSharp Version).
 * Get the Source Code in GitHub:
 * https://github.com/MrKelpy/LaminariaCore
 *
 * The LaminariaCore is Open Source and distributed under the
 * MIT License
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LaminariaCore.LaminariaCore {
    public static class PathUtils {

        public static String getCwd() {
            return Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        }


    }
}
