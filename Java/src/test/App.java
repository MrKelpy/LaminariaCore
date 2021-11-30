package test;

import LaminariaCore.IOUtils;
import LaminariaCore.PathUtils;

import java.util.ArrayList;

@SuppressWarnings("RedundantThrows")
public class App {

    public static void run() throws Exception {

        String path = PathUtils.join(System.getProperty("user.dir"), "src", "test", "testfile");
        ArrayList<String> test = IOUtils.readFile(path);
        System.out.println(test);
        System.out.println(path);
        System.out.println(PathUtils.getParent(path));

    }

}
