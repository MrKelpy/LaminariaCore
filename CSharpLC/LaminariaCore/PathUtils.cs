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
