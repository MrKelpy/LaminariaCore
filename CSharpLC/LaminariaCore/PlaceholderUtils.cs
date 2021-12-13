using System;
using System.Collections.Generic;
using System.Text;

namespace LaminariaCore {
    public static class PlaceholderUtils {


        public static String small_ipsum() {
            /* Returns a small version of the lorem ipsum text.
             */
            return "Lorem ipsum dolor sit amet.";
        }


        public static String big_ipsum() {
            /* Returns a bigger version of the lorem ipsum text than the small_ipsum method does.
             */
            return "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
                "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris " +
                "nisi ut aliquip ex ea commodo consequat.";
        }


    }
}
