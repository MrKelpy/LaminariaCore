/*
 * This class is distributed as part of the Laminaria Core (Java Version).
 * Get the Source Code in GitHub:
 * https://github.com/MrKelpy/LaminariaCore
 *
 * The LaminariaCore is Open Source and distributed under the
 * MIT License
 */
package test;

import LaminariaCore.PlaceholderUtils;
import LaminariaDB.LaminariaDB;


@SuppressWarnings("RedundantThrows")
public class App {

    public static void run() throws Exception {
        // Start writing here

        LaminariaDB db = new LaminariaDB("./woeisme");
        System.out.println("hi");
        db.addCollection("Test");

    }
}