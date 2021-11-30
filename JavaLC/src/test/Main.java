package test;

import LaminariaCore.PathUtils;
import test.App;
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
