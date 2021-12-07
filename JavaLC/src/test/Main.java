/*
 * This class is distributed as part of the Laminaria Core (Java Version).
 * Get the Source Code in GitHub:
 * https://github.com/MrKelpy/LaminariaCore
 *
 * The LaminariaCore is Open Source and distributed under the
 * MIT License
 */
package test;

import LaminariaCore.PathUtils;
import LaminariaCore.IOUtils;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;


public class Main {

    public static void main(String[] args) {

        try {
            App.run();
        }
        catch (Exception e) {

            DateTimeFormatter dtf = DateTimeFormatter.ofPattern("yyyy-MM-dd.HH.mm.ss");
            LocalDateTime now = LocalDateTime.now();

            String logs_path = PathUtils.join(System.getProperty("user.dir"), "logs", dtf.format(now) + ".log");
            IOUtils.ensureFilepath(logs_path);
            IOUtils.writeFile(logs_path, e.toString());

        }
    }
}
